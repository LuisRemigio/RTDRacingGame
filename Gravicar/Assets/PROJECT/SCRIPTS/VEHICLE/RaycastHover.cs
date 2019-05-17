using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHover : MonoBehaviour
{
    Transform flipperRaycast;
    List<Vector3> currentRaycastPositions;
    Vector3 raycastDirection;
    Rigidbody body;
    ArtificialGravity artGrav;
    PlayerController controller;
    int mask = 1 << 10;
	bool hooked;
	RaycastHit hookHit;
	float m_transitionTimer = 0;
    [SerializeField] float rayRange = 10.0f;
	[SerializeField] float transitionTime = 0.0001f;
	[SerializeField] float maxRayRange = 70.0f;
    [SerializeField] List<Transform> hoverRaycastOrigins;
    [SerializeField] Transform centerOfVehicle;
    [SerializeField] float hoverHeight = 8.0f;
    [SerializeField] bool physicsBased = false;
    [SerializeField] float stabilizingRange = .5f;
    [SerializeField] float hoverForce = 5.0f;
    [SerializeField] float stabilizeForce = 8.0f;
    [SerializeField] float clampingSpeed = 2.0f;
    [SerializeField] float flipForce = 25.0f;
	[SerializeField] float distanceOffGround = 20.0f;
	Vector3 lastUp;


	Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        flipperRaycast = gameObject.transform;
        artGrav = gameObject.GetComponent<ArtificialGravity>();
        controller = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        raycastDirection = -gameObject.transform.up;
        RaycastHit hit;
        // Checks if the vehicle is grounded
        for (int i = 0; i < hoverRaycastOrigins.Count; i++)
        {
            if (!Physics.Raycast(hoverRaycastOrigins[i].position, raycastDirection, out hit, rayRange + 2.0f, mask))
            {
                controller.setGrounded(false);
                artGrav.setGrounded(false);
                break;
            }
			Debug.Log("Grounded");
            controller.setGrounded(true);
            artGrav.setGrounded(true);
        }
        // Hovering functionality
        for (int i = 0; i < hoverRaycastOrigins.Count; i++)
        {
            // Checks for ground
            if (Physics.Raycast(hoverRaycastOrigins[i].position, raycastDirection, out hit, maxRayRange, mask))
            {
                artGrav.setCollider(hit.collider.gameObject.GetComponentsInChildren<Transform>()[1].gameObject);
            }
            // Checks for within hover distance
            if (Physics.Raycast(hoverRaycastOrigins[i].position, raycastDirection, out hit, rayRange, mask))
            {
                Debug.DrawRay(hoverRaycastOrigins[i].position, raycastDirection * hit.distance, Color.yellow);
                Debug.Log("Did Hit");

                // Physics-based hovering
                if (physicsBased)
                {
                    artGrav.setPhysics(true);
                    body.AddForceAtPosition(-raycastDirection * body.mass * (hoverForce * (1 - (hit.distance / rayRange))), hoverRaycastOrigins[i].position);
                    //if (hit.distance < rayRange && hit.distance > rayRange - .5f)
                    //{
                    //    body.AddForceAtPosition(-raycastDirection * body.mass * stabilizeForce, hoverRaycastOrigins[i].position);
                    //    Debug.Log("Stabilizing");
                    //}

                }
                // Distance-based hovering
                else
                {
                    artGrav.setPhysics(false);
                    //gameObject.transform.SetPositionAndRotation(
                    //    Vector3.SmoothDamp(
                    //        gameObject.transform.position,
                    //        hit.point + hit.collider.transform.up * (hoverHeight * .8f),
                    //        ref velocity,
                    //        1.0f,
                    //        10.0f
                    //        ),
                    //    Quaternion.Lerp(
                    //        gameObject.transform.rotation,
                    //        new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, 1),
                    //        Time.deltaTime
                    //        )
                    //    );
                    gameObject.transform.position =
                        Vector3.SmoothDamp(
                            gameObject.transform.position,
                            hit.point + hit.collider.transform.up * (hoverHeight * .8f),
                            ref velocity,
                            1.0f,
                            10.0f
                        );
                }

            }
        }

        // Stabilization code
        if (Physics.Raycast(centerOfVehicle.position, raycastDirection, out hit, rayRange, mask))
        {
            Debug.Log("Attempt to Clamp");
            ClampPosition(body, hit.collider.gameObject, hit.point, rayRange, stabilizingRange);
        }

        // Vehicle needs to be flipped flip code
        if (Physics.Raycast(flipperRaycast.position, -raycastDirection, out hit, rayRange, mask))
        {
            body.AddForceAtPosition(raycastDirection * body.mass * flipForce, hoverRaycastOrigins[0].position);
        }
		// Hook flipping code
		if (hooked)
		{
			m_transitionTimer += Time.deltaTime;
			if (m_transitionTimer < transitionTime)
			{
				Debug.Log("Hooked, transitioning.");
				//gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
				gameObject.GetComponent<ArtificialGravity>().enabled = false;
				gameObject.transform.up = Vector3.SmoothDamp(lastUp, hookHit.normal, ref velocity, transitionTime);
				gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, hookHit.point, ref velocity, transitionTime);
			}
			else
			{
				Debug.Log("Unhooked.");
				hooked = false;
				m_transitionTimer = 0.0f;
			}
		}
	}

    // Function to enforce vehicle stabilization
    void ClampPosition(Rigidbody floater, GameObject surface, Vector3 rayHitPoint, float clampHeight, float clampRange)
    {
        if (floater.velocity.y < clampingSpeed)
        {
            float difference = floater.position.magnitude - surface.transform.position.magnitude;
            if (difference > surface.transform.up.magnitude * (clampHeight - clampRange)
                && difference < surface.transform.up.magnitude * (clampHeight + clampRange))
            {
                floater.position = rayHitPoint + surface.transform.up * (clampHeight * .8f);
                floater.velocity = Vector3.zero;
                Debug.Log("Clamped");
            }
        }
    }

	public void GoToHook(Vector3 pos, GameObject newTrack)
	{
		Debug.Log("Hit track");
		gameObject.transform.position = pos;
		gameObject.transform.rotation = newTrack.transform.rotation;
	}

	//Vector3 velocity;
	public void GoToHook(RaycastHit raycastHit)
	{
		//gameObject.transform.position = raycastHit.point;
		hooked = true;
		hookHit = raycastHit;
		lastUp = gameObject.transform.up;
		//gameObject.transform.up = raycastHit.normal;
		//gameObject.transform.position = raycastHit.point + raycastHit.normal * distanceOffGround;
		//..transform.forward = endForward;
		//gameObject.transform.Rotate(transform.up, 90);
	}
}
