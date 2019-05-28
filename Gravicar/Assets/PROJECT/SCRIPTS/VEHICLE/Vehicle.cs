using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    ArtificialGravity m_artGrav;
    PlayerController m_controller;
    RaycastHover m_hover;
    int m_totalLaps = 0;
    int m_currentLap = 0;
    float m_maxSpeed = 300;
    float m_maxAcceleration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
