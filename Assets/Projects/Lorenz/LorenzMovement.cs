using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorenzMovement : MonoBehaviour
{

    GameObject point;

    float x = 0;
    float y = 0.3f;
    float z = 0;

    float alpha = 10;
    float beta = 8.0f/3.0f;
    float ro = 28;

    void Start()
    {
        transform.position = new Vector3(x, y, z);
        GetComponent<TrailRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = 0.01f;
        float dx = (alpha * (y - x)) * dt;
        float dy = (x * (ro - z) - y) * dt;
        float dz = (x * y - beta * z) * dt;
        x += dx;
        y += dy;
        z += dz;
        transform.position = new Vector3(x, y, z);
    }
}
