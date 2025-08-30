using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchGauge : MonoBehaviour
{
    public Vector2 coverStartPos, coverEndPos;
    public float coverEndX;
    public MouseLaunch mouseLaunch;
    public Image cover, mask;
    public bool debugEndPos;
    public bool throwNotLaunch;
    // Start is called before the first frame update
    void Start()
    {
        coverStartPos = cover.transform.position;
        coverEndPos.y = cover.transform .position.y;
       
        RectTransform rt = mask.GetComponent<RectTransform>();
       Vector3[] v = new Vector3[4];
       rt.GetWorldCorners (v);
        coverEndPos.x = coverStartPos.x + ((v[3].x - v[0].x)/1) ;
    }

    // Update is called once per frame
    void Update()
    {
        float nowStr = throwNotLaunch?mouseLaunch.throwForce / mouseLaunch.maxThrowForce : mouseLaunch.strength / mouseLaunch.maxStrength;
        
        cover.transform.position = Vector2.Lerp(coverStartPos, coverEndPos, nowStr);
        if (debugEndPos)
        {
            coverEndPos.x = coverEndX;
            cover.transform.position = coverEndPos; 
        }
    }
}
