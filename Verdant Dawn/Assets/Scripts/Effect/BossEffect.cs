using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : RecycleObject
{
    protected override void OnReset()
    {
        DisableTimer(1.0f);
    }
}
