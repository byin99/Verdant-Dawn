using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianClass : BaseClass, IClass
{
    /// <summary>
    /// 무기
    /// </summary>
    Weapon staff;

    /// <summary>
    /// 장비하는 코루틴 저장용(무기를 장비하는 도중에 다른 무기로 바꿀때 이전 코루틴 제거용)
    /// </summary>
    IEnumerator equipWeapon;

    // 공격할 때의 시간들
    float attack1AnimTime = 1.25f;
    float attack2AnimTime = 1.167f;
    float attack3AnimTime = 1.033f;

    /// <summary>
    /// 구르기 시간
    /// </summary>
    float rollTime = 0.55f;

    /// <summary>
    /// 무기를 드는 시간
    /// </summary>
    float drawAnimTime = 1.333f;

    /// <summary>
    /// 공격 애니메이션 개수
    /// </summary>
    int attackCount = 3;

    public override void Enter(PlayerClass sender)
    {
        base.Enter(sender);

        // 애니메이션 시간들 바꾸기
        ChangeAnimTime();

        // 장비 시간 초기화
        equipTime = 0.3f;

        // 무기 가져오기
        staff = sender.weapons[4];

        // 코루틴 저장
        equipWeapon = EquipWeapon(sender);

        //코루틴 실행
        sender.StartCoroutine(equipWeapon);

        // Effect함수 연결하기
        attack.onEffect += AttackEffect;
    }

    public override void Exit(PlayerClass sender)
    {
        // 코루틴 제거
        sender.StopCoroutine(equipWeapon);

        // 장비 해제하기
        staff.UnEquip(sender.gameObject);

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
        animator.SetInteger(Class_Hash, 3);

        GameObject weapon = staff.Equip(sender.gameObject);

        weapon.SetActive(false);

        attack.canAttack = false;

        yield return new WaitForSeconds(equipTime);

        weapon.SetActive(true);

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
        attack.attackAnimTime[1] = attack2AnimTime;
        attack.attackAnimTime[2] = attack3AnimTime;
        attack.attackCount = attackCount;
        attack.attackIndex = 0;
        movement.rollAnimTime = rollTime;
    }

    /// <summary>
    /// StaffEffect 소환 함수
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    void AttackEffect(Transform attackTransform)
    {
        // 애니메이션이 방향이 계속 바뀌기 때문에 attack의 forward를 방향으로 해준다.
        Factory.Instance.GetStaffEffect(attackTransform.position, attack.transform.rotation.eulerAngles);
    }
}
