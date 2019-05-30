using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCheckpoint : MajorCheckpoint
{
    protected void OnTriggerEnter(Collider c)
    {
        base.OnTriggerEnter(c);
        v.resetCheckpoints();
        v.nextLap();
    }
}
