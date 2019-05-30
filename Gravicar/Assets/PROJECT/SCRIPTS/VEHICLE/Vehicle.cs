using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    ArtificialGravity m_artGrav;
    PlayerController m_controller;
    AIStateMachine m_AI;
    RaycastHover m_hover;
    [SerializeField] int m_totalLaps = 1;
    int m_currentLap = 0;
    int placement = 0;
    float m_maxSpeed = 300;
    Vector3 m_startPosition;
    Quaternion m_startRotation;
    [SerializeField] List<GameObject> nextCheckpoints;
    [SerializeField] List<GameObject> prevCheckpoints;
    [SerializeField] bool isPlayer = false;
    [SerializeField] GameObject vehicleCamera;

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
        m_startPosition = gameObject.transform.position;
        m_startRotation = gameObject.transform.rotation;
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
            m_controller.setMoveSpeed(m_moveSpeed);
            m_controller.setTorqueMod(m_torqueMod);
            m_controller.setBreakMod(m_breakMod);
            m_controller.setAccelerationMod(m_accelMod);
            m_controller.setMaxSpeed(m_maxSpeed);
            m_controller.setInput(true);
            SetCameras();
        }
        else
        {
            m_AI = gameObject.AddComponent(typeof(AIStateMachine)) as AIStateMachine;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentLap >= m_totalLaps)
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
        if (prevCheckpoints.Count > 0)
        {
            Transform prevCP = prevCheckpoints[prevCheckpoints.Count - 1].transform;
            gameObject.transform.SetPositionAndRotation(prevCP.position, prevCP.rotation);
        }
        else
        {
            gameObject.transform.SetPositionAndRotation(m_startPosition, m_startRotation);
        }
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_controller.setMoveForce(0);
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
                for (int j = 0; j <= i; j++)
                {
                    prevCheckpoints.Add(nextCheckpoints[0]);
                    nextCheckpoints.Remove(nextCheckpoints[0]);
                }
                break;
            }
        }
    }

    public void resetCheckpoints()
    {
        nextCheckpoints = prevCheckpoints;
        prevCheckpoints.Clear();
    }

    void SetCameras()
    {
        Instantiate(vehicleCamera, transform);
        m_controller.PlugCameras(vehicleCamera.transform.GetChild(0).gameObject, vehicleCamera.transform.GetChild(1).gameObject);
    }

    public float calculateDistance()
    {
        Vector3 nextGate = nextCheckpoints[0].transform.position;
        Vector3 position = gameObject.transform.position;
        if (prevCheckpoints.Count != 0)
        {
            Vector3 lastGate = prevCheckpoints[prevCheckpoints.Count - 1].transform.position;
            return Vector3.Magnitude(position - nextGate) / ((Vector3.Magnitude(position - lastGate) + Vector3.Magnitude(nextGate - position)) * Vector3.Magnitude(nextGate - lastGate));
        }
        else
        {
            return Vector3.Magnitude(position - nextGate) / ((Vector3.Magnitude(position - m_startPosition) + Vector3.Magnitude(nextGate - position)) * Vector3.Magnitude(nextGate - m_startPosition));
        }
    }

    public int getCheckpointsLeft()
    {
        return nextCheckpoints.Count;
    }

    public void setPlacement(int place)
    {
        placement = place;
    }

    public int getPlacement()
    {
        return placement;
    }

}
