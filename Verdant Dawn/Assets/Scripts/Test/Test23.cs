using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test23 : TestBase
{
    Player player;
    PlayerAttack attack;
    PlayerStatus status;

    protected override void Awake()
    {
        base.Awake();
        player = GameManager.Instance.Player;
        attack = GameManager.Instance.PlayerAttack; 
        status = GameManager.Instance.PlayerStatus;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        attack.test1();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.GetGhoul(new Vector3(20, 0, 0), Vector3.zero);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        status.IdentityGauge = 100.0f;
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
    }
}
