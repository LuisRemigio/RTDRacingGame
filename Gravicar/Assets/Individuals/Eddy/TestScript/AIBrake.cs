using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrake : MonoBehaviour
{
    public AIMove move;
    public float brakePower;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AIMove>() != null)
        {
            move.brake = true;
            move.brakeSpeed = brakePower;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<AIMove>() != null)
        {
            move.brake = false;
        }
    }
}
