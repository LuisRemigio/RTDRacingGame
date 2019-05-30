using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorCheckpoint : Checkpoint
{
    protected override void OnTriggerEnter(Collider c)
    {
        base.OnTriggerEnter(c);
        if (firstAcrossTime == 0)
        {
            firstAcrossTime = Time.time;
        }
        else if (c.tag == "Player")
        {
            distanceFromFirst = Time.time - firstAcrossTime;
            // Display distance from first in time here
            // Code stuff
        }
    }
}
