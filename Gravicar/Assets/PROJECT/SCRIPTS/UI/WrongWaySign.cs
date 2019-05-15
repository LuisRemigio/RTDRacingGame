using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrongWaySign : MonoBehaviour
{

    public Image wrongWaySign;
    public float pointL;
    public int currentNode = 0;
    public GameObject[] pathGroup;


    AIStateMachine aiStatemachine;
    Transform[] pathTransforms;
    int index;
    List<Transform> nodes;

    void Awake()
    {
        aiStatemachine = FindObjectOfType<AIStateMachine>();
        pointL = aiStatemachine.pointL;
        index = Random.Range(0, pathGroup.Length);
        Transform[] pathTransforms = pathGroup[index].GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != pathGroup[index].transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    void Start()
    {
        wrongWaySign.enabled = false;
    }

    
    void Update()
    {
        Transform point = nodes[currentNode].transform;
        Vector3 dir = (transform.transform.position - point.transform.position).normalized;
        float direction = Vector3.Dot(transform.forward, dir);

        if (direction > 0)
        {
            wrongWaySign.enabled = true;
        }
        else
        {
            wrongWaySign.enabled = false;
        }
    }

    void FixedUpdate()
    {
        CheckWaypointDistance();
    }

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
}
