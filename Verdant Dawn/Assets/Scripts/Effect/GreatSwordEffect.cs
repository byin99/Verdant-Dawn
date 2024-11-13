using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSwordEffect : RecycleObject
{
    protected override void OnReset()
    {
        DisableTimer(3.0f);
    }
}
