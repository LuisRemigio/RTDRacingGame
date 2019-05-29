using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    ArtificialGravity m_artGrav;
    PlayerController m_controller;
    AIStateMachine m_AI;
    RaycastHover m_hover;
    int m_totalLaps = 0;
    int m_currentLap = 0;
    float m_maxSpeed = 300;
	[SerializeField] List<GameObject> nextCheckpoints;
	[SerializeField] List<GameObject> prevCheckpoints;
	bool isPlayer = false;

    // Serialized Fields
    [Tooltip("At least a size of 4 (Left, Right, Front, Back)")]
    [SerializeField]
    List<Transform> feelerOrigins;
    [Tooltip("Size should match number of Feelers")]
    [SerializeField]
    List<Transform> hoverOrigins;

    // ArtGrav
    [SerializeField] float m_gravComponent = 1.0f;

    // Hover
    [SerializeField] Transform CenterOfVehicle;
    [SerializeField] float m_rayRange = 8.0f;
    [SerializeField] float m_maxRayRange = 15.0f;
    [SerializeField] float m_stabilizingRange = .5f;
    [SerializeField] float m_hoverForce = 8.0f;
    [SerializeField] float m_stabilizeForce = 10.0f;
    [SerializeField] float m_clampingSpeed = 2.0f;
    [SerializeField] float m_flipForce = 200.0f;

    // Player Controller
    [SerializeField] float m_turnSpeed = 7.0f;
    [SerializeField] float m_moveSpeed = 250.0f;
    [SerializeField] float m_torqueMod = 1.0f;
    [SerializeField] float m_breakMod = .98f;
    [SerializeField] float m_accelMod = 1.0f;
    [SerializeField] bool canInput = true;


    // Start is called before the first frame update
    void Start()
    {
        CenterOfVehicle = gameObject.transform;
        // Artificial gravity creation
        m_artGrav = gameObject.AddComponent(typeof(ArtificialGravity)) as ArtificialGravity;
        m_artGrav.setComponent(m_gravComponent);

        // Hovering functionality
        m_hover = gameObject.AddComponent(typeof(RaycastHover)) as RaycastHover;
        m_hover.setFeelers(feelerOrigins);
        m_hover.setHovers(hoverOrigins);
        m_hover.setCenter(CenterOfVehicle);
        m_hover.setRayRange(m_rayRange);
        m_hover.setFeelerRange(m_rayRange * 2.0f);
        m_hover.setMaxRayRange(m_maxRayRange);
        m_hover.setStabilizationRange(m_stabilizingRange);
        m_hover.setHoverForce(m_hoverForce);
        m_hover.setStabilizationForce(m_stabilizeForce);
        m_hover.setClampingSpeed(m_clampingSpeed);
        m_hover.setFlipForce(m_flipForce);

        // Player Controller creation
        if (isPlayer)
        {
            m_controller = gameObject.AddComponent(typeof(PlayerController)) as PlayerController;
            m_controller.setTurnSpeed(m_turnSpeed);
            m_controller.setTorqueMod(m_torqueMod);
            m_controller.setBreakMod(m_breakMod);
            m_controller.setAccelerationMod(m_accelMod);
            m_controller.setMaxSpeed(m_maxSpeed);
            m_controller.setInput(true);
        }
        else
        {
            m_AI = gameObject.AddComponent(typeof(AIStateMachine)) as AIStateMachine;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentLap > m_totalLaps)
        {
            m_controller.setInput(false);
        }
    }

    public void nextLap()
    {
        m_currentLap++;
    }

    public void setLaps(int laps)
    {
        m_totalLaps = laps;
    }

    public void setPlayer(bool whatIs)
    {
        isPlayer = whatIs;
    }

	public void resetVehicle()
	{
		GameObject prevCP = prevCheckpoints[prevCheckpoints.Count - 1];
		gameObject.transform.SetPositionAndRotation(prevCP.transform.position, prevCP.transform.rotation);
	}

	public GameObject getNextCheckpoint()
	{
		return nextCheckpoints[0];
	}

    public void updateCheckpoints(GameObject checkpoint)
    {
        for (int i = 0; i < nextCheckpoints.Count; i++)
        {
            if (nextCheckpoints[i] == checkpoint)
            {
                for (int j = 0; j < i; j++)
                {
                    prevCheckpoints.Add(nextCheckpoints[j]);
                }
                break;
            }
        }
    }

    public void resetCheckpoints()
    {
        nextCheckpoints = prevCheckpoints;
    }
}
