using UnityEngine;
using StateStuff;

public class State : State<StateBaseScript>
{
    private static State _instance;
    private State()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static State Instance
    {
        get
        {
            if (_instance == null)
            {
                new State();
            }

            return _instance;
        }
    }

    public override void EnterState(StateBaseScript _owner)
    {
        Debug.Log("Entering this State");
        
    }

    public override void ExitState(StateBaseScript _owner)
    {
        Debug.Log("Exiting this State");
        
    }

    public override void UpdateState(StateBaseScript _owner)
    {
        
    }
}
