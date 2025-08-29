using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Collideable : MonoBehaviour
{
    public Rigidbody rb;
    public SpringJoint joint;
    public ParticleSystem particles;
    public bool isPickedUp = false, isThrown = false;
    public float velocityBeforeDestruction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            joint.minDistance = 0.25f;
            joint.maxDistance = 1.5f;
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

                if(collision.gameObject.tag == "Character")
                {
                    // Do something else!
                }

                BreakCollideable();
            }
            
        }
    }

    void BreakCollideable()
    {
        // Create a distraction orb or raycast or whatever we're doing HERE
        Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude > velocityBeforeDestruction)
        {
            Instantiate(particles, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
