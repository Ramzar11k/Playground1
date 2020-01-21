using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGame : MonoBehaviour
{

    bool spinnable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("saf");
        if (other.CompareTag("Bullet") && spinnable)
        {
            Destroy(other.gameObject);
            StartCoroutine(SpinAndStart());
        }
    }

    IEnumerator SpinAndStart()
    {
        spinnable = false;
        float rotZ = 0.0f;
        float speed = 14.0f;
        while (rotZ > -750.0f)
        {
            rotZ -= speed;
            if (speed > 0.1f)
                speed -= 0.13f;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, rotZ);
            yield return new WaitForEndOfFrame();
        }
        transform.parent.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        speed = 0.5f;
        while (rotZ < -720.0f)
        {
            rotZ += speed;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, rotZ);
            yield return new WaitForEndOfFrame();
        }
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        spinnable = true;
        yield return null;
    }
}
