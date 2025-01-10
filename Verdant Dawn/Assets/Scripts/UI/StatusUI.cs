using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StatusUI : MonoBehaviour
{
    /// <summary>
    /// 스테이터스 창을 닫는 버튼
    /// </summary>
    Button closeButton;

    /// <summary>
    /// 직업 텍스트
    /// </summary>
    TextMeshProUGUI classText;

    /// <summary>
    /// 레벨 텍스트
    /// </summary>
    TextMeshProUGUI levelText;

    /// <summary>
    /// 공격력 텍스트
    /// </summary>
    TextMeshProUGUI attackText;

    /// <summary>
    /// 방어력 텍스트
    /// </summary>
    TextMeshProUGUI defenseText;

    /// <summary>
    /// 체력 텍스트
    /// </summary>
    TextMeshProUGUI healthText;

    /// <summary>
    /// 마나 텍스트
    /// </summary>
    TextMeshProUGUI manaText;

    /// <summary>
    /// 스테이터스 창이 열려있는지 닫혀있는지 기록하는 변수(true면 열려있음, false면 닫혀있음)
    /// </summary>
    bool isOpenStatusUI;

    // 컴포넌트들
    PlayerStatus playerStatus;
    PlayerClass playerClass;
    CanvasGroup canvasGroup;
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        playerStatus = GameManager.Instance.PlayerStatus;
        playerClass = GameManager.Instance.PlayerClass;

        Transform child = transform.GetChild(0);
        closeButton = child.GetComponent<Button>();

        child = transform.GetChild(1);
        classText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        levelText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        attackText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(4);
        defenseText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(5);
        healthText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(6);
        manaText = child.GetComponent<TextMeshProUGUI>();

        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        // 스테이스터스 창 닫기
        CloseStatusUI();
        
        // 스테이터스 업데이트용 함수 연결하기
        playerStatus.onStatus += UpdateStatus;

        // 클릭했을 때 창이 닫히게 만들기
        closeButton.onClick.AddListener(CloseStatusUI);
    }

    private void OnEnable()
    {
        playerInputActions.UI.Status.Enable();
        playerInputActions.UI.Status.performed += OnOffStatus;
    }

    private void OnDisable()
    {
        playerInputActions.UI.Status.performed -= OnOffStatus;
        playerInputActions.UI.Status.Disable();
    }

    /// <summary>
    /// P키
    /// </summary>
    private void OnOffStatus(InputAction.CallbackContext _)
    {
        if (isOpenStatusUI)
        {
            CloseStatusUI();
        }
        else
        {
            ShowStatusUI();
        }
    }

    /// <summary>
    /// 스테이터스 창을 여는 함수
    /// </summary>
    void ShowStatusUI()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        isOpenStatusUI = true;
        UpdateStatus();
    }

    /// <summary>
    /// 스테이터스 창을 닫는 함수
    /// </summary>
    void CloseStatusUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        isOpenStatusUI = false;
    }

    /// <summary>
    /// 스테이터스를 업데이트 하는 함수
    /// </summary>
    void UpdateStatus()
    {
        classText.text = $"{playerClass.CurrentClass.ToString()}";
        levelText.text = $"{playerStatus.Level}";
        attackText.text = $"{playerStatus.AttackPower:f0}";
        defenseText.text = $"{playerStatus.DefensePower:f0}";
        healthText.text = $"{playerStatus.MaxHP:f0}";
        manaText.text = $"{playerStatus.MaxMP:f0}";
    }

}
