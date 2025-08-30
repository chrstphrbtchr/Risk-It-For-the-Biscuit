using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MouseLaunch : MonoBehaviour
{
    Vector3 originalPosition;
    Rigidbody mousebody;
    SpringJoint spring;
    public Transform cam;
    public float strength;
    public static bool IsLaunching;
    public static bool HasRock = true;
    public static bool IsThrowing;
    public float maxTime = 6, timer = 0;
    public float minStrenght, maxStrength, initStrength, strengthPerSecond;
    public float possibleMaxSpring, possibleMinSpring, springAbsMax, springAbsMin;
    public static float massMuliplier = 1f;
    public static Collideable haul;
    public Transform anchorPoint;
    public float throwForce = 0, throwMult = 5000;
    public float maxThrowForce = 1000;
    public GameObject rock;
    public float maxInverseDistance;
    public float initReelDelay;
    float reelDelay;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
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
        if (!IsLaunching)
        {
            // LAUNCH
            if (Input.GetMouseButtonDown(0))
            {
                //mousebody.constraints = RigidbodyConstraints.FreezeAll;
                strength = initStrength;
            }
            if (Input.GetMouseButton(0))
            {
                strength += Time.deltaTime * strengthPerSecond;
            }
        }
        

        if (Input.GetMouseButtonUp(0) || strength > maxStrength)
        {
            if (IsLaunching)
            {
                return;
            }
            IsLaunching = true;
            
            strength = Mathf.Clamp(strength, minStrenght, maxStrength);
            timer = (strength/maxStrength) * maxTime;  //I'm fucking with this - Michael
            mousebody.AddForce(cam.forward * strength, ForceMode.Impulse);
            strength = 0;
            reelDelay = initReelDelay;
            //mousebody.AddForceAtPosition(cam.forward *strength, mousebody.position, ForceMode.Impulse);
            //mousebody.constraints = RigidbodyConstraints.FreezeRotation;
            //Vector2 camcenter = new Vector2(Camera.main.scaledPixelWidth * 0.5f,Camera.main.scaledPixelHeight * 0.5f);
            //Vector3 testing = Camera.main.ScreenToWorldPoint(camcenter);
            //Vector3 launchDirection = (anchorPoint.up - cam.forward).normalized;
            //Debug.DrawRay(cam.transform.position, cam.forward, Color.yellow, 5);
        }
    }

    void Throw()
    {
        // Charge throw for everything
        if (Input.GetKey(KeyCode.Space) && IsThrowing)
        {
            throwForce += Time.deltaTime * throwMult;
            throwForce = Mathf.Clamp(throwForce, 1, maxThrowForce);
        }

        // BEGIN THROW BAKED GOODS / ROCKS
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (haul != null || HasRock)
            {
                IsThrowing = true;
            }
        }
        
        // THROW WHATEVER
        if (Input.GetKeyUp(KeyCode.Space) || throwForce > maxThrowForce)
        {
            if (!IsThrowing)
            {
                return;
            }

            if(haul != null)
            {
                Vector3 throwDirection = cam.forward;
                RaycastHit hit;
                if (Physics.Raycast(cam.position, cam.forward, out hit, 100))
                {
                    throwDirection = (hit.transform.position - haul.transform.position).normalized;
                }
                Destroy(haul.joint);
                haul.joint = null;
                haul.rb.velocity = Vector3.zero;
                haul.rb.angularDrag = 0.05f;
                haul.rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
                haul.isPickedUp = false;
                haul.isThrown = true;
                haul = null;
                IsThrowing = false;
                Debug.Log("<color=cyan>So long-a, Bowser!</color>");
            }
            else if (HasRock)
            {
                HasRock = false;
                GameObject newRock = Instantiate(rock, transform.position, Quaternion.identity);
                Rigidbody rockybody = newRock.GetComponent<Rigidbody>();
                rockybody.AddForce(cam.forward * throwForce, ForceMode.Impulse);
                IsThrowing = false;
                Debug.Log("<color=magenta>I wanna rock (rock)</color>");
            }
            else
            {
                // sad noise
            }
            throwForce = 0;
        }
    }

    void Reel()
    {
        if (timer > 0)
        {
            if (reelDelay > 0) { reelDelay -= Time.deltaTime; }
            else 
            {
                float timeAndDistance = Mathf.Clamp((maxInverseDistance - Mathf.Abs(Vector3.Distance(this.transform.position, originalPosition)) * Time.deltaTime), Time.deltaTime, 1);
                //  massMuliplier = Mathf.Clamp(massMuliplier, 1, 25);
                if (haul != null) timeAndDistance = timeAndDistance/haul.rb.mass;
                
                timer -= timeAndDistance;
            }

            // timer -= (Time.deltaTime / massMuliplier);
            timer = Mathf.Clamp(timer, 0, maxTime);
            float nowish = timer / maxTime;
            spring.maxDistance = Mathf.Lerp(0.2f, springAbsMax, nowish);
            spring.minDistance = Mathf.Lerp(0f, springAbsMin, nowish);
            spring.spring = Mathf.Lerp(possibleMaxSpring, possibleMinSpring, nowish);
            mousebody.angularDrag = Mathf.Lerp(10, 0.05f, nowish);
        }
        else
        {
            if (IsLaunching)
            {
                // FAILSAFE
                IsLaunching = false;
                mousebody.gameObject.transform.position = originalPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Food Zone")    // Change to whatever?
        {
            return;
        }
        if (IsLaunching && haul != null)
        {
            // DO MATH, ETC.
            Destroy(haul.gameObject);
            haul = null;
        }
        IsLaunching = false;
        mousebody.velocity = Vector3.zero;
        //mousebody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
