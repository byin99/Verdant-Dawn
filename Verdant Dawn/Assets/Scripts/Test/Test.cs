using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        GhoulController ghoul = FindAnyObjectByType<GhoulController>();
        ghoul.enemyStateMachine.TransitionTo(ghoul.die);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        SkeletonController skeleton = FindAnyObjectByType<SkeletonController>();
        skeleton.enemyStateMachine.TransitionTo(skeleton.die);
    }
}
