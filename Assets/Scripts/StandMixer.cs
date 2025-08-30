using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandMixer : Distractable
{
    public GameObject bowl, doughHook;
    public bool mixerOn;
    Vector3 bowlOrigin, jitter;
    public float mixSpeed, mixMaxSpeed;
    float currentMixSpeed;
    public float bowlJitter;
    // Start is called before the first frame update
    void Start()
    {
        bowlOrigin = bowl.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mixerOn)
        {
             currentMixSpeed = Mathf.Clamp(currentMixSpeed + mixSpeed, 0, mixMaxSpeed);
            jitter.x = Random.Range(-bowlJitter/128, bowlJitter/128);
            jitter.y = Random.Range(-bowlJitter/254, bowlJitter/254);
            jitter.z = Random.Range(-bowlJitter/128, bowlJitter/128);
            bowl.transform.position = jitter + bowlOrigin;
        }
        else
        {
            currentMixSpeed = Mathf.Clamp(currentMixSpeed - .05f, 0, mixMaxSpeed);
            bowl.transform.position = bowlOrigin;
        }

        doughHook.transform.Rotate(0, 0, currentMixSpeed);
    }
/*
    private void OnCollisionEnter(Collision collision)
    {
        ToggleMixer();
    }
*/



    public void TurnOnMixer() => mixerOn = true;
    public void TurnOffMixer() => mixerOn = false;
    public void ToggleMixer() => mixerOn = !mixerOn;
}
