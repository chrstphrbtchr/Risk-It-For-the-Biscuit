using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpin : MonoBehaviour
{
    float sx, sy, sz;
    // Start is called before the first frame update
    void Start()
    {
        sx = Random.Range(-1, 1);
        sz = Random.Range(-1, 1);
        sy = Random.Range(-1, 1);
        this.transform.rotation = Random.rotation;
        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(sx, sy, sz));
    }
}
