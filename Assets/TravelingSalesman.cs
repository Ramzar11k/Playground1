using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingSalesman : MonoBehaviour
{
    public GameObject spot;
    public int count = 10;
    GameObject spotsObject;
    float[] bounds = new float[4];

    void Start()
    {
        SphereObject sphere = new SphereObject(spot);
        spotsObject = GameObject.Find("Spots");
        bounds[0] = spotsObject.transform.GetChild(0).GetChild(0).transform.position.y;
        bounds[1] = spotsObject.transform.GetChild(0).GetChild(1).transform.position.x;
        bounds[2] = spotsObject.transform.GetChild(0).GetChild(2).transform.position.y;
        bounds[3] = spotsObject.transform.GetChild(0).GetChild(3).transform.position.x;
        for (int i = 0; i < 4; i++)
            print(bounds[i] + " " + i.ToString());
        for (int i = 0; i< count; i++)
        {
            Instantiate(spot, new Vector3(Random.Range(bounds[1],bounds[3]), Random.Range(bounds[0], bounds[2])), Quaternion.identity ,spotsObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
