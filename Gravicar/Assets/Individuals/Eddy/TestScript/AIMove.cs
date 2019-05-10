using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    public float turnSpeed;
    public float moveSpeed;
    public float maxSpeed;
    public Transform path;
    public float pointL = 10f;
    public int currentNode = 0;
    public bool brake = false;
    public float brakeSpeed;
    private float startTimer = 3.5f;
    private bool canInput = false;
    private List<Transform> nodes;

    Rigidbody rb;

    //[Header("Sensors")]
    //public float sensorLength = 3f;
    //public Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
    //public float frontSideSensorPosition = 0.2f;
    //public float frontSensorAngle = 30f;

    void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }
	
	
	void Update ()
    {
        startTimer -= Time.deltaTime;
        
        if (startTimer <= 0)
        {
            canInput = true;
        }       
    }

    void FixedUpdate()
    {
        if (canInput)
        {
            if (brake == true)
            {
                moveSpeed = brakeSpeed;
                Debug.Log("Brake");
            }
            else
            {
                moveSpeed = 15;
            }
            Transform point = nodes[currentNode].transform;
            Vector3 dir = (point.transform.position - transform.position).normalized;
            //float direction = Vector3.Dot(transform.forward, dir);

            //Debug.Log("Dot:" + direction);


            float z = moveSpeed;

            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = rotation;

            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * z, ForceMode.Force);
        }
        CheckWaypointDistance();
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < pointL)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

}
