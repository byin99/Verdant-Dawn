using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController), typeof(PlayerMovement), typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerClass))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// 공격이 가능함을 알리는 프로퍼티(true면 공격 가능함)
    /// </summary>
    public bool CanAttack => (!movement.isRoll && !playerClass.isChange && !attack.isUseSkill && !attack.isCombo);

    /// <summary>
    /// 움직일 수 있음을 알리는 프로퍼티(true면 움직일 수 있음)
    /// </summary>
    public bool CanMove => (!movement.isRoll && !attack.isAttack && !attack.isUseSkill && !attack.isCombo);

    /// <summary>
    /// Class를 바꿀 수 있음을 알리는 프로퍼티(true면 바꿀 수 있음)
    /// </summary>
    public bool CanChange => (!attack.isAttack && !attack.isUseSkill && !attack.isCombo);

    /// <summary>
    /// 스킬을 사용할 수 있음을 알리는 프로퍼티(true면 사용할 수 있음)
    /// </summary>
    public bool CanUseSkill => (!attack.isAttack && !movement.isRoll && !playerClass.isChange && !attack.isUseSkill);

    /// <summary>
    /// 구르기를 사용할 수 있음을 알리는 프로퍼티(true면 사용할 수 있음)
    /// </summary>
    public bool CanRoll => (!attack.isUseSkill);

    // 플레이어 컴포넌트들
    PlayerInputController inputcontroller;
    PlayerMovement movement;
    PlayerAttack attack;
    PlayerClass playerClass;

    private void Awake()
    {
        inputcontroller = GetComponent<PlayerInputController>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        playerClass = GetComponent<PlayerClass>();

        inputcontroller.onMove += movement.SetDestination;
        inputcontroller.onRoll += movement.Roll;
        inputcontroller.onAttack += attack.Attack;
        inputcontroller.onChargingSkill += attack.StartCharge;
        inputcontroller.offChargingSkill += attack.FinishCharge;
        inputcontroller.onComboSkill += attack.StartCombo;
        inputcontroller.offComboSkill += attack.FinishCombo;
        movement.onRoll += attack.CancelCombo;
        inputcontroller.onUltimateSkill += attack.StartUltimateSkill;
        inputcontroller.offUltimateSkill += attack.FinishUltimateSkill;
    }
}
