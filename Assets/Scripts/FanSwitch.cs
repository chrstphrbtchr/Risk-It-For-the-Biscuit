using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSwitch : MonoBehaviour
{
    public Fan fan;

    private void OnCollisionEnter(Collision collision)
    {
        fan.turnFanOff();
    }
}
