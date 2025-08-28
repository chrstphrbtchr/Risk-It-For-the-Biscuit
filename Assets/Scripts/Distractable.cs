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

    }

    public void BeginFixDistraction()
    {

    }

    IEnumerator Distract()
    {
        // ANYTHING THAT HAPPENS GOES HERE
        yield return null;
    }

    IEnumerator Fix()
    {
        yield return new WaitForSeconds(timeOfDistraction);
        yield return null;
    }
}
