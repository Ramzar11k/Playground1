using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    float speed = 3.0f;
    float speedC = 2.0f;
    public float changeY = 0.0f;


    // Update is called once per frame
    void FixedUpdate()
    {
        Movement(speed);
        changeY += speedC * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0.0f, changeY, 0.0f);
    }

    void Movement(float speed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-Vector3.left * Time.deltaTime * speed);
        }
    }

}
