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
    public static CharacterNavigation[] DistractibleCharacters = new CharacterNavigation[2];
    int taskIndex = 0;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if(isCat || DistractibleCharacters[0] != null)
        {
            DistractibleCharacters[1] = this;
        }
        else
        {
            DistractibleCharacters[0] = this;
        }
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
            case NPC_State.Angry:   // UNNECESSARY?!
                break;
            case NPC_State.Fixing:
                break;
            case NPC_State.Huh:
                // STANDING BUT LEADS INTO INVESTIGATING INSTEAD OF WORKING / SLEEPING
                myAgent.isStopped = true;
                if(currentDistraction != null)
                {
                    myAgent.SetDestination(currentDistraction.distractionFixLocation.position);
                    ChangeState(NPC_State.Investigating);
                }
                else
                {
                    ChangeState(NPC_State.Walking);
                }
                // Turn into IENUM?
                myAgent.isStopped = false;
                break;
            case NPC_State.Investigating:
                if (myAgent.remainingDistance < maxDistanceFromPoint)
                {
                    // ARRIVED AT DISTRACTION
                    if(currentDistraction != null)
                    {
                        currentDistraction.BeginFixDistraction();
                    }
                }
                else
                {

                }
                break;
            case NPC_State.Sleeping:
                break;
            case NPC_State.Standing:
                if (myAgent.remainingDistance < myAgent.stoppingDistance)
                {
                    //Vector3 taskpos = tasks[taskIndex].transform.forward;
                    //Vector3 looking = new Vector3(taskpos.x, transform.position.y, taskpos.z);
                    //transform.LookAt(looking);
                    ChangeState(NPC_State.Working);
                    tasks[taskIndex].BeginCharacterTask(this);
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
                // myAgent.isStopped = true;
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
        SwitchAnimations();
        // Change animations here!
    }

    void SwitchAnimations()
    {
        int mod = 0;
        
        anim.SetFloat("RB_VEL", myAgent.velocity.magnitude);
        if (currentDistraction != null)
        {
            if (currentCharacterState == NPC_State.Fixing)
            {
                if (isCat)
                {
                    mod = currentDistraction.animationParameterForCatOnFix;
                }
                else
                {
                    mod = currentDistraction.animationParameterForBakerOnFix;
                } 
            }
            anim.SetInteger("ANIM_MOD", mod);

            if (currentDistraction.transform.position.y < -1.7f)
            {
                anim.SetBool("IS_ON_GROUND", true);
            }
            else
            {
                anim.SetBool("IS_ON_GROUND", false);
            }  
            anim.SetFloat("TIME_OF_TASK", currentDistraction.timeOfDistraction);
        }
        else
        {
            switch (currentCharacterState)
            {
                case NPC_State.Working:
                case NPC_State.Fixing:
                    mod = 3; break;
                case NPC_State.Walking:
                    mod = 1; break;
                case NPC_State.Huh:
                    mod = 2; break;
                case NPC_State.Sleeping:
                    mod = 5; break;
                case NPC_State.Angry:
                    mod = 4; break;
                default:
                    mod = 0; break;
            }
            anim.SetInteger("ANIM_MOD", mod);
            anim.SetBool("IS_ON_GROUND", true);
            anim.SetFloat("TIME_OF_TASK", tasks[taskIndex].taskTime);
        }

        if (isCat && currentCharacterState == NPC_State.Jumping)
        {
            anim.SetBool("JUMPING", true);
        }
        else
        {
            anim.SetBool("JUMPING", false);
        }
        
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
                if (currentCharacterState == NPC_State.Huh ||
                    currentCharacterState == NPC_State.Fixing ||
                    currentCharacterState == NPC_State.Angry ||
                    currentCharacterState == NPC_State.Working ||
                    currentCharacterState == NPC_State.Jumping)
                {
                    answer = false;
                }
                break;
            case NPC_State.Jumping:
                answer = false;
                break;
            default:
                break;
        }
        return answer;
    }

    public IEnumerator GoToNextPlace(bool charInterrupted)
    {
        if (charInterrupted)
        {
            myAgent.SetDestination(currentDistraction.distractionFixLocation.position);
            while (Mathf.Abs(Vector3.Angle(transform.forward.normalized, currentDistraction.distractionFixLocation.forward.normalized)) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, currentDistraction.distractionFixLocation.rotation, 0.5f);
                yield return null;
            }
        }
        else
        {
            taskIndex++;
            taskIndex = taskIndex % tasks.Length;
            Debug.LogFormat("{0} is going to {1}", this.gameObject.name, tasks[taskIndex].name);
            while (Mathf.Abs(Vector3.Angle(transform.forward.normalized, tasks[taskIndex].transform.forward.normalized)) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, tasks[taskIndex].transform.rotation, 0.5f);
                yield return null;
            }
            myAgent.SetDestination(tasks[taskIndex].transform.position);
            ChangeState(NPC_State.Walking);
        }

        
        yield return null;
    }

    public void DoneFixingDistraction()
    {
        ChangeState(CharacterNavigation.NPC_State.Walking);
        currentDistraction = null;
        GoToNextPlace(false);
    }
}
