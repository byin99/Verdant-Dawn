using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    // ---------------------------------------------------------------------------------------------------------------------------------------------
    [Header("UI 클릭 확인용 컴포넌트")]
    /// <summary>
    /// UI 클릭 확인용 컴포넌트(Canvas)
    /// </summary>
    public GraphicRaycaster graphicRaycaster;

    /// <summary>
    /// UI 클릭 확인용 컴포넌트(EventSystem)
    /// </summary>
    public EventSystem eventSystem;
    // ---------------------------------------------------------------------------------------------------------------------------------------------

    // ---------------------------------------------------------------------------------------------------------------------------------------------
    [Header("공격 이펙트 트랜스폼")]
    /// <summary>
    /// 왼손 공격 이펙트 트랜스폼
    /// </summary>
    public Transform leftHandAttackEffect;

    /// <summary>
    /// 오른손 공격 이펙트 트랜스폼
    /// </summary>
    public Transform rightHandAttackEffect;

    /// <summary>
    /// 이펙트 소환 델리게이트
    /// </summary>
    public event Action<Transform> onEffect;
    // ---------------------------------------------------------------------------------------------------------------------------------------------

    // ---------------------------------------------------------------------------------------------------------------------------------------------
    [Header("공격용 변수들(확인용)")]
    /// <summary>
    /// 애니메이션 시간들
    /// </summary>
    public float[] attackAnimTime;

    /// <summary>
    /// 공격 총 개수
    /// </summary>
    public int attackCount;

    /// <summary>
    /// 공격 번호
    /// </summary>
    public int attackIndex = 0;

    /// <summary>
    /// 공격 중인것을 알리는 변수
    /// </summary>
    [HideInInspector]
    public bool isAttack = false;

    /// <summary>
    /// 공격 쿨타임을 재기위한 변수
    /// </summary>
    float attackElapsedTime = 0.0f;

    /// <summary>
    /// 공격 쿨타임
    /// </summary>
    float attackCoolTime;

    /// <summary>
    /// 공격 쿨타임을 줄여주는 프로퍼티
    /// </summary>
    float AttackTimeDecreaseRate => attackCoolTime * 0.4f;
    // ---------------------------------------------------------------------------------------------------------------------------------------------

    // ---------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// W스킬 이펙트(준비)
    /// </summary>
    public event Action<Transform> onCharge_Prepare;

    /// <summary>
    /// W스킬 이펙트(성공)
    /// </summary>
    public event Action<Transform> onCharge_Success;

    /// <summary>
    /// W스킬 이펙트(실패)
    /// </summary>
    public event Action<Transform> onCharge_Fail;

    /// <summary>
    /// W스킬 끝
    /// </summary>
    public event Action offCharge;

    /// <summary>
    /// 차징에 필요한 시간
    /// </summary>
    public float chargingTime = 3.0f;

    /// <summary>
    /// W스킬 쿨타임
    /// </summary>
    public float chargeCoolTime = 4.0f;

    /// <summary>
    /// W스킬 쿨타임 재는 변수
    /// </summary>
    float chargeRemainTime = 0.0f;

    /// <summary>
    /// 스킬을 사용중인지 알리는 변수
    /// </summary>
    [HideInInspector]
    public bool isUseSkill = false;

    /// <summary>
    /// 차징 시간을 재기위한 변수
    /// </summary>
    float chargingTimeElapsed = 0.0f;

    /// <summary>
    /// 차징이 성공적으로 됐는지 확인하는 변수(true면 성공, false면 실패)
    /// </summary>
    bool isChargeSuccessful;

    /// <summary>
    /// 차징 시간을 알려주는 프로퍼티(UI용)
    /// </summary>
    public float ChargingTimeElapsed => chargingTimeElapsed;

    /// <summary>
    /// W스킬 쿨타임을 알려주는 프로퍼티(UI용)
    /// </summary>
    public float ChargeRemainTime => chargeRemainTime;

    /// <summary>
    /// 차징 코루틴 저장용
    /// </summary>
    IEnumerator chargingSkill;

    /// <summary>
    /// 스킬 공격 트랜스폼
    /// </summary>
    Transform skillAttackTransform;
    // ---------------------------------------------------------------------------------------------------------------------------------------------

    // 컴포넌트들
    Animator animator;
    NavMeshAgent agent;
    Player player;
    SkillBarUI skillBarUI;

    // 애니메이터용 해시값
    readonly int Attack_Hash = Animator.StringToHash("Attack");
    readonly int WSkill_Hash = Animator.StringToHash("WSkill");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GetComponent<Player>();
        skillBarUI = UIManager.Instance.SkillBarUI;
        skillAttackTransform = transform.GetChild(2);
    }

    /// <summary>
    /// 공격하는 함수
    /// </summary>
    public void Attack()
    {
        // UI클릭시 공격하지 않음
        if (IsPointerOverUI())
        {
            return;
        }

        // 공격 가능한 시간이고 콤보가 끝나지 않았다면
        if (attackElapsedTime > AttackTimeDecreaseRate
            && attackIndex != attackCount
            && player.CanAttack)
        {
            Vector2 screen = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(screen);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
            {
                // 전 쿨타임 재는거 취소하기
                StopAllCoroutines();

                // 공격 방향 쳐다보기
                Vector3 targetPosition = hitInfo.point;
                targetPosition.y = 0;
                transform.LookAt(targetPosition);

                // 공격 코루틴 시작
                StartCoroutine(PerformedAttack());
            }
        }
    }

    /// <summary>
    /// 차징 스킬 시작 함수
    /// </summary>
    public void StartCharge()
    {
        // UI클릭시 공격하지 않음
        if (IsPointerOverUI())
        {
            return;
        }

        // 스킬이 사용 가능하다면
        if (player.CanUseSkill && chargeRemainTime < 0.0f)
        {
            Vector2 screen = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(screen);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
            {
                isUseSkill = true;
                agent.enabled = false;

                // 공격 방향 쳐다보기
                Vector3 targetPosition = hitInfo.point;
                targetPosition.y = 0;
                transform.LookAt(targetPosition);

                // 차징 코루틴 저장
                chargingSkill = ChargingSkill();

                // 차징 스킬 코루틴 실행
                StartCoroutine(chargingSkill);

                // 차징 스킬바 실행
                skillBarUI.StartChargingSkill();
            }
        }
    }

    /// <summary>
    /// 차징 스킬 종료 함수
    /// </summary>
    public void FinishCharge()
    {
        if (isUseSkill)
        {
            StopCoroutine(chargingSkill);
            animator.SetBool(WSkill_Hash, false);
            skillBarUI.StopChargingSKill(isChargeSuccessful);
        }
    }

    /// <summary>
    /// 애니메이터 업데이트용 함수
    /// </summary>
    void OnAnimatorMove()
    {
        if (attackElapsedTime < attackCoolTime)
        {
            transform.position += animator.deltaPosition;
        }
        attackElapsedTime += Time.deltaTime;

        if (isUseSkill)
        {
            transform.position += animator.deltaPosition;
        }
        chargeRemainTime -= Time.deltaTime;
    }

    /// <summary>
    /// 공격 쿨타임을 측정하는 코루틴 함수
    /// </summary>
    IEnumerator PerformedAttack()
    {
        // 공격하기
        animator.SetTrigger(Attack_Hash);

        // 공격 쿨타임 주기
        attackCoolTime = attackAnimTime[attackIndex];

        // 쿨타임 재기
        attackElapsedTime = 0.0f;

        // 다음 공격 번호로 바꾸기
        attackIndex++;

        // Idle상태로 가지않고 바로 공격하기 위해 ResetPath를 한프레임 뒤에 해줌
        yield return null;

        // 공격 중
        isAttack = true;

        // agent 경로 초기화
        agent.ResetPath();

        // 공격 쿨타임이 끝나면
        yield return new WaitForSeconds(attackCoolTime);

        // 공격 끝
        isAttack = false;

        // 공격을 처음부터 시작
        attackIndex = 0;
    }

    /// <summary>
    /// 차징 스킬 시간을 재는 코루틴
    /// </summary>
    public IEnumerator ChargingSkill()
    {
        chargingTimeElapsed = 0.0f;
        animator.SetBool(WSkill_Hash, true);
        isChargeSuccessful = false;

        while (chargingTimeElapsed < chargingTime * 0.7f)
        {
            chargingTimeElapsed += Time.deltaTime;
            yield return null;
        }

        isChargeSuccessful = true;

        while (chargingTimeElapsed < chargingTime * 0.9f)
        {
            chargingTimeElapsed += Time.deltaTime;
            yield return null;
        }

        isChargeSuccessful = false;

        while (chargingTimeElapsed < chargingTime)
        {
            chargingTimeElapsed += Time.deltaTime;
            yield return null;
        }

        FinishCharge();
    }

    /// <summary>
    /// 마우스가 UI를 클릭했는지 확인하는 함수
    /// </summary>
    /// <returns>UI를 클릭했다면 true, UI를 클릭하지 않았다면 false</returns>
    bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        return results.Count > 0;
    }

    /// <summary>
    /// 왼손 공격(Animation Clip Event용)
    /// </summary>
    void LeftHandAttackEffect()
    {
        onEffect.Invoke(leftHandAttackEffect);
    }

    /// <summary>
    /// 오른손 공격(Animation Clip Event용)
    /// </summary>
    void RightHandAttackEffect()
    {
        onEffect.Invoke(rightHandAttackEffect);
    }

    /// <summary>
    /// 차징 스킬 시작(Animation Clip Event용)
    /// </summary>
    void StartChargingSkill()
    {
        onCharge_Prepare?.Invoke(skillAttackTransform);
    }

    /// <summary>
    /// 차징 스킬 종료(Animation Clip Event용)
    /// </summary>
    void FinishChargingSkill()
    {
        if (isChargeSuccessful)
        {
            onCharge_Success?.Invoke(skillAttackTransform);
        }
        else
        {
            onCharge_Fail?.Invoke(skillAttackTransform);
        }
        
        chargeRemainTime = chargeCoolTime;
        offCharge?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    void FinishSkill()
    {
        isUseSkill = false;
        agent.enabled = true;
        agent.ResetPath();
    }
}
