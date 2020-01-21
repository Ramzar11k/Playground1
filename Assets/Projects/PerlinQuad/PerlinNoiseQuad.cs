using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseQuad : MonoBehaviour
{
    int width = 256;
    int height = 256;
    Renderer rendererr;

    public float scale = 1.0f;
    float offsetX;
    float offsetY;

    float randomR;
    float randomG;
    float randomB;

    public bool color = false;

    private void Start()
    {
        offsetX = Random.Range(0.0f, 99999.0f);
        offsetY = Random.Range(0.0f, 99999.0f);
        rendererr = GetComponent<Renderer>();
        randomR = Random.Range(0.0f, 99999.0f);
        randomG = Random.Range(0.0f, 99999.0f);
        randomB = Random.Range(0.0f, 99999.0f);

        if (color)
        {
            scale = 7.0f;
        }
        else
            scale = 75.0f;
    }
    void Update()
    {
        rendererr.material.mainTexture = GenerateTexture();

        if (color)
        {
            offsetX += 0.01f;
            offsetY += 0.01f;
        }
        else
        {
            offsetX += 100.0f;
            offsetY += 100.0f;
        }
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Color color = CalculateColor(i, j);
                texture.SetPixel(i, j, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int i, int j)
    {
        float iCoord = (float)i / width * scale + offsetX;
        float jCoord = (float)j / height * scale + offsetY;
        if (!color)
        {
            float sample = Mathf.PerlinNoise(iCoord, jCoord);
            return new Color(sample, sample, sample);
        }
        else
        {
        float sampleR = Mathf.PerlinNoise(iCoord + randomR, jCoord + randomR);
        float sampleG = Mathf.PerlinNoise(iCoord + randomG, jCoord + randomG);
        float sampleB = Mathf.PerlinNoise(iCoord + randomB, jCoord + randomB);
        return new Color(sampleR, sampleG, sampleB);
        }
    }
}
