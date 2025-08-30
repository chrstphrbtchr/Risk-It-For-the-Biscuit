using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : Distractable
{
    public GameObject burnEffect;
    public Collider triggerCollider;
    public bool burnerOn;

    private void OnCollisionEnter(Collision collision)
    {
       
        burnerOn = !burnerOn;
        burnEffect.SetActive(burnerOn);
        Invoke("BeginToDistract", 1);
    }

    public void BurnerOff()
    {
        burnerOn = false;
        burnEffect.SetActive(false);
    }

    public void BurnerOn()
    {
        burnerOn = true;
        burnEffect.SetActive(true);
    }
}
