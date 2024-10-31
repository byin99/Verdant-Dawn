using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponChangeUI : MonoBehaviour
{
    /// <summary>
    /// 클래스별 이미지의 보더
    /// </summary>
    Dictionary<CharacterClass, Image> borders;

    // 컴포넌트들
    PlayerInputActions inputActions;
    CanvasGroup canvasGroup;
    PlayerClass playerClass;

    private void Awake()
    {
        borders = new Dictionary<CharacterClass, Image>(5);
        inputActions = new PlayerInputActions();
        canvasGroup = GetComponent<CanvasGroup>();
        playerClass = GameManager.Instance.PlayerClass;
    }

    private void Start()
    {
        // 보더들 찾기
        Transform child = transform.GetChild(0).GetChild(0);
        borders[CharacterClass.Fighter] = child.GetComponent<Image>();

        child = transform.GetChild(1).GetChild(0);
        borders[CharacterClass.Berserker] = child.GetComponent<Image>();

        child = transform.GetChild(2).GetChild(0);
        borders[CharacterClass.Hunter] = child.GetComponent<Image>();

        child = transform.GetChild(3).GetChild(0);
        borders[CharacterClass.Magician] = child.GetComponent<Image>();

        child = transform.GetChild(4).GetChild(0);
        borders[CharacterClass.Assassin] = child.GetComponent<Image>();

        // canvasGroup 초기화
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.WeaponChange.performed += OnChange;
        inputActions.UI.WeaponChange.canceled += OffChange;

    }

    private void OnDisable()
    {
        inputActions.UI.WeaponChange.canceled -= OffChange;
        inputActions.UI.WeaponChange.performed -= OnChange;
        inputActions.UI.Disable();
    }

    /// <summary>
    /// Shift키 눌렀을 때
    /// </summary>
    private void OnChange(InputAction.CallbackContext _)
    {
        // 클래스 바꾸는 창 보여주기
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Shift키 뗐을 때
    /// </summary>
    private void OffChange(InputAction.CallbackContext _)
    {
        // 클래스 바꾸는 창 안보여주기
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 플레이어 클래스를 바꾸는 함수
    /// </summary>
    /// <param name="nextClass">바꿀 클래스</param>
    public void ChangeClass(CharacterClass nextClass)
    {
        // 지금 클래스와 바꿀 클래스가 다르다면
        if (playerClass.CurrentClass != nextClass)
        {
            // 클래스 바꾸기
            borders[playerClass.CurrentClass].enabled = false;
            borders[nextClass].enabled = true;
            playerClass.CurrentClass = nextClass;
        }
    }
}
