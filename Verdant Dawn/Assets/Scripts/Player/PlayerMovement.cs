using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{ 
    /// <summary>
    /// VFX를 실행하는 델리게이트
    /// </summary>
    public Action onArrive;

    /// <summary>
    /// VFX의 실행위치를 알려주는 델리게이트
    /// </summary>
    public Action<Vector3> onDirection;

    /// <summary>
    /// 구르기를 하면 실행하는 델리게이트(UI 용)
    /// </summary>
    public Action onRoll;

    [Header("구르기용 변수들")]
    /// <summary>
    /// 구르는 힘
    /// </summary>
    public float rollPower = 10.0f;

    /// <summary>
    /// 구르기 쿨타임
    /// </summary>
    public float rollCoolTime = 7.0f;

    /// <summary>
    /// 구르기 애니매이션이 나오는 시간
    /// </summary>
    [HideInInspector]
    public float rollAnimTime = 0.8f;

    /// <summary>
    /// 다음 구르기까지 남은 시간
    /// </summary>
    float rollRemainTime = 0.0f;

    /// <summary>
    /// 구르는 방향
    /// </summary>
    Vector3 rollDirection = Vector3.zero;

    /// <summary>
    /// 다음 구르기까지 남은 시간을 알려주는 프로퍼티 (UI용)
    /// </summary>
    public float RollRemainTime => rollRemainTime;

    // 컴포넌트들
    NavMeshAgent agent;
    Animator animator;
    PlayerAttack attack;

    // Animator에 있는 Parameter를 Hash값으로 저장하기
    readonly int Move_Hash = Animator.StringToHash("Move");
    readonly int Roll_Hash = Animator.StringToHash("Roll");

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attack = gameObject.GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (agent.enabled && agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            animator.SetBool(Move_Hash, false);             // 이동이 끝나면 Idle 애니메이션 주기
            onArrive?.Invoke();                             // VFX 제거
        }

        if (rollRemainTime < rollCoolTime - rollAnimTime)
        {
            // 다시 agent 사용하기
            if (agent.isStopped)
            {
                animator.SetBool(Move_Hash, false);
                agent.ResetPath();
                agent.isStopped = false;
            }
        }
    }

    public void SetDestination(Vector2 screen)
    {
        Ray ray = Camera.main.ScreenPointToRay(screen);         // 마우스의 좌표로 쏘는 Ray 만들기
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, LayerMask.GetMask("Ground")))    // Ray가 Ground에 맞았다면
        {
            onDirection?.Invoke(new Vector3(hitInfo.point.x, 0.001f, hitInfo.point.z));         // 새로운 목표지점을 보내준다.
            agent.SetDestination(hitInfo.point);                // 맞은 곳으로 이동하기
            if (!agent.isStopped)
            {
                animator.SetBool(Move_Hash, true);              // 이동하면서 Move 애니메이션 주기
            }
        }
    }

    public void Roll()
    {
        if (rollRemainTime < 0.0f)                                // 구르기 쿨타임이 끝났다면
        {
            Vector2 screen = Mouse.current.position.ReadValue();
            Debug.Log(screen);
            Ray ray = Camera.main.ScreenPointToRay(screen);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, LayerMask.GetMask("Ground")))
            {
                // 플레이어 마우스 방향으로 몸 회전하기
                transform.LookAt(hitInfo.point);

                // 구르기 준비
                agent.isStopped = true;

                // 구르기
                animator.SetTrigger(Roll_Hash);

                // 구른 후에 조치
                rollRemainTime = rollCoolTime;      // 구르기 쿨타임 주기
                onRoll?.Invoke();                   // UI에 신호보내기
            }
        }
    }

    /// <summary>
    /// 애니메이터 업데이트용 함수
    /// </summary>
    void OnAnimatorMove()
    {
        // 구르는 동안에는
        if (rollRemainTime > rollCoolTime - rollAnimTime && rollRemainTime < rollCoolTime)
        {
            // 구르기
            rollDirection = animator.deltaPosition; 
            transform.position += rollDirection * rollPower;
        }
        rollRemainTime -= Time.deltaTime;
    }
}
