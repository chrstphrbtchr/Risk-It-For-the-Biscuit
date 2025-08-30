using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public bool fanOn, testing;
    public float rotateSpeed, maxSpeed;
    float currentSpeed;
    public GameObject fanLight;


    // Update is called once per frame
    void Update()
    {

        if (fanOn) currentSpeed = Mathf.Clamp(currentSpeed + rotateSpeed, 0, maxSpeed);
        else currentSpeed = Mathf.Clamp(currentSpeed - .01f, 0, 1.5f);
        this.transform.Rotate(0, 0, currentSpeed);
    }

   

    public void turnFanOn() => fanOn = true;
    public void turnFanOff()  
            {fanOn = false; 
            fanLight.SetActive(false);}

}
