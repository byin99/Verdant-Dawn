using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test23 : TestBase
{
    Player player;
    PlayerAttack attack;
    PlayerStatus status;
    BossController boss;
    EnemyStatus enemy;
    BossHPUI bossHPUI;

    protected override void Awake()
    {
        base.Awake();
        player = GameManager.Instance.Player;
        attack = GameManager.Instance.PlayerAttack; 
        status = GameManager.Instance.PlayerStatus;
        bossHPUI = UIManager.Instance.BossHPUI;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        attack.test1();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        boss = Factory.Instance.GetDemonLord(new Vector3(20, 0, 0));
        bossHPUI.bossStatus = boss.GetComponent<BossStatus>();
        bossHPUI.ShowBossHPBarUI();    
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        status.ExperiencePoint += 1000000;
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEvilWatcher(new Vector3(20, 0, 0));
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
    }
}
