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
    [Tooltip("The velocity of the other object needed to cause a distraction, if any.")]
    public float distractabilityThreshold;
    [Tooltip("True if not an instantaneous distraction and must be fixed before going away.")]
    public bool continuedDistraction;
    [Tooltip("True only if not something that needs to be 'fixed' (i.e. a baked good being thrown)")]
    public bool oneTimeDistraction = false;
    public CharacterNavigation currentlyBeingFixedBy;
    [Header("Animation Parameters for Characters")]
    public int animationParameterForBakerOnFix;
    public int animationParameterForCatOnFix;
    

    private void Update()
    {
        if (continuedDistraction && isCurrentlyDistracting && currentlyBeingFixedBy == null)
        {
            DistractNearestCharacter();
        }
    }

    void DistractNearestCharacter()
    {
        CharacterNavigation cur = null;
        // Continue launching out distraction raycasts until someone picks up
        for (int i = 0; i < CharacterNavigation.DistractibleCharacters.Length; i++)
        {
            CharacterNavigation c = CharacterNavigation.DistractibleCharacters[i];
            Vector3 v = (c.transform.position - this.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, v.normalized, out hit, distanceOfDistraction))
            {
                if (cur == null) { cur = c; }
                else if (v.magnitude > (cur.transform.position - this.transform.position).magnitude)
                {
                    cur = c;
                }
            }
        }
        if (cur != null)
        {
            currentlyBeingFixedBy = cur;
            cur.ChangeState(CharacterNavigation.NPC_State.Huh);
            Debug.Log(cur.gameObject.name);
        }
    }

    public void BeginToDistract()
    {
        DistractNearestCharacter();
        StartCoroutine("Distract");
    }

    public void BeginFixDistraction()
    {
        StartCoroutine("Fix");
    }

    IEnumerator Distract()
    {
        // ANYTHING THAT HAPPENS GOES HERE
        if (continuedDistraction)
        {
            
            yield return null;
        }
        yield return null;  // If just distracting for one minute, then do this.
    }

    IEnumerator Fix()
    {
        currentlyBeingFixedBy.ChangeState(CharacterNavigation.NPC_State.Fixing);
        StopCoroutine("Distract");  // Stops any behaviors in the Distract coroutine that might be ongoing
        if (currentlyBeingFixedBy.isCat)
        {
            //set animation for cat
        }
        else
        {
            // set animation for chef
        }
        yield return new WaitForSeconds(timeOfDistraction);
        currentlyBeingFixedBy.DoneFixingDistraction();
        // Destroys if something that can be cleaned up like a cookie or whatever.
        if(oneTimeDistraction) { Destroy(this.gameObject, 0.25f); }

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
