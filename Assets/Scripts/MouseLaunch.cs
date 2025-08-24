using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLaunch : MonoBehaviour
{
    Rigidbody mousebody;
    SpringJoint spring;
    public Transform cam;
    public float strength;
    public static bool IsLauching;
    float maxTime = 3, timer = 0;
    public float maxStrength;
    public float possibleMaxSpring;
    // Start is called before the first frame update
    void Start()
    {
        mousebody = GetComponent<Rigidbody>();   
        spring = GetComponent<SpringJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            strength = 0;
        }
        if (Input.GetMouseButton(0))
        {
            strength += Time.deltaTime * 25;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //IsLauching = true;
            //mousebody.constraints = RigidbodyConstraints.None;
            strength = Mathf.Clamp(strength, 0, maxStrength);
            timer = maxTime;
            mousebody.AddForce(cam.forward * strength, ForceMode.Impulse);
        }

        if (Input.GetMouseButton(1))
        {
            mousebody.mass += 1;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, maxTime);
            float nowish = timer / maxTime;
            spring.maxDistance = Mathf.Lerp(0, 50, nowish);
            spring.minDistance = Mathf.Lerp(0, 10, nowish);
            spring.spring = Mathf.Lerp(possibleMaxSpring, 10, nowish);
        }
    }
}
