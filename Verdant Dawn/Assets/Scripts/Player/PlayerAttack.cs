using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    // 보간용-----------------------------------------------------------------------------------------------------------------------------------------
    [Header("보간용 변수들")]
    /// <summary>
    /// 회전 속도(보간)
    /// </summary>
    public float rotationSpeed = 1000.0f;

    /// <summary>
    /// 회전 오차 범위
    /// </summary>
    public float stopThreshold = 1.0f;

    /// <summary>
    /// 현재 회전과 목표 회전과의 오차
    /// </summary>
    float angleDifference;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    Camera mainCamera;
    // ---------------------------------------------------------------------------------------------------------------------------------------------

    // UI 클릭 확인용--------------------------------------------------------------------------------------------------------------------------------
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

    // 기본 공격-------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 기본 공격 이펙트
    /// </summary>
    public event Action<Transform> onAttack;

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

    /// <summary>
    /// 기본 공격 코루틴 저장용
    /// </summary>
    IEnumerator attackCoroutine;
    // ---------------------------------------------------------------------------------------------------------------------------------------------

    // W 스킬---------------------------------------------------------------------------------------------------------------------------------------
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
    /// W스킬 시작(UI용)
    /// </summary>
    public event Action onCharge;

    /// <summary>
    /// W스킬 끝(UI용)
    /// </summary>
    public event Action offCharge;

    [Header("W스킬(차징 스킬)에 필요한 변수들")]
    /// <summary>
    /// 차징에 필요한 시간
    /// </summary>
    public float chargingTime = 3.0f;

    /// <summary>
    /// W스킬 쿨타임
    /// </summary>
    public float chargeCoolTime = 4.0f;

    /// <summary>
    /// 차징 스킬 중에 회전이 가능한지 판단하는 변수
    /// </summary>
    public bool canChargeRotate = false;

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
    // ---------------------------------------------------------------------------------------------------------------------------------------------

    // E 스킬---------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 콤보 스킬이 시작하면 실행되는 델리게이트
    /// </summary>
    public event Action onComboSkill;

    /// <summary>
    /// 콤보 스킬의 콤보가 끝나면 실행되는 델리게이트(UI용, true면 스킬 종료, false면 스킬 종료되지 않음)
    /// </summary>
    public event Action offComboSkill;

    /// <summary>
    /// 콤보 스킬이 끝나면 실행되는 델리게이트(UI용)
    /// </summary>
    public event Action finishComboSkill;

    /// <summary>
    /// 콤보 스킬 이펙트1을 실행시키는 델리게이트
    /// </summary>
    public event Action<Transform> comboEffect1;

    /// <summary>
    /// 콤보 스킬 이펙트2를 실행시키는 델리게이트
    /// </summary>
    public event Action<Transform> comboEffect2;

    /// <summary>
    /// 콤보 스킬 이펙트3을 실행시키는 델리게이트
    /// </summary>
    public event Action<Transform> comboEffect3;

    [Header("E스킬(콤보 스킬)에 필요한 변수들")]

    /// <summary>
    /// 줌 카메라(Hunter)
    /// </summary>
    public CinemachineVirtualCamera virtualCamera;

    /// <summary>
    /// Intensity 조절용(Hunter)
    /// </summary>
    public Volume volume;

    /// <summary>
    /// Intensity 조절용(Hunter)
    /// </summary>
    [HideInInspector]
    public Vignette vignette;

    /// <summary>
    /// 콤보 스킬 애니메이션 시간들
    /// </summary>
    public float[] comboAnimTime;

    /// <summary>
    /// 스킬 사용 후 돌아가는 시간
    /// </summary>
    [HideInInspector]
    public float returnTime = 0.0f;

    /// <summary>
    /// W스킬 쿨타임
    /// </summary>
    public float comboCoolTime = 5.0f;

    /// <summary>
    /// 콤보 스킬 개수
    /// </summary>
    public int comboCount;

    /// <summary>
    /// 콤보 스킬 현재 번호
    /// </summary>
    public int comboIndex;

    /// <summary>
    /// 콤보 스킬 전용 변수(움직임 제한을 풀기 위한 변수)
    /// </summary>
    public bool isCombo = false;

    /// <summary>
    /// 콤보 스킬 쿨타임을 재기위한 변수
    /// </summary>
    float comboRemainTime = 0.0f;

    /// <summary>
    /// 콤보 스킬 쿨타임
    /// </summary>
    float comboTime;

    /// <summary>
    /// 다음 콤보까지 남은 시간을 알려주는 프로퍼티
    /// </summary>
    public float ComboRemainTime => comboRemainTime;

    /// <summary>
    /// 콤보 스킬 코루틴 저장용
    /// </summary>
    IEnumerator comboCoroutine;

    // R 스킬---------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 궁극기가 시작되면 실행되는 델리게이트
    /// </summary>
    public event Action onUltimate;

    /// <summary>
    /// 궁극기버튼에서 손을 떼면 실행되는 델리게이트
    /// </summary>
    public event Action offUltimate;

    /// <summary>
    /// 궁극기가 끝나면 실행되는 델리게이트
    /// </summary>
    public event Action finishUltimate;

    /// <summary>
    /// 궁극기 이펙트 소환 델리게이트1
    /// </summary>
    public event Action<Transform> ultimateEffect1;

    /// <summary>
    /// 궁극기 이펙트 소환 델리게이트2
    /// </summary>
    public event Action<Transform> ultimateEffect2;

    [Header("궁극기용 변수들")]
    /// <summary>
    /// 궁극기 쿨타임
    /// </summary>
    public float ultimateCoolTime = 40.0f;

    /// <summary>
    /// 궁극기 애니메이션 시간
    /// </summary>
    [HideInInspector]
    public float ultimateAnimTime;

    /// <summary>
    /// 궁극기 쿨타임 남은시간
    /// </summary>
    float ultimateRemainTime;

    /// <summary>
    /// 궁극기 쿨타임 남은시간 알려주는 프로퍼티
    /// </summary>
    public float UltimateRemainTime => ultimateRemainTime;


    // ---------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 이펙트가 나오는 트랜스폼
    /// </summary>
    Transform attackTransform;

    // 컴포넌트들
    Animator animator;
    NavMeshAgent agent;
    Player player;
    SkillBarUI skillBarUI;

    // 애니메이터용 해시값
    readonly int Attack_Hash = Animator.StringToHash("Attack");
    readonly int WSkill_Hash = Animator.StringToHash("WSkill");
    readonly int ESkill_Hash = Animator.StringToHash("ESkill");
    readonly int RSkill_Hash = Animator.StringToHash("RSkill");
    readonly int SkillCancel_Hash = Animator.StringToHash("SkillCancel");

    private void Awake()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GetComponent<Player>();
        skillBarUI = UIManager.Instance.SkillBarUI;
        attackTransform = transform.GetChild(2);
        volume.profile.TryGet<Vignette>(out vignette);
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
            // 전 쿨타임 재는거 취소하기
            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);

            // 코루틴 저장
            attackCoroutine = PerformedAttack();

            // 공격 코루틴 시작
            StartCoroutine(attackCoroutine);
        }
    }

    /// <summary>
    /// 차징 스킬 시작 함수
    /// </summary>
    public void StartCharge()
    {
        // 스킬이 사용 가능하다면
        if (player.CanUseSkill && chargeRemainTime < 0.0f && !isCombo)
        {
            // 차징 시작
            onCharge?.Invoke();

            Vector2 screen = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(screen);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
            {
                // 차징 중에 회전하지 못하는 직업은 바로 회전시켜 적용
                if (!canChargeRotate)
                {
                    Vector3 target = hitInfo.point;
                    target.y = 0;
                    transform.LookAt(target);
                }
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
            if (chargingSkill != null)
                StopCoroutine(chargingSkill);

            animator.SetBool(WSkill_Hash, false);
            skillBarUI.StopChargingSKill(isChargeSuccessful);
        }
    }

    /// <summary>
    /// 콤보 스킬 시작 함수
    /// </summary>
    public void StartCombo()
    {
        if (player.CanUseSkill
            && comboRemainTime < 0
            && comboIndex != comboCount)
        {
            onComboSkill?.Invoke();
            isCombo = true;

            // 전 쿨타임 재는거 취소하기
            if (comboCoroutine != null)
                StopCoroutine(comboCoroutine);

            // 콤보 코루틴 저장
            comboCoroutine = ComboSkill();

            // 콤보 코루틴 시작
            StartCoroutine(comboCoroutine);
        }
    }

    /// <summary>
    /// 콤보 스킬의 콤보 종료 함수(UI용)
    /// </summary>
    public void FinishCombo()
    {
        // E_Glow 끄기
        offComboSkill?.Invoke();
    }

    /// <summary>
    /// 콤보 스킬 중간 취소
    /// </summary>
    public void CancelCombo()
    {
        if (isCombo) 
        {
            StopAllCoroutines();
            isUseSkill = false;

            agent.enabled = true;
            agent.ResetPath();

            comboIndex = 0;
            comboRemainTime = comboCoolTime;
            isCombo = false;
            finishComboSkill?.Invoke();
        }

    }

    /// <summary>
    /// 궁극기 시작
    /// </summary>
    public void StartUltimateSkill()
    {
        if (player.CanUseSkill && ultimateRemainTime < 0.0f)
        {
            onUltimate?.Invoke();
            StartCoroutine(UltimateSkill());
        }
    }

    /// <summary>
    /// 궁극기 끝
    /// </summary>
    public void FinishUltimateSkill()
    {
        offUltimate?.Invoke();
    }

    /// <summary>
    /// 애니메이터 업데이트용 함수
    /// </summary>
    void OnAnimatorMove()
    {
        // 기본 공격
        if (attackElapsedTime < attackCoolTime)
        {
            transform.position += animator.deltaPosition;
        }
        attackElapsedTime += Time.deltaTime;

        // W스킬
        if (isUseSkill)
        {
            transform.position += animator.deltaPosition;
        }
        chargeRemainTime -= Time.deltaTime;

        // E스킬
        if (isCombo)
        {
            transform.position += animator.deltaPosition;
        }
        comboRemainTime -= Time.deltaTime;

        // R스킬
        if (isUseSkill)
        {
            transform.position += animator.deltaPosition;
        }
        ultimateRemainTime -= Time.deltaTime;
    }

    /// <summary>
    /// 공격 쿨타임을 측정하는 코루틴 함수
    /// </summary>
    IEnumerator PerformedAttack()
    {
        agent.enabled = false;
        isAttack = true;

        do
        {
            RotateToMouse();
            yield return null;
        }
        while (angleDifference > stopThreshold);

        // 공격 시작
        animator.SetTrigger(Attack_Hash);
        attackCoolTime = attackAnimTime[attackIndex];
        attackElapsedTime = 0.0f;
        attackIndex++;

        // 공격 쿨타임이 끝나면
        yield return new WaitForSeconds(attackCoolTime);

        // 공격 끝
        isAttack = false;
        agent.enabled = true;
        agent.ResetPath();
        attackIndex = 0;
    }

    /// <summary>
    /// 차징 스킬 시간을 재는 코루틴
    /// </summary>
    IEnumerator ChargingSkill()
    {
        // 차징 스킬 시작
        animator.SetBool(WSkill_Hash, true);
        chargingTimeElapsed = 0.0f;
        isChargeSuccessful = false;

        yield return null;

        isUseSkill = true;
        agent.enabled = false;

        // 실패 구간
        while (chargingTimeElapsed < chargingTime * 0.7f)
        {
            chargingTimeElapsed += Time.deltaTime;

            if (canChargeRotate)
            {
                RotateToMouse();
            }

            yield return null;
        }

        // 성공 구간
        isChargeSuccessful = true;

        while (chargingTimeElapsed < chargingTime * 0.9f)
        {
            chargingTimeElapsed += Time.deltaTime;

            if (canChargeRotate)
            {
                RotateToMouse();
            }

            yield return null;
        }

        // 실패 구간
        isChargeSuccessful = false;

        while (chargingTimeElapsed < chargingTime)

        {
            chargingTimeElapsed += Time.deltaTime;

            if (canChargeRotate)
            {
                RotateToMouse();
            }

            yield return null;
        }

        FinishCharge();
    }

    /// <summary>
    /// 콤보 스킬 쿨타임을 재는 코루틴
    /// </summary>
    IEnumerator ComboSkill()
    {
        isUseSkill = true;
        agent.enabled = false;

        do
        {
            RotateToMouse();
            yield return null;
        }
        while (angleDifference > stopThreshold);

        // 콤보 공격 시작
        animator.SetTrigger(ESkill_Hash);
        animator.SetBool(SkillCancel_Hash, false);
        comboTime = comboAnimTime[comboIndex] * 0.4f;
        comboIndex++;

        yield return new WaitForSeconds(comboTime);

        // 콤보 다음 공격 가능
        isUseSkill = false;

        if (comboIndex != comboCount)
        {
            yield return new WaitForSeconds(2.0f - comboTime);
        }
        else
        {
            yield return new WaitForSeconds(comboTime * 1.5f);
        }

        // 콤보 공격 끝
        animator.SetBool(SkillCancel_Hash, true);

        yield return new WaitForSeconds(returnTime);

        agent.enabled = true;
        agent.ResetPath();

        comboIndex = 0;
        comboRemainTime = comboCoolTime;
        isCombo = false;
        finishComboSkill?.Invoke();
    }

    /// <summary>
    /// 궁극기용 코루틴
    /// </summary>
    IEnumerator UltimateSkill()
    {
        // 스킬 시작
        isUseSkill = true;
        agent.enabled = false;

        // 플레이어 방향을 마우스 방향으로 바꾸기
        do
        {
            RotateToMouse();
            yield return null;
        }
        while (angleDifference > stopThreshold);

        // 궁극기 쿨타임 주기
        ultimateRemainTime = ultimateCoolTime;
        finishUltimate?.Invoke();

        // 카메라 조정 및 애니메이션 시작
        SwitchToUltimateCamera();
        animator.SetBool(RSkill_Hash, true);
        ultimateRemainTime = ultimateCoolTime;

        yield return new WaitForSeconds(ultimateAnimTime);

        // 카메라 조정 및 애니메이션 끝
        animator.SetBool(RSkill_Hash, false);
        SwitchToUltimateCamera();

        // 궁극기 종료
        agent.enabled = true;
        isUseSkill = false;
    }

    /// <summary>
    /// 마우스가 UI위에 있는지 확인하는 함수
    /// </summary>
    /// <returns>UI위에 있다면 true, UI위에 없다면 false</returns>
    bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        return results.Count > 0;
    }

    /// <summary>
    /// 보간을 이용해서 마우스 방향을 자연스럽게 쳐다보는 함수
    /// </summary>
    void RotateToMouse()
    {
        // 마우스 화면 좌표 가져오기
        Vector2 screenMousePosition = Mouse.current.position.ReadValue();

        // 화면 좌표를 월드 좌표로 변환
        Ray ray = mainCamera.ScreenPointToRay(screenMousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f)) // 필요한 거리만큼만 Raycast
        {
            // 대상 방향 계산
            Vector3 direction = (hit.point - transform.position).normalized;
            direction.y = 0; // Y축 고정 (회전 제한)

            // 목표 회전값 계산
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 현재 회전에서 목표 회전으로 보간
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        }

    }

    /// <summary>
    /// 궁극기를 사용했을 때 카메라의 위치를 바꿔주는 함수
    /// </summary>
    void SwitchToUltimateCamera()
    {
        virtualCamera.Priority += 10;
        virtualCamera.Priority %= 20;
    }

    /// <summary>
    /// 기본 공격(Animation Clip Event용)
    /// </summary>
    void BasicAttackEffect()
    {
        onAttack.Invoke(attackTransform);
    }

    /// <summary>
    /// 차징 스킬 시작(Animation Clip Event용)
    /// </summary>
    void StartChargingSkill()
    {
        onCharge_Prepare?.Invoke(attackTransform);
    }

    /// <summary>
    /// 차징 스킬 종료(Animation Clip Event용)
    /// </summary>
    void FinishChargingSkill()
    {
        if (isChargeSuccessful)
        {
            onCharge_Success?.Invoke(attackTransform);
        }
        else
        {
            onCharge_Fail?.Invoke(attackTransform);
        }
        
        chargeRemainTime = chargeCoolTime;
        offCharge?.Invoke();
    }

    /// <summary>
    /// 콤보 스킬1(Animation Clip Event용)
    /// </summary>
    void ComboSKill1()
    {
        comboEffect1?.Invoke(attackTransform);
    }

    /// <summary>
    /// 콤보 스킬2(Animation Clip Event용)
    /// </summary>
    void ComboSkill2()
    {
        comboEffect2?.Invoke(attackTransform);
    }

    /// <summary>
    /// 콤보 스킬3(Animation Clip Event용)
    /// </summary>
    void ComboSkill3()
    {
        comboEffect3?.Invoke(attackTransform);
    }

    /// <summary>
    /// 궁극기1(Animation Clip Event용)
    /// </summary>
    void UltimateSkill1()
    {
        ultimateEffect1?.Invoke(attackTransform);
    }

    /// <summary>
    /// 궁극기2(Animation Clip Event용)
    /// </summary>
    void UltimateSkill2()
    {
        ultimateEffect2?.Invoke(attackTransform);
    }

    /// <summary>
    /// 스킬 종료(Animation Clip Event용)
    /// </summary>
    void FinishSkill()
    {
        isUseSkill = false;
        agent.enabled = true;
        agent.ResetPath();
    }
}
