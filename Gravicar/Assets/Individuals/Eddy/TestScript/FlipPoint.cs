using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AIStateMachine>() != null)
        {
            other.GetComponent<AIStateMachine>().stateMachine.ChangeState(AIFlipState.Instance);
        }
    }
}
