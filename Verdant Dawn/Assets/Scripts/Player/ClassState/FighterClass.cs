using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterClass : BaseClass, IClass
{
    /// <summary>
    /// 왼쪽 무기
    /// </summary>
    Weapon leftFist;

    /// <summary>
    /// 오른쪽 무기
    /// </summary>
    Weapon rightFist;

    // 공격할 때의 시간들
    float attack1AnimTime = 0.9f;
    float attack2AnimTime = 0.833f;
    float attack3AnimTime = 1.017f;

    /// <summary>
    /// 구르기 시간
    /// </summary>
    float rollTime = 0.6f;

    /// <summary>
    /// 공격 애니메이션 개수
    /// </summary>
    int attackCount = 3;

    public override void Enter(PlayerClass sender)
    {
        base.Enter(sender);

        // 애니메이션 시간들 바꾸기
        ChangeAnimTime();

        // 무기 가져오기
        leftFist = sender.weapons[0];
        rightFist = sender.weapons[1];

        // Player에 무기 장착
        leftFist.Equip(sender.gameObject);
        rightFist.Equip(sender.gameObject);

        // Animation 부여
        animator.SetInteger(Class_Hash, 0);

        // Effect함수 연결하기
        attack.onEffect += AttackEffect;
    }

    public override void Exit(PlayerClass sender)
    {
        // 무기 해제
        leftFist.UnEquip(sender.gameObject);
        rightFist.UnEquip(sender.gameObject);

        // Effect함수 없애기
        attack.onEffect -= AttackEffect;
    }

    public override void UpdateState(PlayerClass sender)
    {
    }

    /// <summary>
    /// 클래스별로 가지는 애니메이션 시간을 바꿔주는 함수
    /// </summary>
    public void ChangeAnimTime()
    {
        attack.attackAnimTime = new float[attackCount];
        attack.attackAnimTime[0] = attack1AnimTime;
        attack.attackAnimTime[1] = attack2AnimTime;
        attack.attackAnimTime[2] = attack3AnimTime;
        attack.attackCount = attackCount;
        attack.attackIndex = 0;
        movement.rollAnimTime = rollTime;
    }

    /// <summary>
    /// FistEffect 소환 함수
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    void AttackEffect(Transform attackTransform)
    {
        Factory.Instance.GetFistEffect(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}

