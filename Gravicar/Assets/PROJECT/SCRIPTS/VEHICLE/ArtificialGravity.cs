using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialGravity : MonoBehaviour
{
    [SerializeField] float gravityComponent = 10.0f;
    [SerializeField] float maxFallSpeed = 30.0f;
    [SerializeField] GameObject lastCollider;
    [SerializeField] bool isGrounded;
    Vector3 velocity;
    bool physicsBased;

    // Update is called once per frame
    void Update()
    {
        if (!isGrounded)
        {
            if (physicsBased)
            {
                if (lastCollider == null)
                    gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.up * gameObject.GetComponent<Rigidbody>().mass * gravityComponent);
                else
                    gameObject.GetComponent<Rigidbody>().AddForce(-lastCollider.transform.up * gameObject.GetComponent<Rigidbody>().mass * gravityComponent);
            }
            else
            {
                if (lastCollider == null)
                    gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), ref velocity, 1.0f, maxFallSpeed);
                else
                    gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, lastCollider.transform.position.y, gameObject.transform.position.z), ref velocity, 1.0f, maxFallSpeed);
            }
        }

    }

    public void setCollider(GameObject c)
    {
        lastCollider = c;
    }

    public void setGrounded(bool ground)
    {
        isGrounded = ground;
    }

    public void setPhysics(bool phys)
    {
        physicsBased = phys;
    }
}
