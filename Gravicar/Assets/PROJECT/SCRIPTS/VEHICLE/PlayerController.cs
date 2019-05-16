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
        //canInput = false;
    }


    void Update()
    {
        //other stuff
    }

    [SerializeField] float moveForce = 0;
    float velocity;
    [SerializeField] float smoothTime = 1.0f;
    void FixedUpdate()
    {
        if (canInput == true)
        {
            var turnForce = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;

            //var moveForce;
            if (Input.GetAxis("Vertical") != 0)
                moveForce = Input.GetAxis("Vertical") * moveSpeed;
            else
                Mathf.SmoothDamp(moveForce, 0, ref velocity, smoothTime);
            if (moveForce < moveSpeed * .05f)
                moveForce = 0;
            if (isGrounded)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity = rb.velocity * breakMod;
                }
            }
            rb.AddForce(gameObject.transform.forward * moveForce, ForceMode.Acceleration);
            rb.AddTorque(gameObject.transform.up * turnForce * torque, ForceMode.Acceleration);
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

    public void setGrounded(bool grounded)
    {
        isGrounded = grounded;
    }
}
