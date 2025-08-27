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
            joint.maxDistance = 3;
        }
        else
        {
            if (isThrown)
            {
                BreakCollideable();
            }
            
        }
    }

    void BreakCollideable()
    {
        Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude > velocityBeforeDestruction)
        {
            Instantiate(particles, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
