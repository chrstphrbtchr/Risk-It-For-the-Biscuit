using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public GameObject burnEffect;
    public Collider triggerCollider;
    public bool burnerOn;

    private void OnCollisionEnter(Collision collision)
    {
       
        burnerOn = !burnerOn;
        burnEffect.SetActive(burnerOn);
    }
}
