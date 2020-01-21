using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetGameCanvas : MonoBehaviour
{
    Text welcome;
    // Start is called before the first frame update
    void Start()
    {
        welcome = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
        StartCoroutine(WelcomeText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WelcomeText()
    {
        while (welcome.color.a < 255)
        {
            welcome.color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
