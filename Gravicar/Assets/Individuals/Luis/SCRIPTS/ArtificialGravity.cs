using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialGravity : MonoBehaviour
{
    [SerializeField] float gravitationalForce = 10.0f;
    [SerializeField] Collider lastCollider;
    // Update is called once per frame
    void Update()
    {
        if (lastCollider == null)
            gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.up * gameObject.GetComponent<Rigidbody>().mass * gravitationalForce);
        else
            gameObject.GetComponent<Rigidbody>().AddForce(-lastCollider.transform.up * gameObject.GetComponent<Rigidbody>().mass * gravitationalForce);

    }

    public void setCollider(Collider c)
    {
        lastCollider = c;
    }
}
