using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkScript : MonoBehaviour
{
    ParticleSystem ps;
    Color color;
    Rigidbody rb;
    float timer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GetComponent<Renderer>().material.color = color;
        var main = ps.main;
        main.startColor = color;
        rb.AddForce(Vector3.up * 800.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2.0f)
        {
            var main = ps.main;
            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, main.duration);
            rb.isKinematic = true;
            ps.Play();
            timer = -100.0f;
        }
    }
}
