using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseLaunch : MonoBehaviour
{
    Rigidbody mousebody;
    SpringJoint spring;
    public Transform cam;
    public float strength;
    public static bool IsLauching;
    float maxTime = 4, timer = 0;
    public float minStrenght, maxStrength;
    public float possibleMaxSpring;
    public static float massMuliplier = 1;
    public static Collideable haul;
    public Transform anchorPoint;
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
            strength = 10;
        }
        if (Input.GetMouseButton(0))
        {
            strength += Time.deltaTime * 25;
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsLauching = true;
            //mousebody.constraints = RigidbodyConstraints.FreezeRotation;
            strength = Mathf.Clamp(strength, minStrenght, maxStrength);
            timer = maxTime;
            mousebody.AddForce(cam.forward * strength, ForceMode.Impulse);
            //Debug.DrawRay(cam.transform.position, cam.forward, Color.yellow, 5);
        }

        if (timer > 0)
        {
            massMuliplier = Mathf.Clamp(massMuliplier, 1, 25);
            timer -= (Time.deltaTime / massMuliplier );
            timer = Mathf.Clamp(timer, 0, maxTime);
            float nowish = timer / maxTime;
            spring.maxDistance = Mathf.Lerp(0, 55, nowish);
            spring.minDistance = Mathf.Lerp(0, 5, nowish);
            spring.spring = Mathf.Lerp(possibleMaxSpring, 10, nowish);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Food Zone")
        {
            return;
        }
        if (IsLauching && haul != null)
        {
            // DO MATH, ETC.
            Destroy(haul.gameObject);
            haul = null;
        }
        IsLauching = false;
        mousebody.velocity = Vector3.zero;
        //mousebody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
