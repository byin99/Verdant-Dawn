using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerClass : BaseClass, IClass
{ 
    /// <summary>
    /// 무기
    /// </summary>
    Weapon greatSword;

    // 공격할 때의 시간들
    float attackAnimTime1 = 1.4f;
    float attackAnimTime2 = 1.0f;
    float attackAnimTime3 = 1.333f;

    // 콤보 공격할 때의 시간들
    float comboAnimTime1 = 1.233f;
    float comboAnimTime2 = 1.067f;
    float comboAnimTime3 = 1.000f;
    float comboAnimTime4 = 1.333f;

    /// <summary>
    /// 궁극기 시간
    /// </summary>
    float ultimateAnimTime = 6.433f;

    /// <summary>
    /// 구르기 시간
    /// </summary>
    float rollTime = 1.0f;

    /// <summary>
    /// 무기를 드는 시간
    /// </summary>
    float drawAnimTime = 2.0f;

    /// <summary>
    /// 공격 애니메이션 개수
    /// </summary>
    int attackCount = 3;

    /// <summary>
    /// 콤보 공격 애니메이션 개수
    /// </summary>
    int comboCount = 4;

    /// <summary>
    /// 궁극기 이펙트
    /// </summary>
    R_SkillEffect r_SkillEffect;

    public override void Enter(PlayerClass sender)
    {
        base.Enter(sender);

        // 애니메이션 시간들 바꾸기
        ChangeAnimTime();

        // 장비 시간 초기화
        equipTime = 0.5f;

        // 무기 가져오기
        greatSword = sender.weapons[2];

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
        attack.ultimateEffect1 += R_Skill1;
        attack.ultimateEffect2 += R_Skill2;
    }

    public override void Exit(PlayerClass sender)
    {
        // 코루틴 제거
        sender.StopCoroutine(equipWeapon);

        // 장비 해제하기
        greatSword.UnEquip(sender.gameObject);

        // Effect함수 없애기
        attack.ultimateEffect2 -= R_Skill2;
        attack.ultimateEffect1 -= R_Skill1;
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

        attack.canChargeRotate = false;

        attack.comboAnimTime = new float[comboCount];
        attack.comboAnimTime[0] = comboAnimTime1;
        attack.comboAnimTime[1] = comboAnimTime2;
        attack.comboAnimTime[2] = comboAnimTime3;
        attack.comboAnimTime[3] = comboAnimTime4;
        attack.comboCount = comboCount;

        attack.returnTime = 0.0f;

        movement.rollAnimTime = rollTime;

        attack.ultimateAnimTime = ultimateAnimTime;
    }

    /// <summary>
    /// GreatSwordEffect 소환 함수
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    public void AttackEffect(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.BaseAttack_B, 0.5f);
        Factory.Instance.GetGreatSwordEffect(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(준비)
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    public void W_Skill_Prepare(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.WSkill_B_1, 0.5f);
        w_SkillEffect = Factory.Instance.GetBerserkerWSkill_Prepare(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(성공)
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    public void W_Skill_Success(Transform attackTransform)
    {
        w_SkillEffect.gameObject.SetActive(false);
        audioManager.PlaySound2D(AudioCode.WSkill_B_2, 0.5f);
        Factory.Instance.GetBerserkerWSkill_Success(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(실패)
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    public void W_Skill_Fail(Transform attackTransform)
    {
        w_SkillEffect.gameObject.SetActive(false);
        audioManager.PlaySound2D(AudioCode.WSkill_B_2, 0.5f);
        Factory.Instance.GetBerserkerWSkill_Fail(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// E스킬1 이펙트
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    void E_Skill1(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.ESkill_B_1, 0.5f);
        Factory.Instance.GetBerserkerESkill1(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// E스킬2 이펙트
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    void E_Skill2(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.ESkill_B_2, 0.5f);
        Factory.Instance.GetBerserkerESkill2(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// R스킬1,2 이펙트
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    void R_Skill1(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.RSkill_B_1, 0.5f);
        r_SkillEffect = Factory.Instance.GetBerserkerRSkill1(attackTransform.position, attackTransform.rotation.eulerAngles);
        Factory.Instance.GetBerserkerRSkill2(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// R스킬 이펙트 지우기
    /// </summary>
    void R_Skill2(Transform _)
    {
        r_SkillEffect.gameObject.SetActive(false);
    }

    /// <summary>
    /// 무기를 장착하는 코루틴(무기가 애니메이션에 맞게 보여지기 위함)
    /// </summary>
    /// <param name="sender">장착할 플레이어</param>
    public IEnumerator EquipWeapon(PlayerClass sender)
    {
        animator.SetInteger(Class_Hash, 1);

        GameObject weapon = greatSword.Equip(sender.gameObject);

        weapon.SetActive(false);

        sender.isChange = true;

        yield return new WaitForSeconds(equipTime);

        weapon.SetActive(true);

        yield return new WaitForSeconds(drawAnimTime - equipTime);

        sender.isChange = false;
    }
}
