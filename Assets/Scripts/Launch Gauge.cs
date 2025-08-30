using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchGauge : MonoBehaviour
{
    public Vector2 coverStartPos, coverEndPos;
    public MouseLaunch mouseLaunch;
    public Image cover;
    public bool debugEndPos;
    public bool throwNotLaunch;
    // Start is called before the first frame update
    void Start()
    {
        coverStartPos = cover.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float nowStr = throwNotLaunch?mouseLaunch.throwForce / mouseLaunch.maxThrowForce : mouseLaunch.strength / mouseLaunch.maxStrength;
        
        cover.transform.position = Vector2.Lerp(coverStartPos, coverEndPos, nowStr);
        if (debugEndPos) { cover.transform.position = coverEndPos; }
    }
}
