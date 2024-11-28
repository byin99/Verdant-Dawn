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
    float attack1AnimTime = 0.9f;
    float attack2AnimTime = 0.833f;
    float attack3AnimTime = 1.017f;

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
        attack.onEffect += AttackEffect;
        attack.onCharge_Prepare += W_Skill_Prepare;
        attack.onCharge_Success += W_Skill_Success;
        attack.onCharge_Fail += W_Skill_Fail;
    }

    public override void Exit(PlayerClass sender)
    {
        // 코루틴 제거
        sender.StopCoroutine(equipWeapon);

        // 무기 해제
        leftFist.UnEquip(sender.gameObject);
        rightFist.UnEquip(sender.gameObject);

        // Effect함수 없애기
        attack.onCharge_Fail -= W_Skill_Fail;
        attack.onCharge_Success -= W_Skill_Success;
        attack.onCharge_Prepare -= W_Skill_Prepare;
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

