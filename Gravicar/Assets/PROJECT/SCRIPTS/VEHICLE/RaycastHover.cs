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
    int mask = 1 << 10;
    [SerializeField] float rayRange = 10.0f;
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

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        flipperRaycast = gameObject.transform;
        artGrav = gameObject.GetComponent<ArtificialGravity>();
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
                artGrav.setGrounded(false);
                break;
            }
            artGrav.setGrounded(true);
        }
        // Hovering functionality
        for (int i = 0; i < hoverRaycastOrigins.Count; i++)
        {
            // Checks for ground
            if (Physics.Raycast(hoverRaycastOrigins[i].position, raycastDirection, out hit, maxRayRange, mask))
            {
                artGrav.setCollider(hit.collider);
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

        // Vehicle flip code
        if (Physics.Raycast(flipperRaycast.position, -raycastDirection, out hit, rayRange, mask))
        {
            body.AddForceAtPosition(raycastDirection * body.mass * flipForce, hoverRaycastOrigins[0].position);
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

}
