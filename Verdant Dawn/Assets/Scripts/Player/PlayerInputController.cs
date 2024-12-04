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
    public event Action<Vector3> onMove;

    /// <summary>
    /// 스페이스바를 누르면 실행되는 델리게이트
    /// </summary>
    public event Action onRoll;

    /// <summary>
    /// 마우스 왼클릭을 하면 실행되는 델리게이트
    /// </summary>
    public event Action onAttack;

    /// <summary>
    /// Q키를 누르면 실행되는 델리게이트
    /// </summary>
    public event Action onIdentitySkill;

    /// <summary>
    /// W키를 누르면 실행되는 델리게이트
    /// </summary>
    public event Action onChargingSkill;

    /// <summary>
    /// W키를 떼면 실행되는 델리게이트
    /// </summary>
    public event Action offChargingSkill;

    /// <summary>
    /// E키를 누르면 실행되는 델리게이트
    /// </summary>
    public event Action onComboSkill;

    /// <summary>
    /// E키를 떼면 실행되는 델리게이트
    /// </summary>
    public event Action offComboSkill;

    /// <summary>
    /// R키를 누르면 실행되는 델리게이트
    /// </summary>
    public event Action onUltimateSkill;

    /// <summary>
    /// R키를 떼면 실행되는 델리게이트
    /// </summary>
    public event Action offUltimateSkill;

    // 인풋 액션
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();                               // PlayerInputActions의 Player맵 활성화
        inputActions.Player.Move.performed += OnMove;               // Move의 performed에 OnMove함수 넣기
        inputActions.Player.Roll.performed += OnRoll;               // Roll의 performed에 OnRoll함수 넣기

        inputActions.Player.Attack.performed += OnAttack;           // Attack의 performed에 OnAttack함수 넣기

        inputActions.Player.Skill1.performed += OnIdentitySkill;    // Skill1의 performed에 OnIdentitySkill함수 넣기

        inputActions.Player.Skill2.performed += OnChargingSkill;    // Skill2의 performed에 OnChargingSkill함수 넣기
        inputActions.Player.Skill2.canceled += OffChargingSkill;    // Skill2의 canceled에 OnChargingSkill함수 넣기

        inputActions.Player.Skill3.performed += OnComboSkill;       // Skill3의 performed에 OnComboSkill함수 넣기
        inputActions.Player.Skill3.canceled += OffComboSkill;       // Skill3의 canceled에 OffComboSkill함수 넣기

        inputActions.Player.Skill4.performed += OnUltimateSkill;    // Skill4의 performed에 OnUltimateSkill함수 넣기
    }

    private void OnDisable()
    {
        inputActions.Player.Skill4.performed -= OnUltimateSkill;    // Skill4의 performed에 OnUltimateSkill함수 빼기

        inputActions.Player.Skill3.performed -= OnComboSkill;       // Skill3의 performed에 OnComboSkill함수 빼기

        inputActions.Player.Skill2.canceled -= OffChargingSkill;    // Skill2의 canceled에 OnChargingSkill함수 넣기
        inputActions.Player.Skill2.performed -= OnChargingSkill;    // Skill2의 performed에 OnChargingSkill함수 빼기

        inputActions.Player.Skill1.performed -= OnIdentitySkill;    // Skill1의 performed에 OnIdentitySkill함수 빼기

        inputActions.Player.Attack.performed -= OnAttack;           // Attack의 performed에 OnAttack함수 빼기

        inputActions.Player.Roll.performed -= OnRoll;               // Roll의 performed에 OnRoll함수 빼기
        inputActions.Player.Move.performed -= OnMove;               // Move의 performed에 OnMove함수 빼기
        inputActions.Player.Disable();                              // PlayerInputActions의 Player맵 비활성화
    }

    /// <summary>
    /// 마우스 우클릭(움직임)
    /// </summary>
    private void OnMove(InputAction.CallbackContext _)
    {
        Vector2 screen = Mouse.current.position.ReadValue();    // 스크린 좌표에서 마우스의 좌표를 저장하기
        Ray ray = Camera.main.ScreenPointToRay(screen);         // 마우스의 좌표로 쏘는 Ray 만들기
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, LayerMask.GetMask("Ground")))    // Ray가 Ground에 맞았다면
        {
            onMove?.Invoke(hitInfo.point);
        }
    }

    /// <summary>
    /// 스페이스 바(구르기)
    /// </summary>
    private void OnRoll(InputAction.CallbackContext _)
    {
        onRoll?.Invoke();
    }

    /// <summary>
    /// 마우스 좌클릭(기본 공격)
    /// </summary>
    private void OnAttack(InputAction.CallbackContext _)
    {
        onAttack?.Invoke();
    }

    /// <summary>
    /// Q 키(아이덴티티 스킬)
    /// </summary>
    private void OnIdentitySkill(InputAction.CallbackContext _)
    {
        onIdentitySkill?.Invoke();
    }

    /// <summary>
    /// W 키(차징 스킬)
    /// </summary>
    private void OnChargingSkill(InputAction.CallbackContext _)
    {
        onChargingSkill?.Invoke();
    }

    /// <summary>
    /// W 키(차징 스킬)
    /// </summary>
    private void OffChargingSkill(InputAction.CallbackContext _)
    {
        offChargingSkill?.Invoke();
    }

    /// <summary>
    /// E 키(연속 공격)
    /// </summary>
    private void OnComboSkill(InputAction.CallbackContext _)
    {
        onComboSkill?.Invoke();
    }

    /// <summary>
    /// E 키(연속 공격)
    /// </summary>
    private void OffComboSkill(InputAction.CallbackContext _)
    {
        offComboSkill?.Invoke();
    }

    /// <summary>
    /// R 키(궁극기 스킬)
    /// </summary>
    private void OnUltimateSkill(InputAction.CallbackContext _)
    {
        onUltimateSkill?.Invoke();
    }

    /// <summary>
    /// R 키(궁극기 스킬)
    /// </summary>
    private void OffUltimateSkill(InputAction.CallbackContext _)
    {
        offUltimateSkill?.Invoke();
    }
}
