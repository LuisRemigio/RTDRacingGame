using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCheckpoint : MajorCheckpoint
{
    protected void OnTriggerEnter(Collider c)
    {
		if (v.getCheckpointsLeft() <= 1)
		{
			base.OnTriggerEnter(c);
			v.resetCheckpoints();
			v.nextLap();
		}
    }
}
