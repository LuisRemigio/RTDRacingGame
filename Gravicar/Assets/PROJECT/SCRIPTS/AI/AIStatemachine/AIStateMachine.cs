using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using UnityEngine.AI;

public class AIStateMachine : MonoBehaviour
{
    public StateMachine<AIStateMachine> stateMachine { get; set; }

    //public float turnSpeed;
    public float speed;
    public float moveSpeed;
    public float maxSpeed;
    public GameObject[] pathGroup;
    public float pointL;
    public int currentNode = 0;
    public bool brake = false;
    public float brakeSpeed;
    public float startTimer = 3.5f;
    public bool canInput = false;
    public bool flip = false;
    private List<Transform> nodes;

    Rigidbody rb;
    int index;
    Transform[] pathTransforms;

    void Awake()
    {
        index = Random.Range(0, pathGroup.Length);
        rb = gameObject.GetComponent<Rigidbody>();

        Transform[] pathTransforms = pathGroup[index].GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != pathGroup[index].transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        stateMachine = new StateMachine<AIStateMachine>(this);
        stateMachine.ChangeState(AIIdleState.Instance);
    }

    //void Start()
    //{
    //    
    //}

    void CheckWaypointDistance()
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

    public void Drive()
    {
        if (canInput)
        {
            if (brake == true)
            {
                moveSpeed = -brakeSpeed;
                Debug.Log("Brake");
            }
            else
            {
                moveSpeed = 100;
            }
            Transform point = nodes[currentNode].transform;
            Vector3 dir = (point.transform.position - transform.position).normalized;
            //float direction = Vector3.Dot(transform.forward, dir);

            //Debug.Log("Dot:" + direction);


            //float z = moveSpeed;

            Quaternion rotation = Quaternion.LookRotation(dir, transform.up);
            transform.rotation = rotation;

            this.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed, ForceMode.Acceleration);

            //AddForce(transform.forward * z, ForceMode.Force);
        }
        CheckWaypointDistance();
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Update()
    {
        if(flip == true)
        {
            stateMachine.ChangeState(AIFlipState.Instance);
        }


        stateMachine.Update();
    }

    void FixedUpdate()
    {
        
    }
}
