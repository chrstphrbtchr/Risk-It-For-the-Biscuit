using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigation : MonoBehaviour
{
    public enum NPC_State {Walking, Standing, Investigating, Working, Sleeping, Huh, Angry, Fixing}
    public NPC_State currentCharacterState = NPC_State.Standing;
    NavMeshAgent myAgent;
    public Transform[] places;
    public float maxDistanceFromPoint;
    // public Distraction currentDistraction = null;
    int placeIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.SetDestination(places[placeIndex].position);
        currentCharacterState = NPC_State.Walking;
    }

    private void Update()
    {
        CharacterStateMachine();
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
                break;
            case NPC_State.Investigating:
                break;
            case NPC_State.Sleeping:
                break;
            case NPC_State.Standing:
                break;
            case NPC_State.Walking:
                if (Vector3.Distance(transform.position, myAgent.destination) < maxDistanceFromPoint)
                {
                    placeIndex++;
                    placeIndex = placeIndex % places.Length;
                    myAgent.SetDestination(places[placeIndex].position);
                    //print(placeIndex);
                }
                break;
            case NPC_State.Working:
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
            default:
                break;
        }
        return answer;
    }
}
