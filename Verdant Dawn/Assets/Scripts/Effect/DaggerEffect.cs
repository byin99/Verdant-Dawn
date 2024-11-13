using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerEffect : RecycleObject
{
    protected override void OnReset()
    {
        DisableTimer(3.0f);
    }
}
