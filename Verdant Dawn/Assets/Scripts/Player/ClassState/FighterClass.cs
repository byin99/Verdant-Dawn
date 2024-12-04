using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    float attackAnimTime1 = 0.9f;
    float attackAnimTime2 = 0.833f;
    float attackAnimTime3 = 1.017f;

    // 콤보 공격할 때의 시간들
    float comboAnimTime1 = 1.033f;
    float comboAnimTime2 = 1.167f;
    float comboAnimTime3 = 1.967f;

    /// <summary>
    /// 구르기 시간
    /// </summary>
    float rollTime = 0.6f;

    /// <summary>
    /// 무기를 드는 시간
    /// </summary>
    float drawAnimTime = 1.0f;

    /// <summary>
    /// 공격 애니메이션 개수
    /// </summary>
    int attackCount = 3;

    /// <summary>
    /// 콤보 공격 애니메이션 개수
    /// </summary>
    int comboCount = 3;

    public override void Enter(PlayerClass sender)
    {
        base.Enter(sender);

        // 애니메이션 시간들 바꾸기
        ChangeAnimTime();

        // 무기 가져오기
        leftFist = sender.weapons[0];
        rightFist = sender.weapons[1];

        // 코루틴 저장
        equipWeapon = EquipWeapon(sender);

        // 코루틴 실행
        sender.StartCoroutine(equipWeapon);

        // Effect함수 연결하기
        attack.onAttack += AttackEffect;
        attack.onCharge_Prepare += W_Skill_Prepare;
        attack.onCharge_Success += W_Skill_Success;
        attack.onCharge_Fail += W_Skill_Fail;
        attack.comboEffect1 += E_Skill1;
        attack.comboEffect2 += E_Skill2;
        attack.comboEffect3 += E_Skill3;
    }

    public override void Exit(PlayerClass sender)
    {
        // 코루틴 제거
        sender.StopCoroutine(equipWeapon);

        // 무기 해제
        leftFist.UnEquip(sender.gameObject);
        rightFist.UnEquip(sender.gameObject);

        // Effect함수 없애기
        attack.comboEffect3 -= E_Skill3;
        attack.comboEffect2 -= E_Skill2;
        attack.comboEffect1 -= E_Skill1;
        attack.onCharge_Fail -= W_Skill_Fail;
        attack.onCharge_Success -= W_Skill_Success;
        attack.onCharge_Prepare -= W_Skill_Prepare;
        attack.onAttack -= AttackEffect;
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
        attack.attackAnimTime[0] = attackAnimTime1;
        attack.attackAnimTime[1] = attackAnimTime2;
        attack.attackAnimTime[2] = attackAnimTime3;
        attack.attackCount = attackCount;
        attack.attackIndex = 0;

        attack.canChargeRotate = true;

        attack.comboAnimTime = new float[comboCount];
        attack.comboAnimTime[0] = comboAnimTime1;
        attack.comboAnimTime[1] = comboAnimTime2;
        attack.comboAnimTime[2] = comboAnimTime3;
        attack.comboCount = comboCount;

        attack.returnTime = 0.0f;

        movement.rollAnimTime = rollTime;
    }

    /// <summary>
    /// FistEffect 소환 함수
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void AttackEffect(Transform attackTransform)
    {
        Factory.Instance.GetFistEffect(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(준비)
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void W_Skill_Prepare(Transform attackTransform)
    {
        w_SkillEffect = Factory.Instance.GetFighterWSkill_Prepare(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(성공)
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void W_Skill_Success(Transform attackTransform)
    {
        w_SkillEffect.gameObject.SetActive(false);
        Factory.Instance.GetFighterWSkill_Success(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(실패)
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void W_Skill_Fail(Transform attackTransform)
    {
        w_SkillEffect.gameObject.SetActive(false);
        Factory.Instance.GetFighterWSkill_Fail(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// E스킬1 이펙트
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    void E_Skill1(Transform attackTransform)
    {
        Factory.Instance.GetFighterESkill1(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// E스킬2 이펙트
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    void E_Skill2(Transform attackTransform)
    {
        Factory.Instance.GetFighterESkill2(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// E스킬3 이펙트
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    void E_Skill3(Transform attackTransform)
    {
        Factory.Instance.GetFighterESkill3(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// 무기를 장착하는 코루틴(무기가 애니메이션에 맞게 보여지기 위함)
    /// </summary>
    /// <param name="sender">장착할 플레이어</param>
    public IEnumerator EquipWeapon(PlayerClass sender)
    {
        animator.SetInteger(Class_Hash, 0);

        leftFist.Equip(sender.gameObject);
        rightFist.Equip(sender.gameObject);

        sender.isChange = true;

        yield return new WaitForSeconds(drawAnimTime);

        sender.isChange = false;
    }
}

