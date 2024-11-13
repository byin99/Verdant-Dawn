using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterClass : BaseClass, IClass
{
    /// <summary>
    /// 무기
    /// </summary>
    Weapon riple;

    /// <summary>
    /// 장비하는 코루틴 저장용(무기를 장비하는 도중에 다른 무기로 바꿀때 이전 코루틴 제거용)
    /// </summary>
    IEnumerator equipWeapon;

    // 공격할 때의 시간들
    float attack1AnimTime = 0.45f;

    /// <summary>
    /// 구르기 시간
    /// </summary>
    float rollTime = 0.633f;

    /// <summary>
    /// 무기를 드는 시간
    /// </summary>
    float drawAnimTime = 2.167f;

    /// <summary>
    /// 공격 애니메이션 개수
    /// </summary>
    int attackCount = 1;

    public override void Enter(PlayerClass sender)
    {
        base.Enter(sender);

        // 애니메이션 시간들 바꾸기
        ChangeAnimTime();

        // 장비 시간 초기화
        equipTime = 0.6f;

        // 무기 가져오기
        riple = sender.weapons[3];

        // 코루틴 저장
        equipWeapon = EquipWeapon(sender);

        // 코루틴 실행
        sender.StartCoroutine(equipWeapon);

        // Effect함수 연결하기
        attack.onEffect += AttackEffect;
    }

    public override void Exit(PlayerClass sender)
    {
        // 코루틴 제거
        sender.StopCoroutine(equipWeapon);

        // 장비 해제하기
        riple.UnEquip(sender.gameObject);

        // Effect함수 없애기
        attack.onEffect -= AttackEffect;
    }

    public override void UpdateState(PlayerClass sender)
    {
    }

    /// <summary>
    /// 무기를 장착하는 코루틴(무기가 애니메이션에 맞게 보여지기 위함)
    /// </summary>
    /// <param name="sender">장착할 플레이어</param>
    IEnumerator EquipWeapon(PlayerClass sender)
    {
        animator.SetInteger(Class_Hash, 2);

        GameObject weapon = riple.Equip(sender.gameObject);

        weapon?.SetActive(false);

        attack.canAttack = false;

        yield return new WaitForSeconds(equipTime);

        weapon?.SetActive(true);

        yield return new WaitForSeconds(drawAnimTime - equipTime);

        attack.canAttack = true;
    }

    /// <summary>
    /// 클래스별로 가지는 애니메이션 시간을 바꿔주는 함수
    /// </summary>
    public void ChangeAnimTime()
    {
        attack.attackAnimTime = new float[attackCount];
        attack.attackAnimTime[0] = attack1AnimTime;
        attack.attackCount = attackCount;
        attack.attackIndex = 0;
        movement.rollAnimTime = rollTime;
    }

    /// <summary>
    /// RipleEffect 소환 함수
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    void AttackEffect(Transform attackTransform)
    {
        Factory.Instance.GetRipleEffect(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
