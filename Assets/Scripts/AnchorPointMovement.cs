using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPointMovement : MonoBehaviour
{
    public Transform viewport;
    public float speed;
    private Vector3 startingPoint;
    public float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ShiftAnchorPoint();
    }

    void ShiftAnchorPoint()
    {
        if (MouseLaunch.IsLaunching)
        {
            return;
        }
        //transform.rotation = viewport.rotation;
        //transform.up = -viewport.forward;
        float apx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float apz = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        Vector3 fwrdRelative = apz * viewport.up;
        Vector3 rightRelative = apx * viewport.right;
        Vector3 camRelative = (fwrdRelative + rightRelative);
        Vector3 finalNewPosition = new Vector3(camRelative.x, 0, camRelative.z);
        Vector3 wantToGo = transform.position + finalNewPosition;
        Vector3 offset = wantToGo - startingPoint;
        transform.position = startingPoint + Vector3.ClampMagnitude(offset, maxDistance);
        //transform.position += finalNewPosition;
        //transform.localPosition += new Vector3(apx, 0, apz);
    }
}
