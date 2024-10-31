using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterClass : BaseClass
{
    /// <summary>
    /// 무기
    /// </summary>
    Weapon riple;

    /// <summary>
    /// 장비하는 코루틴 저장용(무기를 장비하는 도중에 다른 무기로 바꿀때 이전 코루틴 제거용)
    /// </summary>
    IEnumerator equipWeapon;

    public override void Enter(PlayerClass sender)
    {
        // 구르기 시간 바꾸기
        rollTime = 0.633f;

        base.Enter(sender);

        // 장비 시간 초기화
        equipTime = 0.6f;

        // 무기 가져오기
        riple = sender.weapons[3];

        // 코루틴 저장
        equipWeapon = EquipWeapon(sender);

        // 코루틴 실행
        sender.StartCoroutine(equipWeapon);
    }

    public override void Exit(PlayerClass sender)
    {
        // 코루틴 제거
        sender.StopCoroutine(equipWeapon);

        // 장비 해제하기
        riple.UnEquip(sender.gameObject);
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
        yield return new WaitForSeconds(equipTime);
        weapon?.SetActive(true);
    }
}
