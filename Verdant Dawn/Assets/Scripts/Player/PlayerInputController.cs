using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public event Action<Vector2> onMove;

    public event Action onRoll;

    public event Action<bool> onChange;

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
        inputActions.Player.Change.performed += OnChange;   // Change의 performed에 OnChange함수 넣기
    }

    private void OnDisable()
    {
        inputActions.Player.Change.performed -= OnChange;   // Change의 performed에 OnChange함수 빼기
        inputActions.Player.Roll.performed -= OnRoll;       // Roll의 performed에 OnRoll함수 빼기
        inputActions.Player.Move.performed -= OnMove;       // Move의 performed에 OnMove함수 빼기
        inputActions.Player.Enable();                       // PlayerInputActions의 Player맵 비활성화
    }

    /// <summary>
    /// 마우스 우클릭
    /// </summary>
    /// <param name="_"></param>
    private void OnMove(InputAction.CallbackContext _)
    {
        Vector2 screen = Mouse.current.position.ReadValue();    // 스크린 좌표에서 마우스의 좌표를 저장하기
        onMove?.Invoke(screen);
    }

    /// <summary>
    /// 스페이스 바
    /// </summary>
    /// <param name="_"></param>
    private void OnRoll(InputAction.CallbackContext _)
    {
        onRoll?.Invoke();
    }

    /// <summary>
    /// E키
    /// </summary>
    /// <param name="_"></param>
    private void OnChange(InputAction.CallbackContext context)
    {
        onChange?.Invoke(!context.canceled);
    }

}
