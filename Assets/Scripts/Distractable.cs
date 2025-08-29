using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distractable : MonoBehaviour
{
    public bool isCurrentlyDistracting = false;
    public Transform distractionFixLocation;
    public float timeOfDistraction;
    public float distanceOfDistraction;
    public CharacterNavigation currentlyBeingFixedBy;
    public int animationParameterForBakerOnFix;
    public int animationParameterForCatOnFix;

    private void Update()
    {
        
    }

    public void BeginToDistract()
    {
        StartCoroutine("Distract");
    }

    public void BeginFixDistraction()
    {
        StartCoroutine("Fix");
    }

    IEnumerator Distract()
    {
        // ANYTHING THAT HAPPENS GOES HERE
        yield return null;
    }

    IEnumerator Fix()
    {
        currentlyBeingFixedBy.ChangeState(CharacterNavigation.NPC_State.Fixing);
        if (currentlyBeingFixedBy.isCat)
        {
            //set animation for cat
        }
        else
        {
            // set animation for chef
        }
        yield return new WaitForSeconds(timeOfDistraction);
        currentlyBeingFixedBy.ChangeState(CharacterNavigation.NPC_State.Standing);
        currentlyBeingFixedBy.GoToNextPlace(false);
        yield return null;
    }
}
