using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentScript : MonoBehaviour
{
    public AgentConstructor agent;
    public GameObject target;

    bool rotating = false;
    bool turn = false;
    bool dad = false;
    public bool hungry = true;

    float timer = 0.0f;
    float energy = 20.0f;

    public Color rayColor = Color.red;

    public int foodCollected = 0;

    public float speed;
    public float turnRate;
    public float turnFreq;
    public float fieldOfView;
    public float viewDistance;

    public float fitness = 0.0f;

    void Start()
    {
        agent.m_currentState = AgentConstructor.states.SEARCHING;
        transform.name = agent.m_geneStrength.ToString();
        fitness = Random.Range(0.0f, 10.0f);
        speed = agent.m_speed;
        turnRate = agent.m_turnRate;
        turnFreq = agent.m_turnFreq;
        fieldOfView = agent.m_fieldOfView;
        viewDistance = agent.m_viewDistance;
    }

    private void Update()
    {
        energy -= Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (energy <= 0)
            return;
        switch(agent.m_currentState)
        {
            case (AgentConstructor.states.SEARCHING):
                {
                    timer += Time.deltaTime;
                    if (timer >= 5.0f)
                    {
                        if (Random.Range(0.0f, 100.0f) < agent.m_turnFreq)
                            turn = !turn;
                        timer = 4.0f;
                    }
                    if (turn)
                        transform.Rotate(0, agent.m_turnRate * Time.deltaTime, 0);
                    else
                        transform.Rotate(0, -agent.m_turnRate * Time.deltaTime, 0);
                    transform.Translate(transform.forward * agent.m_speed * Time.deltaTime, Space.World);
                    for (float a = -(agent.m_fieldOfView) / 2.0f; a < agent.m_fieldOfView / 2.0f; a += 5.0f)
                    {
                        float angle = (a + transform.eulerAngles.y) * Mathf.Deg2Rad;
                        Vector3 targetDir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                        RaycastHit hit;
                        if (Physics.Raycast(new Ray(transform.GetChild(0).position, targetDir), out hit, agent.m_viewDistance) && hungry)
                            if (hit.transform.tag == "Food")
                            {
                                agent.m_currentState = AgentConstructor.states.GOINGTOFOOD;
                                target = hit.transform.gameObject;
                                rotating = true;
                            }
                        Debug.DrawRay(transform.GetChild(0).position, targetDir * agent.m_viewDistance, rayColor);
                    }
                    break;
                }
            case (AgentConstructor.states.GOINGTOFOOD):
                {
                    if (target == null)
                    {
                        agent.m_currentState = AgentConstructor.states.SEARCHING;
                        rotating = false;
                        return;
                    }
                    Vector3 targetDir = target.transform.position - transform.position;
                    if (Vector3.Angle(transform.forward, targetDir) > 2.0f && rotating)
                    {
                        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, (agent.m_turnRate * Mathf.Deg2Rad) * Time.deltaTime, 0.0f);
                        transform.rotation = Quaternion.LookRotation(newDir);
                    }
                    else
                    {
                        rotating = false;
                        transform.Translate(transform.forward * agent.m_speed * Time.deltaTime, Space.World);
                        if ((Vector3.Angle(transform.forward, targetDir) > 15.0f))
                            rotating = true;
                    }
                    Debug.DrawRay(transform.position, targetDir, rayColor);
                    break;
                }
            case (AgentConstructor.states.EATING):
                {
                    break;
                }
            case (AgentConstructor.states.MATESEARCH):
                {
                    rayColor = Color.black;
                    timer += Time.deltaTime;
                    if (timer >= 5.0f)
                    {
                        if (Random.Range(0.0f, 100.0f) < agent.m_turnFreq)
                            turn = !turn;
                        timer = 4.0f;
                    }
                    if (turn)
                        transform.Rotate(0, agent.m_turnRate * Time.deltaTime, 0);
                    else
                        transform.Rotate(0, -agent.m_turnRate * Time.deltaTime, 0);
                    transform.Translate(transform.forward * agent.m_speed * Time.deltaTime, Space.World);
                    for (float a = -(agent.m_fieldOfView) / 2.0f; a < agent.m_fieldOfView / 2.0f; a += 5.0f)
                    {
                        float angle = (a + transform.eulerAngles.y) * Mathf.Deg2Rad;
                        Vector3 targetDir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                        RaycastHit hit;
                        if (Physics.Raycast(new Ray(transform.GetChild(0).position, targetDir), out hit, agent.m_viewDistance))
                        {
                            if (hit.transform.CompareTag("Agent") && hit.transform.GetComponent<AgentScript>().agent.m_currentState == AgentConstructor.states.MATESEARCH)
                            {
                                dad = true;
                                target = hit.transform.gameObject;
                                hit.transform.transform.GetComponent<AgentScript>().target = gameObject;
                                agent.m_currentState = AgentConstructor.states.GOINGTOMATE;
                                hit.transform.GetComponent<AgentScript>().agent.m_currentState = AgentConstructor.states.GOINGTOMATE;
                                rotating = true;
                            }
                        }
                        Debug.DrawRay(transform.GetChild(0).position, targetDir * agent.m_viewDistance, rayColor);
                    }
                    break;
                }
            case (AgentConstructor.states.GOINGTOMATE):
                {
                    Vector3 targetDir = target.transform.position - transform.position;
                    if (Vector3.Angle(transform.forward, targetDir) > 2.0f && rotating)
                    {
                        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, (agent.m_turnRate * Mathf.Deg2Rad) * Time.deltaTime, 0.0f);
                        transform.rotation = Quaternion.LookRotation(newDir);
                    }
                    else
                    {
                        rotating = false;
                        transform.Translate(transform.forward * agent.m_speed * Time.deltaTime, Space.World);
                        if ((Vector3.Angle(transform.forward, targetDir) > 15.0f))
                            rotating = true;
                    }
                    if (Vector3.Distance(transform.position, target.transform.position) < 0.8f)
                    {
                        agent.m_currentState = AgentConstructor.states.MATING;
                        target.GetComponent<AgentScript>().agent.m_currentState = AgentConstructor.states.MATING;
                    }
                    Debug.DrawRay(transform.position, targetDir, rayColor);
                    break;
                }
            case (AgentConstructor.states.MATING):
                {
                    if (!dad)
                    {
                        GeneticGameController gGC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GeneticGameController>();
                        gGC.AddParentPair(gameObject.GetComponent<AgentScript>().agent, target.GetComponent<AgentScript>().agent);
                    }
                    transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = Color.red;
                    fitness += 2.0f;
                    agent.m_currentState = AgentConstructor.states.WAITING;
                    break;
                }
            default:
                break;
        }
        CorrectRotation();
    }

    public void StartEating()
    {
        StartCoroutine(Eat(2));
    }

    IEnumerator Eat(float duration)
    {
        agent.m_currentState = AgentConstructor.states.EATING;
        hungry = false;
        foodCollected++;
        fitness += 2.0f;
        yield return new WaitForSeconds(duration);
        agent.m_currentState = AgentConstructor.states.MATESEARCH;
    }

    void CorrectRotation()
    {
        if (transform.rotation.x != 0 || transform.rotation.z != 0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if (transform.position.y != 0)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
