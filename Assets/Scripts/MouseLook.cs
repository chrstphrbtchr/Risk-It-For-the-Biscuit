using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform player;
    float lookSensitivity = 100f;
    float xRot = 0f;
    static Vector2 lockRotation = new Vector2(-160f, 30f);
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, lockRotation.x, lockRotation.y);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        player.Rotate(Vector3.up * mouseX);
    }
}
