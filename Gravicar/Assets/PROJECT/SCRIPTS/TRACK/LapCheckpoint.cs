using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCheckpoint : MajorCheckpoint
{
    protected override void OnTriggerEnter(Collider c)
    {
        v = c.gameObject.GetComponent<Vehicle>();
        if(v.getCurrentLap() == 0)
        {
            v.nextLap();
        }
        else if (v.getCheckpointsLeft() <= 1)
        {
            base.OnTriggerEnter(c);
            v.resetCheckpoints();
            v.nextLap();
        }
    }
}
