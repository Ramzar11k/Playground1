using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperiment : MonoBehaviour
{
    float acceleration = 3.0f;
    float maxSpeed = 3.0f;
    float speedC = 2.0f;
    float jumpSpeed = 25.0f;
    float changeY = 0.0f;
    Rigidbody rb;

    bool grounded = true;
    int walkLayer;

    float jumpCD = 0.0f;

    public Transform firePoint;
    public GameObject bullet;
    public GameObject bullet2;

    private void Start()
    {
        walkLayer = 1 << 10;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (jumpCD > 0)
            jumpCD -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            acceleration *= 2.5f;
            maxSpeed *= 2.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            acceleration = 3.0f;
            maxSpeed = 3.0f;
        }
        CheckJump();

        

        Fire();
    }
    void FixedUpdate()
    {
        Movement(acceleration);

    }

    void Movement(float speed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jumpCD = 1.0f;
            rb.AddForce(Vector3.up * jumpSpeed);
            grounded = false;
        }


        changeY += speedC * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0.0f, changeY, 0.0f);
    }
    void CheckJump()
    {
        Ray ray1 = new Ray(transform.localPosition + new Vector3(transform.localScale.x * 0.4f, -transform.localScale.y * 0.5f, transform.localScale.z * 0.4f), -transform.up);
        Ray ray2 = new Ray(transform.localPosition + new Vector3(-transform.localScale.x * 0.4f, -transform.localScale.y * 0.5f, transform.localScale.z * 0.4f), Vector3.down * 0.5f);
        Ray ray3 = new Ray(transform.localPosition + new Vector3(transform.localScale.x * 0.4f, -transform.localScale.y * 0.5f, -transform.localScale.z * 0.4f), Vector3.down * 0.5f);
        Ray ray4 = new Ray(transform.localPosition + new Vector3(-transform.localScale.x * 0.4f, -transform.localScale.y * 0.5f, -transform.localScale.z * 0.4f), Vector3.down * 0.5f);
        if (Physics.Raycast(ray1, 0.5f, walkLayer) && jumpCD <= 0)
        {
            grounded = true;
        }
        if (Physics.Raycast(ray2, 0.5f, walkLayer) && jumpCD <= 0)
        {
            grounded = true;
        }
        if (Physics.Raycast(ray3, 0.5f, walkLayer) && jumpCD <= 0)
        {
            grounded = true;
        }
        if (Physics.Raycast(ray4, 0.5f, walkLayer) && jumpCD <= 0)
        {
            grounded = true;
        }
    }
    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject bulletInst = Instantiate(bullet, firePoint.position, Quaternion.identity);
                bulletInst.GetComponent<Bullet>().dir = (hit.point - firePoint.position).normalized;
            }
            else
            {
                GameObject bulletInst = Instantiate(bullet, firePoint.position, Quaternion.identity);
                bulletInst.GetComponent<Bullet>().dir = ray.direction;

            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject bulletInst = Instantiate(bullet2, firePoint.position, Quaternion.identity);
                bulletInst.GetComponent<Bullet>().dir = (hit.point - firePoint.position).normalized;
            }
            else
            {
                GameObject bulletInst = Instantiate(bullet2, firePoint.position, Quaternion.identity);
                bulletInst.GetComponent<Bullet>().dir = ray.direction;

            }
        }
    }
}
