using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_SkillEffect2 : RecycleObject
{
    protected override void OnReset()
    {
        DisableTimer(20.0f);
    }
}
