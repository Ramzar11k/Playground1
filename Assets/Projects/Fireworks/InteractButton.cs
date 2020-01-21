using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    public GameObject firework;
    GameObject button;
    GameObject gates;
    List<int> gateso = new List<int>();

    bool fireWorks = false;
    float timer = 0.0f;
    void Start()
    {
        gates = GameObject.Find("FireworkSpots");
        for (int i = 0; i < gates.transform.childCount; i++)
        {
            gateso.Add(i);
        }
        button = transform.GetChild(0).gameObject;
        gameObject.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(PressButton());
    }

    void Update()
    {
        if (!fireWorks)
            return;

        if (timer < 2.0f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            StartCoroutine(FireFireworks());
            timer = 0.0f;
        }
    }

    IEnumerator PressButton()
    {
        do
        {
            button.transform.Translate(Vector3.down * Time.deltaTime * 0.1f);
            yield return new WaitForFixedUpdate();
        } while (button.transform.localPosition.y > 0.05f);
        StartCoroutine(OpenGates());
        yield return null;
    }

    IEnumerator OpenGates()
    {
        float rotZ = 0.0f;
        do
        {
            foreach (Transform child in gates.transform)
            {
                child.GetChild(0).eulerAngles = new Vector3(0.0f, 0.0f, -rotZ);
                child.GetChild(1).eulerAngles = new Vector3(0.0f, 0.0f, rotZ);
            }
            rotZ += 1.0f;
            yield return new WaitForFixedUpdate();
        } while (rotZ < 125.0f);
        fireWorks = true;
        yield return null;
    }

    IEnumerator FireFireworks()
    {
        for (int i = 0; i < gateso.Count; i++)
        {
            int temp = gateso[i];
            int randomIndex = Random.Range(i, gateso.Count);
            gateso[i] = gateso[randomIndex];
            gateso[randomIndex] = temp;
        }
        for (int i = 0; i < gateso.Count; i++)
        {
            Vector3 pos = gates.transform.GetChild(gateso[i]).position;
            Instantiate(firework, pos - new Vector3(0, 1.0f, 0), Quaternion.identity);
            for (int j = 0; j < 20; j++)
                yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
