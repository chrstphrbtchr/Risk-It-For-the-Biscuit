using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigation : MonoBehaviour
{
    public enum NPC_State {Walking, Standing, Investigating, Working, Sleeping, Huh, Angry, Fixing, Jumping}
    public NPC_State currentCharacterState = NPC_State.Standing;
    NavMeshAgent myAgent;
    public CharacterTask[] tasks;
    public float maxDistanceFromPoint;
    public float turningDistanceFromPoint = 1;
    public bool isCat;  // Sloppy but what do you expect it's a game jam! <3
    public Distractable currentDistraction;
    // public Distraction currentDistraction = null;
    int taskIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        GoToNextPlace(false);
        ChangeState(NPC_State.Walking);
    }

    private void Update()
    {
        CharacterStateMachine();
        if (isCat)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (myAgent.navMeshOwner is NavMeshLink && currentCharacterState != NPC_State.Jumping)
        {
            currentCharacterState = NPC_State.Jumping;
        }
        if (myAgent.navMeshOwner is not NavMeshLink && currentCharacterState == NPC_State.Jumping)
        {
            //currentCharacterState = NPC_State.Standing;
            ChangeState(NPC_State.Walking); // FOR NOW.
        }
    }

    void CharacterStateMachine()
    {
        switch (currentCharacterState)
        {
            case NPC_State.Angry:
                break;
            case NPC_State.Fixing:
                break;
            case NPC_State.Huh:
                // STANDING BUT LEADS INTO INVESTIGATING INSTEAD OF WORKING / SLEEPING
                break;
            case NPC_State.Investigating:
                if (myAgent.remainingDistance < maxDistanceFromPoint)
                {
                    // ARRIVED AT DISTRACTION
                }
                else
                {

                }
                    break;
            case NPC_State.Sleeping:
                break;
            case NPC_State.Standing:
                if (myAgent.remainingDistance < maxDistanceFromPoint)
                {
                    Vector3 taskpos = tasks[taskIndex].transform.position;
                    Vector3 looking = new Vector3(taskpos.x, transform.position.y, taskpos.z);
                    transform.LookAt(looking);
                    tasks[taskIndex].BeginCharacterTask(this);
                    ChangeState(NPC_State.Working);
                }
                else
                {
                    ChangeState(NPC_State.Walking);
                }
                break;
            case NPC_State.Walking:
                if (myAgent.remainingDistance < maxDistanceFromPoint)
                {
                    ChangeState(NPC_State.Standing);
                    
                    //print(placeIndex);
                }
                break;
            case NPC_State.Working:
                break;
            case NPC_State.Jumping:
                break;
            default:
                break;
        }
    }

    public void ChangeState(NPC_State newState)
    {
        // SOME STATES WILL NOT OVERRIDE OTHERS.
        if (!ShouldOverrideState(newState))
        {
            return;
        }
        currentCharacterState = newState;
    }

    bool ShouldOverrideState(NPC_State newState)
    {
        bool answer = true;
        switch (newState)
        {
            case NPC_State.Walking:
                break;
            case NPC_State.Standing:
                break;
            case NPC_State.Investigating:
                break;
            case NPC_State.Working:
                break;
            case NPC_State.Sleeping:
                break;
            case NPC_State.Fixing:
                break;
            case NPC_State.Angry:
                answer = false;
                break;
            case NPC_State.Huh:
                break;
            case NPC_State.Jumping:
                answer = false;
                break;
            default:
                break;
        }
        return answer;
    }

    public void GoToNextPlace(bool charInterrupted)
    {
        if (charInterrupted)
        {
            myAgent.SetDestination(currentDistraction.distractionFixLocation.position);
        }
        else
        {
            taskIndex++;
            taskIndex = taskIndex % tasks.Length;
            myAgent.SetDestination(tasks[taskIndex].transform.position);
        }
    }
}
