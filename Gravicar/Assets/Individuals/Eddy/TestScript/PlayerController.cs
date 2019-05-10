using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed;
    public float moveSpeed;
    public float maxSpeed;
    public float torque = 70;
    Rigidbody rb;
    public float breakMod = .98f;
    public bool canInput;
    public bool isGrounded;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        canInput = false;
    }

    
    void Update()
    {
        //other stuff
    }

    void FixedUpdate()
    {
        if (canInput == true)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;
            var z = Input.GetAxis("Vertical") * moveSpeed;


            if (isGrounded)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity = rb.velocity * breakMod;
                }
            }
            rb.AddRelativeTorque(transform.up * x * torque, ForceMode.Acceleration);
            rb.AddForce(transform.forward * z, ForceMode.Acceleration);
            if (Input.GetKeyDown(KeyCode.W))
            {
                //Engine sound
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                //Engine sound
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                //Engine sound
                //mirror bool
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                //Engine sound
                //mirror bool
            }
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }
}
