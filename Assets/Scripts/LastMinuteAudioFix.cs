using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMinuteAudioFix : MonoBehaviour
{
    AudioListener listener;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TurnEverythingOn", 1.5f);
    }

    void TurnEverythingOn()
    {
        Collideable.canMakeNoise = true;
    }
}
