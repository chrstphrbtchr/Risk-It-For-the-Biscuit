using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collideable : MonoBehaviour
{
    bool IsPickedUp = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (IsPickedUp)
        {
            return;
        }
        if(collision.collider.gameObject.tag == "Player")
        {
            if(MouseLaunch.haul != null) { return; }
            MouseLaunch.haul = this;
            IsPickedUp=false;

            SpringJoint joint = this.gameObject.AddComponent<SpringJoint>();

            joint.connectedBody = collision.collider.GetComponentInParent<Rigidbody>();
            joint.spring = 500;
            joint.minDistance = 1;
            joint.maxDistance = 5;
        }
    }
}
