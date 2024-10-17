using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// 구르기를 하면 실행하는 델리게이트
    /// </summary>
    public Action onRoll;

    /// <summary>
    /// VFX를 실행하는 델리게이트
    /// </summary>
    public Action onArrive;

    /// <summary>
    /// VFX의 실행위치를 알려주는 델리게이트
    /// </summary>
    public Action<Vector3> onDirection;

    /// <summary>
    /// 구르기 속도
    /// </summary>
    public float rollSpeed = 10.0f;

    /// <summary>
    /// 구르기 쿨타임
    /// </summary>
    public float rollCoolTime = 7.0f;

    /// <summary>
    /// 다음 구르기까지 남은 시간
    /// </summary>
    float rollRemainTime = 0.0f;

    /// <summary>
    /// 구르기 애니매이션이 나오는 시간
    /// </summary>
    float rollDuration = 0.5835f;

    /// <summary>
    /// 구르기가 끝나기까지 시간 
    /// </summary>
    float elapsedTime = 0.0f;

    /// <summary>
    /// 구르기가 가능한지 알려주는 프로퍼티(true면 구르기 가능, false면 구르기 불가능)
    /// </summary>
    bool CanRoll => rollRemainTime < 0.0f;

    /// <summary>
    /// 다음 구르기까지 남은 시간을 알려주는 프로퍼티 (UI용)
    /// </summary>
    public float RollRemainTime => rollRemainTime;

    /// <summary>
    /// 구르는 방향
    /// </summary>
    Vector3 rollDirection = Vector3.zero;

    /// <summary>
    /// 플레이어 인풋 액션
    /// </summary>
    PlayerInputActions inputActions;

    // 컴포넌트들
    NavMeshAgent agent;
    Animator animator;

    // Animator에 있는 Parameter를 Hash값으로 저장하기
    readonly int Move_Hash = Animator.StringToHash("Move");
    readonly int Roll_Hash = Animator.StringToHash("Roll");

    private void Awake()
    {
        inputActions = new PlayerInputActions();    // PlayerInputActions 새로 생성하기
        agent = GetComponent<NavMeshAgent>();       // NavMeshAgent 컴포넌트 가져오기
        animator = GetComponent<Animator>();        // Animator 컴포넌트 가져오기
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();                   // PlayerInputActions의 Player맵 활성화
        inputActions.Player.Move.performed += OnMove;   // Move의 performed에 OnMove함수 넣기
        inputActions.Player.Roll.performed += OnRoll;   // Roll의 performed에 OnRoll함수 넣기
    }

    private void OnDisable()
    {
        inputActions.Player.Roll.performed -= OnRoll;   // Roll의 performed에 OnRoll함수 빼기
        inputActions.Player.Move.performed -= OnMove;   // Move의 performed에 OnMove함수 빼기
        inputActions.Player.Enable();                   // PlayerInputActions의 Player맵 비활성화
    }

    /// <summary>
    /// 마우스 우클릭
    /// </summary>
    /// <param name="_"></param>
    private void OnMove(InputAction.CallbackContext _)
    {
        Vector2 screen = Mouse.current.position.ReadValue();    // 스크린 좌표에서 마우스의 좌표를 저장하기
        Ray ray = Camera.main.ScreenPointToRay(screen);         // 마우스의 좌표로 쏘는 Ray 만들기
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, LayerMask.GetMask("Ground")))    // Ray가 Ground에 맞았다면
        {
            onDirection?.Invoke(new Vector3(hitInfo.point.x, 0.001f, hitInfo.point.z)); // 새로운 목표지점을 보내준다.
            agent.SetDestination(hitInfo.point);    // 맞은 곳으로 이동하기
            animator.SetBool(Move_Hash, true);      // 이동하면서 Move 애니메이션 주기
        }
    }

    /// <summary>
    /// 스페이스 바
    /// </summary>
    /// <param name="_"></param>
    private void OnRoll(InputAction.CallbackContext _)
    {
        if (CanRoll)                                // 구르기 쿨타임이 끝났다면
        {
            Vector2 screen = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(screen);
    
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, LayerMask.GetMask("Ground")))
            {
                transform.LookAt(hitInfo.point);    // 플레이어 마우스 방향으로 몸 회전하기
                animator.SetTrigger(Roll_Hash);     // 애니메이션 트리거
                agent.ResetPath();                  // 현재 경로 리셋 (NavMeshAgent 사용 중지)
                agent.isStopped = true;
    
                // 구르는 방향을 계산
                rollDirection = (hitInfo.point - transform.position).normalized;
                rollDirection.y = 0;                // 수평면에서만 구르기

                rollRemainTime = rollCoolTime;      // 구르기 쿨타임 주기
                onRoll?.Invoke();                   // UI에 신호보내기
                elapsedTime = 0.0f;                 // 구르기 시작
            }
        }
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            animator.SetBool(Move_Hash, false);     // 이동이 끝나면 Idle 애니메이션 주기
            onArrive?.Invoke();                     // VFX 제거
        }

        rollRemainTime -= Time.deltaTime;

        if (elapsedTime < rollDuration)             // 구르는 도중이면
        {
            elapsedTime += Time.deltaTime;          
            transform.Translate(Time.deltaTime * rollSpeed * rollDirection, Space.World);   // 구르기
        }
        else
        {
            agent.isStopped = false;                // NavmeshAgent 활성화
        }
    } 
}
