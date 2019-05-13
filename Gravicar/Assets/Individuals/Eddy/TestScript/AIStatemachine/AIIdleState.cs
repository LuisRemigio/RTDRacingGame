using UnityEngine;
using StateStuff;

public class AIIdleState : State<AIStateMachine>
{
    private static AIIdleState _instance;
    private AIIdleState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AIIdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new AIIdleState();
            }

            return _instance;
        }
    }

    public override void EnterState(AIStateMachine _owner)
    {
        Debug.Log("Entering AIIdleState State");

    }

    public override void ExitState(AIStateMachine _owner)
    {
        Debug.Log("Exiting AIIdleState State");

    }

    public override void UpdateState(AIStateMachine _owner)
    {
        _owner.startTimer -= Time.deltaTime;

        if (_owner.startTimer <= 0)
        {
            _owner.canInput = true;
            _owner.stateMachine.ChangeState(AIDriveState.Instance);
        }
    }
}
