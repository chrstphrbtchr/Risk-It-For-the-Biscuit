using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introcube : MonoBehaviour
{
   public Vector3 goalPos;
   public Vector3 startPos;
   public bool turnedOn = false;
   public float cubetime, timer, distance;
    // Start is called before the first frame update
    void Start()
    {
        goalPos = transform.position;
        startPos = goalPos;
        startPos.z = distance;
        transform.position = startPos;
    }

    private void Update()
    {
        if (turnedOn && transform.position != goalPos) 
        {
            timer += Time.deltaTime;
            float nowish = timer / cubetime;
            transform.position = Vector3.Lerp(startPos, goalPos, nowish);
        }
    }

    public void turnOn(float moveTime)
    {
        turnedOn = true;
        gameObject.SetActive(true);
        cubetime = moveTime;
        timer = 0;
    }
}
