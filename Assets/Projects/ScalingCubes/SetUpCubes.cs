using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCubes : MonoBehaviour
{
    public GameObject cube;
    List<GameObject> cubes = new List<GameObject>();
    public int nrOfCubes;
    public int rows;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        if ((nrOfCubes * rows) != cubes.Count)
            SetUp();
    }

    void SetUp()
    {
        foreach (var cube in cubes)
        {
            Destroy(cube);
        }
        cubes.Clear();
        float posX = -0.5f * (nrOfCubes - 1);
        float posZ = -0.5f * (rows - 1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < nrOfCubes; j++)
            {
                cubes.Add(Instantiate(cube, new Vector3(posX, 0, posZ), Quaternion.identity));
                posX += 1.0f;
            }
            posZ += 1.0f;
            posX = -0.5f * (nrOfCubes - 1);
        }
    }
}
