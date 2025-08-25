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
        transform.rotation = viewport.rotation;
        float apx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float apy = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.localPosition += new Vector3(apx, 0, apy);
    }
}
