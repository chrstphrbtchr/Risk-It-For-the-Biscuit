using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(NavMeshObstacle))]
public class Collideable : MonoBehaviour
{
    public Rigidbody rb;
    public SpringJoint joint;
    public ParticleSystem particles;
    public bool isPickedUp = false, isThrown = false;
    public float velocityBeforeDestruction;
    public string displayName = "food";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<NavMeshObstacle>().carving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (isPickedUp)
        {
            return;
        }

        if(collision.collider.gameObject.tag == "Player")
        {
            if(MouseLaunch.haul != null) { return; }
            MouseLaunch.haul = this;
            isPickedUp=false;

            joint = this.gameObject.AddComponent<SpringJoint>();

            joint.connectedBody = collision.collider.GetComponentInParent<Rigidbody>();
            joint.spring = 500;
            joint.minDistance = 0.1f;
            joint.maxDistance = .5f;
            rb.angularDrag = 5;
            
        }
        else
        {
            if (isThrown)
            {
                if(collision.gameObject.tag == "Distraction")
                {
                    Distractable dis = collision.gameObject.GetComponent<Distractable>();
                    if (dis==null) { Debug.LogWarning("NO SUCH DISTRACTABLE"); return; }
                    dis.BeginToDistract();
                }
                else if (collision.gameObject.tag == "Character")
                {
                    // Do something else!
                }
                else
                {
                    // Don't add more distractions!
                    CreateNewDistraction();
                }
                // Break regardless
                BreakCollideable(); 
            }
            
        }
    }

    bool ShouldBreak()
    {
        return (rb.velocity.magnitude > velocityBeforeDestruction);
    }
    void BreakCollideable()
    {
        //Debug.Log(rb.velocity.magnitude);
        if (ShouldBreak())
        {
            Instantiate(particles, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void CreateNewDistraction()
    {
        if (ShouldBreak())
        {
            GameObject g = new GameObject("Distraction");
            g.transform.position = this.transform.position;
            Distractable d = g.AddComponent<Distractable>();
            d.continuedDistraction = false;
            d.isCurrentlyDistracting = true;
            d.distanceOfDistraction = this.rb.velocity.magnitude;
            d.distanceOfDistraction = 9999; // TESTINMG DELETEM E
            
            //d.animationParameterForBakerOnFix = 0;
            //d.animationParameterForCatOnFix = 0;
            d.BeginToDistract();
        }
    }
}
