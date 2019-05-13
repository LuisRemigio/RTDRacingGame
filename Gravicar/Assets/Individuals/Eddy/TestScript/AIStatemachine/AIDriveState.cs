using UnityEngine;
using StateStuff;

public class AIDriveState : State<AIStateMachine>
{
    private static AIDriveState _instance;
    private AIDriveState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AIDriveState Instance
    {
        get
        {
            if (_instance == null)
            {
                new AIDriveState();
            }

            return _instance;
        }
    }

    public override void EnterState(AIStateMachine _owner)
    {
        Debug.Log("Entering AIDriveState State");
        
    }

    public override void ExitState(AIStateMachine _owner)
    {
        Debug.Log("Exiting AIDriveState State");

    }

    public override void UpdateState(AIStateMachine _owner)
    {
        _owner.Drive();
    }
}
