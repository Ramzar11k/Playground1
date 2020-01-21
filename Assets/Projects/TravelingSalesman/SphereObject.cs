using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereObject
{
    float posX = Random.Range(-13, 14);
    float posY = Random.Range(-4, 5);

    GameObject sphere;

    SphereObject nextSphere = null;
    SphereObject prevSpehere = null;

    public SphereObject(GameObject sphereG)
    {
        sphere = sphereG;
    }
    public float GetPosX()
    {
        return posX;
    }

}
