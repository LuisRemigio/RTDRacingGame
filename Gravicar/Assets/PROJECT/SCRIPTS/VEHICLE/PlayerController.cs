using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed;
    public float moveSpeed;
    public float maxSpeed;
    private float torque = 70;
    Rigidbody rb;
    public float breakMod = .98f;
    [SerializeField] float accelMod = 1.0f;
    public bool canInput;
    public bool isGrounded;
    [SerializeField] GameObject fCamera;
    [SerializeField] GameObject rCamera;

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
    [SerializeField] float lastMoveForce = 0;
    float velocity;
    float airVelocity;
    [SerializeField] float smoothTime = 1.0f;
    void FixedUpdate()
    {
        if (canInput == true)
        {
            var turnForce = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;

            //var moveForce;
            // Drag Simulation
            if (Input.GetAxis("Vertical") == 0 || !isGrounded)
                Mathf.SmoothDamp(moveForce, 0, ref velocity, smoothTime);
            else if (Input.GetAxis("Vertical") != 0)
                moveForce = Input.GetAxis("Vertical") * moveSpeed;
            if (moveForce < moveSpeed * .05f && moveForce > moveSpeed * -.05f)
                moveForce = 0;

            // Rear Camera Switch

            // Reversing based switching
            //float velAngle = Vector3.Angle(gameObject.GetComponent<Rigidbody>().velocity, gameObject.transform.forward);
            //if (Input.GetAxis("Vertical") == -1 && velAngle > 100)


            // Manual rear camera switching
            if (Input.GetAxis("Camera") == 1)
            {
                rCamera.SetActive(true);
                fCamera.SetActive(false);
            }
            else if(Input.GetAxis("Camera") == 0)
            {
                rCamera.SetActive(false);
                fCamera.SetActive(true);
            }

            // Preventing airborne acceleration
            if (isGrounded)
            {
                lastMoveForce = moveForce;
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity = rb.velocity * breakMod;
                }
            }
            //rb.AddForce(gameObject.transform.forward * moveForce, ForceMode.Acceleration);
            rb.velocity += gameObject.transform.forward * moveForce * (Time.fixedDeltaTime * accelMod);
            //else
            //{
            //    Debug.Log("Slowing Down");
            //    lastMoveForce -= lastMoveForce * .01f;
            //    Mathf.SmoothDamp(lastMoveForce, 0, ref airVelocity, 5.0f);
            //    rb.AddForce(gameObject.transform.forward * lastMoveForce, ForceMode.Acceleration);
            //}
            // Turning
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
