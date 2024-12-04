using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HunterClass : BaseClass, IClass
{
    /// <summary>
    /// 무기
    /// </summary>
    Weapon riple;

    // 공격할 때의 시간들
    float attackAnimTime1 = 0.45f;

    // 콤보 공격할 때의 시간들
    float comboAnimTime1 = 2.5f;
    float comboAnimTime2 = 1.833f;
    float comboAnimTime3 = 0.5f;

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

    /// <summary>
    /// 콤보 공격 애니메이션 개수
    /// </summary>
    int comboCount = 5;

    /// <summary>
    /// 줌하는 시간
    /// </summary>
    float zoomTime = 0.2f;

    /// <summary>
    /// 시간 축적용
    /// </summary>
    float timeElapsed;

    /// <summary>
    /// 줌 이펙트
    /// </summary>
    E_SkillEffect2 e_SkillEffect;

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
        attack.onAttack += AttackEffect;
        attack.onCharge_Prepare += W_Skill_Prepare;
        attack.onCharge_Success += W_Skill_Success;
        attack.onCharge_Fail += W_Skill_Fail;
        attack.comboEffect1 += E_Skill1;
        attack.comboEffect2 += E_Skill2;
        attack.finishComboSkill += E_SkillFinish;
    }

    

    public override void Exit(PlayerClass sender)
    {
        // 코루틴 제거
        sender.StopCoroutine(equipWeapon);

        // 장비 해제하기
        riple.UnEquip(sender.gameObject);

        // Effect함수 없애기
        attack.finishComboSkill -= E_SkillFinish;
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
        attack.attackCount = attackCount;
        attack.attackIndex = 0;

        attack.canChargeRotate = true;

        attack.comboAnimTime = new float[comboCount];
        attack.comboAnimTime[0] = comboAnimTime1;
        attack.comboAnimTime[1] = comboAnimTime2;
        attack.comboAnimTime[2] = comboAnimTime2;
        attack.comboAnimTime[3] = comboAnimTime2;
        attack.comboAnimTime[4] = comboAnimTime3;
        attack.comboCount = comboCount;

        attack.returnTime = 1.1f;

        movement.rollAnimTime = rollTime;
    }

    /// <summary>
    /// RipleEffect 소환 함수
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void AttackEffect(Transform attackTransform)
    {
        Factory.Instance.GetRipleEffect(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(준비)
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void W_Skill_Prepare(Transform attackTransform)
    {
        w_SkillEffect = Factory.Instance.GetHunterWSkill_Prepare(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(성공)
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void W_Skill_Success(Transform attackTransform)
    {
        w_SkillEffect.gameObject.SetActive(false);
        Factory.Instance.GetHunterWSkill_Success(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// W스킬 이펙트(실패)
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void W_Skill_Fail(Transform attackTransform)
    {
        w_SkillEffect.gameObject.SetActive(false);
        Factory.Instance.GetHunterWSkill_Fail(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// E스킬1 이펙트
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    void E_Skill1(Transform attackTransform)
    {
        e_SkillEffect = Factory.Instance.GetHunterESkill1(attackTransform.position, Vector3.zero);
        attack.StartCoroutine(OnZoom());
    }

    /// <summary>
    /// E스킬2 이펙트
    /// </summary>
    void E_Skill2(Transform _)
    {
        Factory.Instance.GetHunterESkill2(e_SkillEffect.transform.position, e_SkillEffect.transform.rotation.eulerAngles);
    }

    /// <summary>
    /// E스킬 종료 또는 취소
    /// </summary>
    void E_SkillFinish()
    {
        attack.StartCoroutine(OffZoom());
        e_SkillEffect.gameObject.SetActive(false);
    }

    /// <summary>
    /// 무기를 장착하는 코루틴(무기가 애니메이션에 맞게 보여지기 위함)
    /// </summary>
    /// <param name="sender">장착할 플레이어</param>
    public IEnumerator EquipWeapon(PlayerClass sender)
    {
        animator.SetInteger(Class_Hash, 2);

        GameObject weapon = riple.Equip(sender.gameObject);

        weapon?.SetActive(false);

        sender.isChange = true;

        yield return new WaitForSeconds(equipTime);

        weapon?.SetActive(true);

        yield return new WaitForSeconds(drawAnimTime - equipTime);

        sender.isChange = false;
    }

    /// <summary>
    /// 줌하는 코루틴
    /// </summary>
    IEnumerator OnZoom()
    {
        timeElapsed = 0.0f;
        attack.virtualCamera.Priority = 15;
        while (timeElapsed < zoomTime)
        {
            timeElapsed += Time.deltaTime;
            attack.vignette.intensity.value = timeElapsed / zoomTime * 0.3f;
            yield return null;
        }
    }

    /// <summary>
    /// 줌을 푸는 코루틴
    /// </summary>
    IEnumerator OffZoom()
    {
        timeElapsed = zoomTime;
        attack.virtualCamera.Priority = 5;
        while (timeElapsed > 0)
        {
            timeElapsed -= Time.deltaTime;
            attack.vignette.intensity.value = timeElapsed / zoomTime;
            yield return null;
        }
    }
}
