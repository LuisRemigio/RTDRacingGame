using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHover : MonoBehaviour
{
    [SerializeField] List<Transform> hoverRaycastOrigins;
    Transform flipperRaycast;
    List<Vector3> currentRaycastPositions;
    [SerializeField] float hoverHeight = 8.0f;
    Vector3 raycastDirection;
    int mask = 1 << 10;
    [SerializeField] float rayRange = 10.0f;
    [SerializeField] float maxRayRange = 70.0f;
    Rigidbody body;
    [SerializeField] float hoverForce = 5.0f;
    [SerializeField] float stabilizeForce = 8.0f;
    [SerializeField] float flipForce = 25.0f;
    ArtificialGravity artGrav;
    [SerializeField] bool physicsBased = false;

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
            if (!Physics.Raycast(hoverRaycastOrigins[i].position, raycastDirection, out hit, rayRange, mask))
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
                    if (hit.distance < rayRange && hit.distance > rayRange - .5f)
                    {
                        body.AddForceAtPosition(-raycastDirection * body.mass * stabilizeForce, hoverRaycastOrigins[i].position);
                        Debug.Log("Stabilizing");
                    }
                    else
                        body.AddForceAtPosition(-raycastDirection * body.mass * (hoverForce * (rayRange / hit.distance)), hoverRaycastOrigins[i].position);
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
        if (Physics.Raycast(flipperRaycast.position, -raycastDirection, out hit, rayRange, mask))
        {
            body.AddForceAtPosition(raycastDirection * body.mass * flipForce, hoverRaycastOrigins[0].position);
        }
    }
}
