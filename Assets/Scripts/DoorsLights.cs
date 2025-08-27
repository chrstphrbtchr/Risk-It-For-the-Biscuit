using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsLights : MonoBehaviour
{
    public GameObject doorHinge;
    public Light connectedLight;
    public Quaternion rotateOnOpen;
    public float openingTime;
    Quaternion rotateOnClose, goalRotate, startRotate;
    public bool isOpen = false, testing;
    float thisRotateTime;

    void Start()
    {
        if (doorHinge == null)  doorHinge = this.gameObject; 
        rotateOnClose = doorHinge.transform.rotation;
        goalRotate = rotateOnClose;
        
    }

    private void Update()
    {
        if (testing) { doorHinge.transform.rotation = rotateOnOpen; }
        else
        {
            if (doorHinge.transform.rotation != goalRotate)
            {
                float nowish = (thisRotateTime / openingTime);
                doorHinge.transform.rotation = Quaternion.Slerp(startRotate, goalRotate, nowish);
                thisRotateTime += Time.deltaTime;
            }
         
              if (connectedLight != null) connectedLight.enabled = isOpen;
        }
    }

    public void openDoor()
    {
        isOpen = true;
        startRotate = doorHinge.transform.rotation;
        goalRotate = rotateOnOpen;
        thisRotateTime = 0;
    }

    public void closeDoor()
    {
        isOpen = false;
        startRotate = doorHinge.transform.rotation;
        goalRotate = rotateOnClose;
        thisRotateTime = 0;
    }

    public void toggleDoor()
    {
        if (isOpen) { closeDoor(); }
        else { openDoor(); }
    }
}
