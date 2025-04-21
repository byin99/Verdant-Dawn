using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class PlayerMovement : MonoBehaviour, IInitializable
{
    /// <summary>
    /// VFX를 실행하는 델리게이트
    /// </summary>
    public event Action onArrive;

    /// <summary>
    /// 구르기를 하면 실행하는 델리게이트(UI 용)
    /// </summary>
    public event Action onRoll;

    /// <summary>
    /// 플레이어가 움직이면 실행되는 델리게이트
    /// </summary>
    public event Action<Vector3> onMove;

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
    /// 움직이고 있는 것을 알리는 변수
    /// </summary>
    [HideInInspector]
    public bool isMove = false;

    /// <summary>
    /// 구르고 있는 것을 알리는 변수
    /// </summary>
    [HideInInspector]
    public bool isRoll = false;

    /// <summary>
    /// 다음 구르기까지 남은 시간
    /// </summary>
    float rollRemainTime = 0.0f;

    /// <summary>
    /// 다음 구르기까지 남은 시간을 알려주는 프로퍼티 (UI용)
    /// </summary>
    public float RollRemainTime => rollRemainTime;

    // 컴포넌트들
    NavMeshAgent agent;
    Rigidbody rigid;
    Animator animator;
    Player player;

    // Animator에 있는 Parameter를 Hash값으로 저장하기
    readonly int Move_Hash = Animator.StringToHash("Move");
    readonly int Roll_Hash = Animator.StringToHash("Roll");

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!agent.enabled || agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            animator.SetBool(Move_Hash, false);             // 이동이 끝나면 Idle 애니메이션 주기
            onArrive?.Invoke();                             // VFX 제거
        }

        onMove?.Invoke(transform.position);                 // 플레이어가 움직일 때마다 위치를 알려주기
    }

    /// <summary>
    /// 움직이기 함수
    /// </summary>
    /// <param name="direction">움직이는 방향</param>
    public void SetDestination(Vector3 direction)
    {
        // 움직일 수 있는 상황이면
        if (player.CanMove)
        {
            animator.SetBool(Move_Hash, true);  // 이동하면서 Move 애니메이션 주기
            agent.SetDestination(direction);    // 목표 지점으로 이동하기
        }
    }

    /// <summary>
    /// 구르기 함수(구르기는 다른 핸동과 다르게 우선되어야 한다.)
    /// </summary>
    public void Roll()
    {
        // 구르기 쿨타임이 끝났다면
        if (rollRemainTime < 0.0f && player.CanRoll)
        {
            Vector2 screen = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(screen);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
            {
                // 몸 회전하기 전에 공격못하게 막기(몸 회전 후에 공격이 가능하면 플레이어의 forward가 돌아감)
                isRoll = true;

                Vector3 position = hitInfo.point;
                position.y = 0.0f;

                // 플레이어 마우스 방향으로 몸 회전하기
                transform.LookAt(position);

                StartCoroutine(PerformedRoll());
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
            Vector3 newPosition = transform.position + (animator.deltaPosition * rollPower);

            // 지형의 높이를 Raycast로 확인
            if (Physics.Raycast(newPosition + Vector3.up, Vector3.down, out RaycastHit hit, 2f, LayerMask.GetMask("Ground")))
            {
                newPosition.y = hit.point.y; // 땅 높이 적용
            }

            rigid.MovePosition(newPosition); // 구르기
        }
        rollRemainTime -= Time.deltaTime;
    }

    IEnumerator PerformedRoll()
    {
        // 움직임 제한
        agent.enabled = false;

        rollRemainTime = rollCoolTime;      // 구르기 쿨타임 주기
        onRoll?.Invoke();                   // UI에 신호보내기

        // 구르기
        animator.SetTrigger(Roll_Hash);
        isRoll = true;

        yield return new WaitForSeconds(rollAnimTime);

        // 구르기 끝
        isRoll = false;
        agent.enabled = true;

        // 공격모션이 씹히지 않기 위해 한프레임 뒤에 공격이 가능함
        yield return null;
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Initialize()
    {
        agent.enabled = true; // NavMeshAgent 활성화
    }
}