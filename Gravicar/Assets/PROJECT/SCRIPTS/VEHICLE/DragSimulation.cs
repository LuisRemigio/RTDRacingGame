using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSimulation : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float frictionPercentage = .2f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //if (rb.velocity != Vector3.zero)
        {
            rb.AddForce(-rb.velocity * frictionPercentage);
        }
    }
}
