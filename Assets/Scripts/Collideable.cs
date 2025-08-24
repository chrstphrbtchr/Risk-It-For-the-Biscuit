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
            IsPickedUp=false;
            SpringJoint joint = this.gameObject.AddComponent<SpringJoint>();

            joint.connectedBody = collision.collider.GetComponentInParent<Rigidbody>();
            joint.spring = 500;
        }
    }
}
