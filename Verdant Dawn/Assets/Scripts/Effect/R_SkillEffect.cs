using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_SkillEffect : RecycleObject
{
    protected override void OnReset()
    {
        DisableTimer(10.0f);
    }
}
