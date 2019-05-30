using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    protected float firstAcrossTime = 0.0f;
    protected float distanceFromFirst = 0.0f;
    protected Vehicle v;
    // Update speed
    protected virtual void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<Vehicle>() != null)
        {
            v = c.gameObject.GetComponent<Vehicle>();
            v.updateCheckpoints(gameObject);
        }
    }
}
