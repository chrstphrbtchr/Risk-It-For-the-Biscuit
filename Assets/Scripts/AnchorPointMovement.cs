using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPointMovement : MonoBehaviour
{
    public Transform viewport;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = viewport.rotation;
        float apx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float apz = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        Vector3 fwrdRelative = apz * viewport.up;
        Vector3 rightRelative = apx * viewport.right;
        Vector3 camRelative = (fwrdRelative + rightRelative);
        Vector3 finalNewPosition = new Vector3(camRelative.x, 0, camRelative.z);
        transform.position += finalNewPosition;
        //transform.localPosition += new Vector3(apx, 0, apz);
    }
}
