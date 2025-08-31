using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspicion : MonoBehaviour
{
   public static float maximumSus = 256;
  public  static float currentSus;
    public Transform mouse;
    public float margin;    //1 is perfect, 0 is bad, so it should be something like 0.75f
    public float maxDist = 5;
    public GameObject failscreen;

    // Update is called once per frame
    void Update()
    {
        LookingAtPlayer();
    }

    void LookingAtPlayer()
    {
        if(mouse == null || !MouseLaunch.IsLaunching)
        {
            return;
        }
        Vector3 dir = (mouse.position - transform.position).normalized;
        float dotProd = Vector3.Dot(dir, transform.forward);
        if (dotProd >= margin)
        {
            float dist = Vector3.Distance(transform.position, mouse.position);
            if (dist < maxDist)
            {
                currentSus += (maxDist - dist) * Time.deltaTime;
            }
            
        }
        print(currentSus);
        if(currentSus >= maximumSus)
        {
            Debug.Log("GAME OVER MAN");
            failscreen.SetActive(true);
        }
    }
}
