using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distractable : MonoBehaviour
{
    [Tooltip("True if in use")]
    public bool isCurrentlyDistracting = false;
    [Tooltip("The location the character will stand and the direction they will be looking.")]
    public Transform distractionFixLocation;
    [Tooltip("Amount of time a character will be distracted by this distraction")]
    public float timeOfDistraction;
    [Tooltip("How far out the Raycast or whatever will go.")]
    public float distanceOfDistraction;
    [Tooltip("The velocity of the other object needed to cause a distraction.")]
    public float distractabilityThreshold;
    public CharacterNavigation currentlyBeingFixedBy;
    [Header("Animation Parameters for Characters")]
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Gives the player enough time to bounce out.
            Invoke("BeginToDistract", 1);
        }
    }
}
