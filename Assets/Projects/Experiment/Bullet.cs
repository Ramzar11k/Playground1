using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 15.0f;
    public Vector3 dir;
    private void Start()
    {
        Destroy(gameObject, 3.0f);
    }
    void FixedUpdate()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
