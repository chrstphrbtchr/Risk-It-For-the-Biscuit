using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ratTail : MonoBehaviour
{
    public GameObject Anchor, Body;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, Anchor.transform.position);
        line.SetPosition(1, Body.transform.position);
    }
}
