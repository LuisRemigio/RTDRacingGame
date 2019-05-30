using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float turnSpeed;
    float moveSpeed;
    float maxSpeed;
    float torque = 70;
    float breakMod = .98f;
    float accelMod = 1.0f;
    float torqueMod = 1.0f;
    float resetTime = 0.0f;
	//float tempMoveSpeed;
	//bool isReset = false;
	[SerializeField] float resetTimeLimit = 5.0f;
	[SerializeField] Vehicle vehicle;

    public bool canInput;
    public bool isGrounded;
    Rigidbody rb;
    [SerializeField] GameObject fCamera;
    [SerializeField] GameObject rCamera;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        //canInput = false;
        torque *= torqueMod;
		vehicle = this.gameObject.GetComponent<Vehicle>();

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

			//if (isReset)
			//{
			//	isReset = false;
			//	moveSpeed = tempMoveSpeed;
			//}

            // Preventing airborne acceleration
            if (isGrounded)
            {
                lastMoveForce = moveForce;
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity *= breakMod;
                }
            }
			else
			{
				resettingVehicle();
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

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

	private void resettingVehicle()
	{
		//if the player is not grounded, increment the resetTime by time.deltaTime
		resetTime += Time.deltaTime;
		if (resetTime > resetTimeLimit)
		{
			resetTime = 0f;
			vehicle.resetVehicle();
		}
	}

	public void setGrounded(bool grounded)
	{
		isGrounded = grounded;
	}

	public void setTurnSpeed(float speed)
    {
        turnSpeed = speed;
    }

    public void setTorqueMod(float modifier)
    {
        torqueMod = modifier;
	}

	public void setMoveSpeed(float speed)
	{
		moveSpeed = speed;
	}

	public void setMoveForce(float force)
	{
		moveForce = force;
	}

	public void setMaxSpeed(float max)
    {
        maxSpeed = max;
    }

    public void setBreakMod(float modifier)
    {
        breakMod = modifier;
    }

    public void setAccelerationMod(float modifier)
    {
        accelMod = modifier;
    }

    public void setInput(bool _canInput)
    {
        canInput = _canInput;
    }

    public void PlugCameras(GameObject frontCam, GameObject rearCam)
    {
        fCamera = frontCam;
        rCamera = rearCam;
    }
}
