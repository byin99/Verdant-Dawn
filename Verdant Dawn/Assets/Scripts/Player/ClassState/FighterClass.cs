using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterClass : BaseClass
{
    /// <summary>
    /// 왼쪽 무기
    /// </summary>
    Weapon leftFist;

    /// <summary>
    /// 오른쪽 무기
    /// </summary>
    Weapon rightFist;

    public override void Enter(PlayerClass sender)
    {
        // 구르기 시간 바꾸기
        rollTime = 0.6f;

        base.Enter(sender);

        // 무기 가져오기
        leftFist = sender.weapons[0];
        rightFist = sender.weapons[1];

        // Player에 무기 장착
        leftFist.Equip(sender.gameObject);
        rightFist.Equip(sender.gameObject);

        // Animation 부여
        animator.SetInteger(Class_Hash, 0);
    }

    public override void Exit(PlayerClass sender)
    {
        // 무기 해제
        leftFist.UnEquip(sender.gameObject);
        rightFist.UnEquip(sender.gameObject);
    }

    public override void UpdateState(PlayerClass sender)
    {
    }
}

