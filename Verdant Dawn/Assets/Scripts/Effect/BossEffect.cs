using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : RecycleObject
{
    public float disableTime;

    protected override void OnReset()
    {
        DisableTimer(disableTime);
    }
}
