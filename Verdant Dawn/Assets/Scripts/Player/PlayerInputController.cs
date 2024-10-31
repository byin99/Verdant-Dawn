using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    /// <summary>
    /// 마우스 우클릭을 하면 실행되는 델리게이트
    /// </summary>
    public event Action<Vector2> onMove;

    /// <summary>
    /// 스페이스바를 누르면 실행되는 델리게이트
    /// </summary>
    public event Action onRoll;

    /// <summary>
    /// 마우스 왼클릭을 하면 실행되는 델리게이트
    /// </summary>
    public event Action onAttack;

    // 인풋 액션
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();                       // PlayerInputActions의 Player맵 활성화
        inputActions.Player.Move.performed += OnMove;       // Move의 performed에 OnMove함수 넣기
        inputActions.Player.Roll.performed += OnRoll;       // Roll의 performed에 OnRoll함수 넣기
        inputActions.Player.Attack.performed += OnAttack;   // Attack의 performed에 OnAttack함수 넣기
    }



    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;   // Attack의 performed에 OnAttack함수 빼기
        inputActions.Player.Roll.performed -= OnRoll;       // Roll의 performed에 OnRoll함수 빼기
        inputActions.Player.Move.performed -= OnMove;       // Move의 performed에 OnMove함수 빼기
        inputActions.Player.Disable();                      // PlayerInputActions의 Player맵 비활성화
    }

    /// <summary>
    /// 마우스 우클릭
    /// </summary>
    private void OnMove(InputAction.CallbackContext _)
    {
        Vector2 screen = Mouse.current.position.ReadValue();    // 스크린 좌표에서 마우스의 좌표를 저장하기
        onMove?.Invoke(screen);
    }

    /// <summary>
    /// 스페이스 바
    /// </summary>
    private void OnRoll(InputAction.CallbackContext _)
    {
        onRoll?.Invoke();   // 구르기
    }

    /// <summary>
    /// 마우스 왼쪽 버튼
    /// </summary>
    private void OnAttack(InputAction.CallbackContext _)
    {
        onAttack?.Invoke();
    }
}
