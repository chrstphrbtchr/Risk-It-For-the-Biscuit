using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform player;
    float lookSensitivity = 500f;
    float xRot = 0f;
    static Vector2 lockRotation = new Vector2(-160f, 30f);
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Debug.DrawRay(transform.position, transform.forward * 50, Color.cyan, 0.5f);
    }

    void Look()
    {
        //if (MouseLaunch.IsLauching) { return; } 
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, lockRotation.x, lockRotation.y);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        player.Rotate(Vector3.up * mouseX);
    }
}
