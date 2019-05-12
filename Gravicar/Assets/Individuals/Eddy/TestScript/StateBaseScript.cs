using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using UnityEngine.AI;

public class StateBaseScript : MonoBehaviour
{

    public StateMachine<StateBaseScript> stateMachine { get; set; }

    void Awake()
    {
        stateMachine = new StateMachine<StateBaseScript>(this);
        stateMachine.ChangeState(State.Instance);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        stateMachine.Update();
    }
}
