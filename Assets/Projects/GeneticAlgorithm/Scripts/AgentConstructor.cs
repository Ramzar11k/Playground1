using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentConstructor
{
    public float m_speed;
    public float m_turnRate;
    public float m_turnFreq;
    public float m_fieldOfView;
    public float m_viewDistance;
    public float m_geneStrength;
    public enum states { SEARCHING, GOINGTOFOOD, EATING, MATESEARCH, GOINGTOMATE, MATING, WAITING}
    public states m_currentState;

    public AgentConstructor(float speed, float turnRate, float turnFreq, float fieldOfView, float viewDistance, float geneStrength)
    {
        m_speed = speed;
        m_turnRate = turnRate;
        m_turnFreq = turnFreq;
        m_fieldOfView = fieldOfView;
        m_viewDistance = viewDistance;
        m_geneStrength = geneStrength;
        m_currentState = states.SEARCHING;
    }
}
