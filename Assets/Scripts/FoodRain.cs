using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodRain : MonoBehaviour
{
    public GameObject[] foods;
    float rx = 5, lx = -5, zed = 5;
    void Start()
    {
        FoodNotBombs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FoodNotBombs()
    {
        GameObject g = Instantiate(foods[Random.Range(0, foods.Length)]);
        g.AddComponent<FoodSpin>();
        float a = Random.Range(rx, lx);
        float b = Random.Range(0, zed);
        g.transform.position = new Vector3(a, this.transform.position.y, zed);
        g.AddComponent<BoxCollider>();
        g.AddComponent<Rigidbody>();
        
        Invoke("FoodNotBombs", 0.5f);
    }
}
