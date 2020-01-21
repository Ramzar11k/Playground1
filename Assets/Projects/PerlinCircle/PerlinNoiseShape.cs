using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerlinNoiseShape : MonoBehaviour
{
    public float aIncrement = 0.01f;
    public float offsetIncrement = 0.05f;
    Vector3 currentSpot;
    Vector3 firstSpot;
    float xOffset;
    float yOffset;

    float xOffsetAdd;
    float yOffsetAdd;

    public float maxNoise = 5;

    float angleIncrement = 0.0f;

    Slider aIncrementSlider;
    Slider offsetIncrementSlider;
    Slider maxNoiseSlider;

    void Start()
    {
        xOffsetAdd = Random.Range(-100000.0f, 100000.0f);
        yOffsetAdd = Random.Range(-100000.0f, 100000.0f);

        GameObject canvas = GameObject.Find("Canvas");
        aIncrementSlider = canvas.transform.GetChild(0).GetChild(1).GetComponent<Slider>();
        offsetIncrementSlider = canvas.transform.GetChild(0).GetChild(2).GetComponent<Slider>();
        maxNoiseSlider = canvas.transform.GetChild(0).GetChild(3).GetComponent<Slider>();
    }


    void Update()
    {
        ControlUI();
        DrawCircle();
    }

    void DrawCircle()
    {
        if (aIncrement <= 0.0f)
        {
            aIncrement = 0.01f;
        }
        for (float a = 0; a < Mathf.PI * 2; a += aIncrement)
        {

            xOffset = Mathf.Lerp(0, maxNoise, Mathf.InverseLerp(-1, 1, Mathf.Cos(a + angleIncrement)));
            yOffset = Mathf.Lerp(0, maxNoise, Mathf.InverseLerp(-1, 1, Mathf.Sin(a)));
            float r = Mathf.Lerp(1.5f, 3.0f, Mathf.InverseLerp(0, 1, Mathf.PerlinNoise(xOffset + xOffsetAdd, yOffset + yOffsetAdd)));
            float x = r * Mathf.Cos(a) + 20;
            float y = r * Mathf.Sin(a) + 10;
            if (a == 0)
            {
                firstSpot = new Vector3(x, y, 0);
                currentSpot = new Vector3(x, y, 0);
                continue;
            }
            Debug.DrawLine(currentSpot,
                            new Vector3(x, y, 0),
                            Color.green);
            currentSpot = new Vector3(x, y, 0);
        }
        Debug.DrawLine(currentSpot,
            firstSpot,
            Color.green
            );

        xOffsetAdd += offsetIncrement;
        yOffsetAdd += offsetIncrement;
    }

    void ControlUI()
    {
        aIncrement = aIncrementSlider.value;
        offsetIncrement = offsetIncrementSlider.value;
        maxNoise = maxNoiseSlider.value;
    }
}
