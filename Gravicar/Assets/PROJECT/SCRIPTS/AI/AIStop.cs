using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStop : MonoBehaviour
{
    //public AIStateMachine move;
    public float brakePower;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AIStateMachine>() != null)
        {
            other.GetComponent<AIStateMachine>().brake = true;
            other.GetComponent<AIStateMachine>().speed = brakePower;
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AIStateMachine>() != null)
        {
            other.GetComponent<AIStateMachine>().brake = false;
            other.GetComponent<AIStateMachine>().speed = 100;
        }
    }
}
