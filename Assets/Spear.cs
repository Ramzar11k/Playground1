using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);
        StartCoroutine(Launch());
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(2.0f);
        rb.AddForce(transform.forward * 2000.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 0.1f);
    }
}
