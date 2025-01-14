using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController), typeof(PlayerMovement), typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerClass), typeof(PlayerStatus))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// 공격이 가능함을 알리는 프로퍼티(true면 공격 가능함)
    /// </summary>
    public bool CanAttack => (status.IsAlive && !movement.isRoll && !playerClass.isChange && !attack.isUseSkill && !attack.isCombo && !attack.isUltimate);

    /// <summary>
    /// 움직일 수 있음을 알리는 프로퍼티(true면 움직일 수 있음)
    /// </summary>
    public bool CanMove => (status.IsAlive && !movement.isRoll && !attack.isAttack && !attack.isUseSkill && !attack.isCombo && !attack.isUltimate && !status.isHit);

    /// <summary>
    /// Class를 바꿀 수 있음을 알리는 프로퍼티(true면 바꿀 수 있음)
    /// </summary>
    public bool CanChange => (status.IsAlive && !attack.isAttack && !attack.isUseSkill && !attack.isCombo && !attack.isUltimate && !status.isHit && !status.isIdentity);

    /// <summary>
    /// 스킬을 사용할 수 있음을 알리는 프로퍼티(true면 사용할 수 있음)
    /// </summary>
    public bool CanUseSkill => (status.IsAlive && !attack.isAttack && !movement.isRoll && !playerClass.isChange && !attack.isUseSkill && !attack.isUltimate && !status.isHit);

    /// <summary>
    /// 구르기를 사용할 수 있음을 알리는 프로퍼티(true면 사용할 수 있음)
    /// </summary>
    public bool CanRoll => (status.IsAlive && !attack.isUseSkill && !attack.isUltimate && !status.isHit);

    /// <summary>
    /// 넉백을 당할 수 있음을 알리는 프로퍼티(true면 당할 수 있음)
    /// </summary>
    public bool CanKnockBack => (status.IsAlive && !movement.isRoll && !attack.isUltimate);

    /// <summary>
    /// 플레이어의 마나를 보여주는 프로퍼티
    /// </summary>
    public float ManaPoint => status.MP;

    // 플레이어 컴포넌트들
    PlayerInputController inputcontroller;
    PlayerMovement movement;
    PlayerAttack attack;
    PlayerClass playerClass;
    PlayerStatus status;

    private void Awake()
    {
        inputcontroller = GetComponent<PlayerInputController>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        playerClass = GetComponent<PlayerClass>();
        status = GetComponent<PlayerStatus>();

        inputcontroller.onMove += movement.SetDestination;
        inputcontroller.onRoll += movement.Roll;
        inputcontroller.onAttack += attack.Attack;
        inputcontroller.onIdentitySkill += status.IdentitySkill;
        inputcontroller.onChargingSkill += attack.StartCharge;
        inputcontroller.offChargingSkill += attack.FinishCharge;
        inputcontroller.onComboSkill += attack.StartCombo;
        inputcontroller.offComboSkill += attack.FinishCombo;
        inputcontroller.onUltimateSkill += attack.StartUltimateSkill;
        inputcontroller.offUltimateSkill += attack.FinishUltimateSkill;

        movement.onRoll += attack.CancelCombo;

        playerClass.onChangeClass += status.ChangeClassStatus;

        status.onKnockBack += attack.CancelSkill;
    }

    /// <summary>
    /// 마나를 바꾸는 함수
    /// </summary>
    /// <param name="mana">바꾸는 마나</param>
    public void ManaChange(float mana)
    {
        status.ManaChange(mana);
    }
}
