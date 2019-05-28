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
    [SerializeField] float rayRange = 10.0f;
    [SerializeField] float feelerRange = 15.0f;
    [SerializeField] float maxRayRange = 70.0f;
    [SerializeField] List<Transform> feelerOrigins;
    [Tooltip("Should be the same size and order as feelers")]
    [SerializeField] List<Transform> hoverOrigins;
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
        controller = gameObject.GetComponent<PlayerController>();
        //feelerOrigins.Capacity = hoverOrigins.Count;
    }

    // Update is called once per frame
    void Update()
    {
        raycastDirection = -gameObject.transform.up;
        RaycastHit hit;
        // Checks if the vehicle is grounded
        for (int i = 0; i < hoverOrigins.Count; i++)
        {
            if (!Physics.Raycast(hoverOrigins[i].position, raycastDirection, out hit, rayRange + 2.0f, mask))
            {
                controller.setGrounded(false);
                artGrav.setGrounded(false);
                break;
            }
            controller.setGrounded(true);
            artGrav.setGrounded(true);
        }
        // Hovering functionality
        if (feelerOrigins.Count != hoverOrigins.Count)
        {
            Debug.Log("Feelers and Hovers don't match");
            return;
        }
        for (int i = 0; i < feelerOrigins.Count; i++)
        {
            // Checks for ground
            if (Physics.Raycast(hoverOrigins[i].position, raycastDirection, out hit, maxRayRange, mask))
            {
                artGrav.setCollider(hit.collider.gameObject.GetComponentsInChildren<Transform>()[1].gameObject);
            }
            // Checks for within hover distance
            if (Physics.Raycast(feelerOrigins[i].position, raycastDirection, out hit, feelerRange, mask))
            {
                Debug.DrawRay(hoverOrigins[i].position, raycastDirection * hit.distance, Color.yellow);
                Debug.DrawRay(feelerOrigins[i].position, raycastDirection * hit.distance, Color.green);
                if (i == 0)
                    Debug.Log("Did Hit Left");
                if (i == 1)
                    Debug.Log("Did Hit Right");
                if (i == 2)
                    Debug.Log("Did Hit Front");
                if (i == 3)
                    Debug.Log("Did Hit Back");

                // Physics-based hovering
                if (physicsBased)
                {
                    artGrav.setPhysics(true);
                    body.AddForceAtPosition(-raycastDirection * body.mass * (hoverForce * (1 - (hit.distance / rayRange))), hoverOrigins[i].position);
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
            body.AddForceAtPosition(raycastDirection * body.mass * flipForce, hoverOrigins[0].position);
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


    // Setters
    public void setHoverForce(float force)
    {
        hoverForce = force;
    }

    public void setFeelerRange(float range)
    {
        feelerRange = range;
    }

    public void setRayRange(float range)
    {
        rayRange = range;
    }

    public void setMaxRayRange(float range)
    {
        maxRayRange = range;
    }


}
