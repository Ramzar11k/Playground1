using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticGameController : MonoBehaviour
{
    public float speed = 1.0f;
    public float turnRate = 1.0f;
    public float turnFreq = 1.0f;
    public float viewField = 10.0f;
    public float viewDistance = 1.0f;

    public int papica = 50;

    public bool randomStats = false;
    public bool differentRandomStats = false;

    public int generations = 1;

    

    struct parentPair
    {
        public AgentConstructor m_parent1;
        public AgentConstructor m_parent2;

        public parentPair(AgentConstructor parent1, AgentConstructor parent2)
        {
            m_parent1 = parent1;
            m_parent2 = parent2;
        }
    };

    public GameObject agent;
    public GameObject food;

    Transform center;

    List<AgentScript> agentsScripts = new List<AgentScript>();
    List<GameObject> foodList = new List<GameObject>();

    List<parentPair> parentPairs = new List<parentPair>();

    float timer = 0.0f;


    void Start()
    {
        center = GameObject.Find("Terrain").transform;
        if (randomStats && !differentRandomStats)
        {
            speed = Random.Range(0.5f, 5.0f);
            turnRate = Random.Range(0.0f, 160.0f);
            turnFreq = Random.Range(0.0f, 100.0f);
            viewField = Random.Range(0.0f, 160.0f);
            viewDistance = Random.Range(0.0f, 5.0f);
        }
        for (float a = 0; a < Mathf.PI * 2; a += 0.08f)
        {
            GameObject newAgent = Instantiate(agent, new Vector3(Mathf.Sin(a) * 19, 0, Mathf.Cos(a) * 19), Quaternion.identity);
            AgentScript newAgentScript = newAgent.AddComponent<AgentScript>();
            if (differentRandomStats)
                newAgentScript.agent = new AgentConstructor(Random.Range(0.5f, 5.0f), Random.Range(0.0f, 160.0f), Random.Range(0.0f, 100.0f), Random.Range(0.0f, 160.0f), Random.Range(0.0f, 5.0f), 1.0f);
            else if (randomStats)
                newAgentScript.agent = new AgentConstructor(speed, turnRate, turnFreq, viewField, viewDistance, 1.0f);
            else
                newAgentScript.agent = new AgentConstructor(speed, turnRate, turnFreq, viewField, viewDistance, 1.0f);
            newAgent.transform.LookAt(center);
            agentsScripts.Add(newAgent.GetComponent<AgentScript>());
        }
        for (int i = 0; i < papica; i++)
        {
            foodList.Add(Instantiate(food, new Vector3(Random.Range(-10.0f, 10.0f), food.transform.position.y, Random.Range(-10.0f, 10.0f)), Quaternion.identity));
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 21.0f)
        {
            NewRound();
            timer = 0.0f;
        }
    }

    void NewRound()
    {
        foreach (AgentScript aS in agentsScripts)
            Destroy(aS.gameObject);
        foreach (GameObject food in foodList)
            Destroy(food.gameObject);
        foodList.Clear();
        RemoveFromList(parentPairs.Count);
        CreateNewGeneration();
        for (int i = 0; i < papica; i++)
        {
            foodList.Add(Instantiate(food, new Vector3(Random.Range(-10.0f, 10.0f), food.transform.position.y, Random.Range(-10.0f, 10.0f)), Quaternion.identity));
        }
        parentPairs.Clear();
    }
    public void AddParentPair(AgentConstructor parent1, AgentConstructor parent2)
    {
        parentPairs.Add(new parentPair(parent1, parent2));
    }

    void RemoveFromList(int nrToRemove)
    {
        SortAgentScriptList();
        for (int i = 0; i < parentPairs.Count; i++)
        {
            agentsScripts.RemoveAt(0);
        }
    }

    void SortAgentScriptList()
    {
        bool sorted = true;
        int runs = 1;
        do
        {
            sorted = true;
            for (int i = 0; i < agentsScripts.Count - runs; i++)
            {
                if (agentsScripts[i].fitness > agentsScripts[i+1].fitness)
                {
                    AgentScript temp = agentsScripts[i];
                    agentsScripts[i] = agentsScripts[i + 1];
                    agentsScripts[i + 1] = temp;
                    sorted = false;
                }
            }
            runs += 1;
        } while (!sorted);
    }
    void CreateNewGeneration()
    {
        List<float> positions = new List<float>();
        for (float a = 0; a < Mathf.PI * 2; a += 0.08f)
        {
            positions.Add(a);
        }
        ShuffleList(positions);
        for (int i = 0; i < positions.Count; i++)
        {
            int index = Random.Range(0, positions.Count);
            if (i < agentsScripts.Count)
            {
                GameObject newAgent = Instantiate(agent, new Vector3(Mathf.Sin(positions[i]) * 19, 0, Mathf.Cos(positions[i]) * 19), Quaternion.identity);
                AgentScript newScript = newAgent.AddComponent<AgentScript>();
                newScript.agent = agentsScripts[i].agent;
                if (agentsScripts[i].foodCollected != 0)
                {
                    if (newScript.agent.m_geneStrength < 200000.0f)
                        newScript.agent.m_geneStrength *= 1.02f;
                    newScript.rayColor = Color.green;
                }
                else
                {
                    if (newScript.agent.m_geneStrength > 2.0f)
                        newScript.agent.m_geneStrength *= 0.5f;
                    else
                        newScript.agent.m_geneStrength = 1.0f;
                }
                Mutate(newScript);
                newAgent.transform.LookAt(center);
                agentsScripts[i] = newAgent.GetComponent<AgentScript>();
            }
            else
            {
                GameObject newAgent = Instantiate(agent, new Vector3(Mathf.Sin(positions[i]) * 19, 0, Mathf.Cos(positions[i]) * 19), Quaternion.identity);
                AgentScript newAgentScript = newAgent.AddComponent<AgentScript>();
                newAgentScript.agent = CreateChild(parentPairs[0].m_parent1, parentPairs[0].m_parent2);
                newAgentScript.rayColor = Color.magenta;
                newAgent.transform.LookAt(center);
                agentsScripts.Add(newAgent.GetComponent<AgentScript>());
                parentPairs.RemoveAt(0);
            }
        }
        generations++;
    }
    AgentConstructor CreateChild(AgentConstructor p1, AgentConstructor p2)
    {
        float speed = (p1.m_speed * p1.m_geneStrength + p2.m_speed * p2.m_geneStrength)/(p1.m_geneStrength + p2.m_geneStrength);
        float turnRate = (p1.m_turnRate * p1.m_geneStrength + p2.m_turnRate * p2.m_geneStrength) / (p1.m_geneStrength + p2.m_geneStrength);
        float turnFreq = (p1.m_turnFreq * p1.m_geneStrength + p2.m_turnFreq * p2.m_geneStrength) / (p1.m_geneStrength + p2.m_geneStrength);
        float viewField = (p1.m_fieldOfView * p1.m_geneStrength + p2.m_fieldOfView * p2.m_geneStrength) / (p1.m_geneStrength + p2.m_geneStrength);
        float viewRange = (p1.m_viewDistance * p1.m_geneStrength + p2.m_viewDistance * p2.m_geneStrength) / (p1.m_geneStrength + p2.m_geneStrength);
        float geneStrength = ((p1.m_geneStrength + p2.m_geneStrength) / 2.0f) * 2.1f;
        if (geneStrength > 200000.0f)
            geneStrength = 200000.0f;
        AgentConstructor newAgent = new AgentConstructor(speed, turnRate, turnFreq, viewField, viewRange, geneStrength);
        return newAgent;
    }

    void ShuffleList(List<float> list)
    {
        for (int i = 0; i < 300; i++)
        {
            int index1 = Random.Range(0, list.Count - 1);
            int index2 = Random.Range(0, list.Count - 1);
            if (index1 == index2)
                continue;
            float temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }

    void Mutate(AgentScript agentScript)
    {
        if (Random.Range(0, 101) < 5)
        {
            agentScript.agent.m_speed *= Random.Range(0.85f, 1.15f);
        }
        if (Random.Range(0, 101) < 5)
        {
            agentScript.agent.m_turnRate *= Random.Range(0.85f, 1.15f);
        }
        if (Random.Range(0, 101) < 5)
        {
            agentScript.agent.m_turnFreq *= Random.Range(0.85f, 1.15f);
        }
        if (Random.Range(0, 101) < 5)
        {
            agentScript.agent.m_fieldOfView *= Random.Range(0.85f, 1.15f);
        }
        if (Random.Range(0, 101) < 5)
        {
            agentScript.agent.m_viewDistance *= Random.Range(0.85f, 1.15f);
        }
    }
}
