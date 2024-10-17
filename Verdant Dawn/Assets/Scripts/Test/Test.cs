using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        EnemyController ghoul = FindAnyObjectByType<EnemyController>();
        ghoul.enemyStateMachine.TransitionTo(ghoul.die);
    }
}
