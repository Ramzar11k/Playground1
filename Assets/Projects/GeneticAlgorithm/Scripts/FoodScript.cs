using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent") && other.GetComponent<AgentScript>().hungry)
        {
            Destroy(gameObject);
            other.GetComponent<AgentScript>().StartEating();
        }
    }
}
