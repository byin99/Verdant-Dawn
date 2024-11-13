using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipleEffect : RecycleObject
{
    protected override void OnReset()
    {
        DisableTimer(5.0f);
    }
}
