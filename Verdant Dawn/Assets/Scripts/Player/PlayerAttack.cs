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
    [Header("UI 클릭 확인용 컴포넌트")]
    /// <summary>
    /// 왼손 공격 이펙트 트랜스폼(Canvas)
    /// </summary>
    public GraphicRaycaster graphicRaycaster;

    /// <summary>
    /// 왼손 공격 이펙트 트랜스폼(EventSystem)
    /// </summary>
    public EventSystem eventSystem;

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

    [Header("공격 쿨타임용 변수들(확인용)")]
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
    /// 다른 행동중일 때 공격을 제한하기 위한 변수
    /// </summary>
    [HideInInspector]
    public bool canAttack = true;

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

    // 컴포넌트들
    Animator animator;
    NavMeshAgent agent;
    PlayerMovement playerMovement;

    // 애니메이터용 해시값
    readonly int Attack_Hash = Animator.StringToHash("Attack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerMovement = GameManager.Instance.PlayerMovement;
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
            && canAttack)
        {
            Vector2 screen = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(screen);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
            {
                // 전 쿨타임 재는거 취소하기
                StopAllCoroutines();

                // 공격 방향 쳐다보기
                transform.LookAt(hitInfo.point);

                // 공격 코루틴 시작
                StartCoroutine(PerformedAttack());
            }
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
    }

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
        playerMovement.canMove = false;
        agent.ResetPath();

        // 공격 쿨타임이 끝나면
        yield return new WaitForSeconds(attackCoolTime);

        // 다시 움직일 수 있음
        playerMovement.canMove = true;

        // 공격을 처음부터 시작
        attackIndex = 0;
    }

    bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        return results.Count > 0;
    }

    void LeftHandAttackEffect()
    {
        if (canAttack)
        {
            onEffect.Invoke(leftHandAttackEffect);
        }
    }

    void RightHandAttackEffect()
    {
        if (canAttack)
        {
            onEffect.Invoke(rightHandAttackEffect);
        }
    }

    void BothHandsAttackEffect()
    {
        if (canAttack)
        {
            onEffect.Invoke(rightHandAttackEffect);
            onEffect.Invoke(leftHandAttackEffect);
        }
    }
}
