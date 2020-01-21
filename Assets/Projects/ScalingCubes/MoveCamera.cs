using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    float speed = 2.0f;

    void Update()
    {
        ZoomCamera(speed);
    }

    void ZoomCamera(float speed)
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            transform.Translate(Vector3.forward * speed);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            transform.Translate(-Vector3.forward * speed);
        }
    }
}
