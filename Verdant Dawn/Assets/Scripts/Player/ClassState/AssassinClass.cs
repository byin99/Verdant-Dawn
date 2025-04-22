using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinClass : BaseClass, IClass
{
    /// <summary>
    /// 왼쪽 무기
    /// </summary>
    Weapon leftDagger;

    /// <summary>
    /// 오른쪽 무기
    /// </summary>
    Weapon rightDagger;

    // 공격할 때의 시간들
    float attackAnimTime1 = 1.267f;
    float attackAnimTime2 = 1.0f;
    float attackAnimTime3 = 1.067f;
    float attackAnimTime4 = 1.05f;

    // 콤보 공격할 때의 시간들
    float comboAnimTime1 = 0.933f;
    float comboAnimTime2 = 0.883f;
    float comboAnimTime3 = 1.383f;
    float comboAnimTime4 = 1.783f;

    /// <summary>
    /// 궁극기 시간
    /// </summary>
    float ultimateAnimTime = 2.467f;

    /// <summary>
    /// 구르기 시간
    /// </summary>
    float rollTime = 0.55f;

    /// <summary>
    /// 무기를 드는 시간
    /// </summary>
    float drawAnimTime = 1.0f;

    /// <summary>
    /// 공격 애니메이션 개수
    /// </summary>
    int attackCount = 4;

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
        equipTime = 0.2f;

        // 무기 가져오기
        leftDagger = sender.weapons[5];
        rightDagger = sender.weapons[6];

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
        attack.ultimateEffect1 += R_Skill1;
        attack.ultimateEffect2 += R_Skill2;
    }

    public override void Exit(PlayerClass sender)
    {
        // 코루틴 제거
        sender.StopCoroutine(equipWeapon);

        // 장비 해제하기
        leftDagger.UnEquip(sender.gameObject);
        rightDagger.UnEquip(sender.gameObject);

        // Effect함수 없애기
        attack.ultimateEffect2 -= R_Skill2;
        attack.ultimateEffect1 -= R_Skill1;
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
        attack.attackAnimTime[3] = attackAnimTime4;
        attack.attackCount = attackCount;
        attack.attackIndex = 0;

        attack.canChargeRotate = true;

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
    /// DaggerEffect 소환 함수
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    public void AttackEffect(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.BaseAttack_A, 0.25f);
        Factory.Instance.GetDaggerEffect(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(준비)
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    public void W_Skill_Prepare(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.WSkill_A_1, 0.5f);
        w_SkillEffect = Factory.Instance.GetAssassinWSkill_Prepare(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(성공)
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    public void W_Skill_Success(Transform attackTransform)
    {
        w_SkillEffect.gameObject.SetActive(false);
        audioManager.PlaySound2D(AudioCode.WSkill_A_2, 0.5f);
        w_SkillEffect = Factory.Instance.GetAssassinWSkill_Success(attackTransform.position, attackTransform.rotation.eulerAngles);
        attack.StartCoroutine(AssassinChargingSkill());
    }

    /// <summary>
    /// W스킬 이펙트(실패)
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    public void W_Skill_Fail(Transform attackTransform)
    {
        w_SkillEffect.gameObject.SetActive(false);
        audioManager.PlaySound2D(AudioCode.WSkill_A_2, 0.5f);
        w_SkillEffect = Factory.Instance.GetAssassinWSkill_Fail(attackTransform.position, attackTransform.rotation.eulerAngles);
        attack.StartCoroutine(AssassinChargingSkill());
    }

    /// <summary>
    /// E스킬1 이펙트
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    void E_Skill1(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.ESkill_A_1, 0.5f);
        Factory.Instance.GetAssassinESkill1(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// E스킬2 이펙트
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    void E_Skill2(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.ESkill_A_2, 0.5f);
        Factory.Instance.GetAssassinESkill2(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// E스킬3 이펙트
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    void E_Skill3(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.ESkill_A_3, 0.5f);
        Factory.Instance.GetAssassinESkill3(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// R스킬1 이펙트
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    void R_Skill1(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.RSkill_A_1, 0.5f);
        r_SkillEffect = Factory.Instance.GetAssassinRSkill1(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// R스킬2 이펙트
    /// </summary>
    /// <param name="attackTransform">LevelUpEffect 소환 트랜스폼</param>
    void R_Skill2(Transform attackTransform)
    {
        audioManager.PlaySound2D(AudioCode.RSkill_A_2, 0.5f);
        r_SkillEffect.gameObject.SetActive(false);
        Factory.Instance.GetAssassinRSkill2(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// 무기를 장착하는 코루틴(무기가 애니메이션에 맞게 보여지기 위함)
    /// </summary>
    /// <param name="sender">장착할 플레이어</param>
    public IEnumerator EquipWeapon(PlayerClass sender)
    {
        animator.SetInteger(Class_Hash, 4);

        GameObject weapon1 = leftDagger.Equip(sender.gameObject);
        GameObject weapon2 = rightDagger.Equip(sender.gameObject);

        sender.isChange = true;

        weapon1?.SetActive(false);
        weapon2?.SetActive(false);

        yield return new WaitForSeconds(equipTime);

        weapon1?.SetActive(true);
        weapon2?.SetActive(true);

        yield return new WaitForSeconds(drawAnimTime - equipTime);

        sender.isChange = false;
    }

    /// <summary>
    /// 어쌔신 전용 차징 스킬(이펙트)
    /// </summary>
    /// <returns></returns>
    IEnumerator AssassinChargingSkill()
    {
        while (attack.isUseSkill)
        {
            yield return null;
        }

        w_SkillEffect.gameObject.SetActive(false);
    }
}
