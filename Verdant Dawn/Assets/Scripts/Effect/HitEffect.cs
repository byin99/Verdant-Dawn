using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : RecycleObject
{
    protected override void OnReset()
    {
        DisableTimer(1.0f);
    }
}
