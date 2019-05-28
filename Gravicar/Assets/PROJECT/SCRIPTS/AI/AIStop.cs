using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStop : MonoBehaviour
{
    public AIStateMachine move;
    public float brakePower;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AIMove>() != null)
        {
            move.brake = true;
            move.brakeSpeed = brakePower;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AIMove>() != null)
        {
            move.brake = false;
        }
    }
}
