using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject particles;
    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            return;
        }

        source.Play();
        Instantiate(particles, transform.position, Quaternion.identity);
        GameObject g = new GameObject("Distraction");
        g.transform.position = this.transform.position;
        Distractable d = g.AddComponent<Distractable>();
        d.continuedDistraction = false;
        d.isCurrentlyDistracting = true;
        d.distanceOfDistraction = 9999;
        d.timeOfDistraction = 3;
        d.BeginToDistract();
        Destroy(this.gameObject);
    }
}
