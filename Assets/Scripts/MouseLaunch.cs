using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MouseLaunch : MonoBehaviour
{
    Rigidbody mousebody;
    SpringJoint spring;
    public Transform cam;
    public float strength;
    public static bool IsLauching;
    public static bool HasRock = true;
    public static bool IsThrowing;
    float maxTime = 4, timer = 0;
    public float minStrenght, maxStrength;
    public float possibleMaxSpring;
    public static float massMuliplier = 1;
    public static Collideable haul;
    public Transform anchorPoint;
    public float throwForce = 0, throwMult = 5000;
    public float maxThrowForce = 1000;
    // Start is called before the first frame update
    void Start()
    {
        mousebody = GetComponent<Rigidbody>();   
        spring = GetComponent<SpringJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        Launch();
        Throw();
        Reel();
    }

    void Launch()
    {
        // LAUNCH
        if (Input.GetMouseButtonDown(0))
        {
            //mousebody.constraints = RigidbodyConstraints.FreezeAll;
            strength = 10;
        }
        if (Input.GetMouseButton(0))
        {
            strength += Time.deltaTime * 25;
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsLauching = true;
            
            strength = Mathf.Clamp(strength, minStrenght, maxStrength);
            timer = maxTime;
            //mousebody.constraints = RigidbodyConstraints.FreezeRotation;
            mousebody.AddForce(cam.forward * strength, ForceMode.Impulse);
            //Debug.DrawRay(cam.transform.position, cam.forward, Color.yellow, 5);
        }
    }

    void Throw()
    {
        // THROW BAKED GOODS
        if (haul != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsThrowing = true;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (IsThrowing)
                {
                    throwForce += Time.deltaTime * throwMult;
                    throwForce = Mathf.Clamp(throwForce, 1, maxThrowForce);
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (IsThrowing)
                {
                    Destroy(haul.joint);
                    haul.joint = null;
                    haul.rb.velocity = Vector3.zero;
                    haul.rb.AddForce(cam.forward * throwForce, ForceMode.Impulse);
                    haul.IsPickedUp = false;
                    haul = null;
                    IsThrowing = false;
                    Debug.Log("<color=cyan>So long-a, Bowser!</color>");
                }
            }
        }
        else
        {
            // THROW ROCK
        }
    }

    void Reel()
    {
        if (timer > 0)
        {
            massMuliplier = Mathf.Clamp(massMuliplier, 1, 25);
            timer -= (Time.deltaTime / massMuliplier);
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
