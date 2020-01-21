using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPortal : MonoBehaviour
{
    float speed = 2.0f;
    float changeX = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
    }
    void FixedUpdate()
    {
        MoveCamera(speed);
    }

    void MoveCamera(float speed)
    {
        changeX -= speed * Input.GetAxis("Mouse Y");
        if (changeX > 66.0f)
            changeX = 66.0f;
        else if (changeX < -80.0f)
            changeX = -80.0f;
        transform.eulerAngles = new Vector3(changeX, transform.eulerAngles.y, 0.0f);
    }
}
