using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialGravity : MonoBehaviour
{
    [SerializeField] float gravitationalForce = 10.0f;
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.up * gameObject.GetComponent<Rigidbody>().mass * gravitationalForce);
    }
}
