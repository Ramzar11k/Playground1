using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform transportTo;
    public Transform cameraTarget;
    public Transform ownCamera;
    public Transform initialPos;
    public Vector3 relative;
    Transform player;
    public float angle;
    public Vector3 distanceFromPlayer;
    
    private void Start()
    {
        initialPos = ownCamera;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        relative = transform.InverseTransformPoint(player.position);
        angle = Mathf.Atan2(relative.x * 0.016709f, relative.z) * Mathf.Rad2Deg;
        distanceFromPlayer = transform.position - player.position;
        cameraTarget.eulerAngles = new Vector3(cameraTarget.eulerAngles.x, angle, cameraTarget.eulerAngles.z);
        cameraTarget.localPosition = new Vector3(distanceFromPlayer.x/ 0.016709f, cameraTarget.localPosition.y, distanceFromPlayer.z/2.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Barry")
        {
            other.GetComponent<PlayerPortal>().changeY += 180;
            other.transform.position = transportTo.position + transportTo.transform.right * 0.5f;
        }
    }
}
