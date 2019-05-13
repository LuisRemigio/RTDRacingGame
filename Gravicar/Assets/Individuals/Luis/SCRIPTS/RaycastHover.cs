using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHover : MonoBehaviour
{
    [SerializeField] List<Transform> hoverRaycastOrigins;
    List<Vector3> currentRaycastPositions;
    [SerializeField] float hoverHeight = 2.0f;
    Vector3 raycastDirection;
    int mask = 1 << 10;
    [SerializeField] float rayRange = 10.0f;
    Rigidbody body;
    [SerializeField] float hoverForce = 5.0f;
    [SerializeField] float stabilizeForce = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        raycastDirection = -gameObject.transform.up;
        RaycastHit hit;

        for (int i = 0; i < hoverRaycastOrigins.Count; i++)
        {
            if (Physics.Raycast(hoverRaycastOrigins[i].position, raycastDirection, out hit, rayRange, mask))
            {
                Debug.DrawRay(hoverRaycastOrigins[i].position, raycastDirection * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                if (hit.distance < rayRange && hit.distance > rayRange - .5f)
                {
                    body.AddForceAtPosition(gameObject.transform.up * body.mass * stabilizeForce, hoverRaycastOrigins[i].position);
                    Debug.Log("Stabilizing");
                }
                else
                    body.AddForceAtPosition(gameObject.transform.up * body.mass * (hoverForce * (rayRange / hit.distance)), hoverRaycastOrigins[i].position);
            }
        }
    }
}
