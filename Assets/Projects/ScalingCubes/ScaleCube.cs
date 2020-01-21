using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCube : MonoBehaviour
{
    float increment;
    float maxHeight = 5.0f;
    float stepChange = 2.0f;
    float scaleY;

    Renderer objMat;
    
    // Start is called before the first frame update
    void Start()
    {
        increment = Mathf.Abs(transform.position.x / 2.0f) + Mathf.Abs(transform.position.z / 2.0f); ;
        objMat = GetComponent<Renderer>();
        objMat.material.color = new Color32(180, 0, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScale();
    }

    void ChangeScale()
    {
        scaleY = (Mathf.Sin(increment) + 1) * 0.5f * maxHeight;
        transform.localScale = new Vector3(1, scaleY, 1);
        increment += stepChange * Time.deltaTime;
    }
}
