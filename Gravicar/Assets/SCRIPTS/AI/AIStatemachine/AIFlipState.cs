using UnityEngine;
using StateStuff;

public class AIFlipState : State<AIStateMachine>
{
    private static AIFlipState _instance;
    private AIFlipState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AIFlipState Instance
    {
        get
        {
            if (_instance == null)
            {
                new AIFlipState();
            }

            return _instance;
        }
    }

    public override void EnterState(AIStateMachine _owner)
    {
        Debug.Log("Entering AIFlipState State");

    }

    public override void ExitState(AIStateMachine _owner)
    {
        Debug.Log("Exiting AIFlipState State");
        _owner.stateMachine.ChangeState(AIDriveState.Instance);
    }

    public override void UpdateState(AIStateMachine _owner)
    {
        
    }
}
