using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFireworks : MonoBehaviour
{
    PlayerFireworks playerFireworks;
    float speed = 2.0f;
    float changeX = 0.0f;
    int layerMask;

    void Start()
    {
        playerFireworks = transform.parent.GetComponent<PlayerFireworks>();
        Cursor.lockState = CursorLockMode.Locked;
        layerMask = 1 << 8;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f, layerMask))
        {
           Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            playerFireworks.currentSelection = hit.transform.gameObject;
 
            (hit.transform.GetComponent("Halo") as Behaviour).enabled = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 2.0f, Color.white);
            if (playerFireworks.currentSelection != null)
                (playerFireworks.currentSelection.transform.GetComponent("Halo") as Behaviour).enabled = false;
            playerFireworks.currentSelection = null;
        }

    }
    void FixedUpdate()
    {
        MoveCamera(speed);
    }

    void MoveCamera(float speed)
    {
        changeX -= speed * Input.GetAxis("Mouse Y");
        if (changeX > 66.0f)
            changeX = 66.0f;
        else if (changeX < -80.0f)
            changeX = -80.0f;
        transform.eulerAngles = new Vector3(changeX, transform.eulerAngles.y, 0.0f);
    }

}
