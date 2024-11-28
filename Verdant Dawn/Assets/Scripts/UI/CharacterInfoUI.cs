using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterInfoUI : MonoBehaviour
{
    [Header("직업 아이콘")]
    /// <summary>
    /// 직업 아이콘
    /// </summary>
    public Sprite[] characterClassImages;

    [Header("직업별 아이덴티티 게이지 아이콘")]
    /// <summary>
    /// 직업별 아이덴티티 게이지 아이콘
    /// </summary>
    public Sprite[] characterClassColors;

    [Header("직업별 아이덴티티 게이지 메테리얼")]
    /// <summary>
    /// 직업별 아이덴티티 게이지 메테리얼
    /// </summary>
    public Material[] characterClassMaterials;

    [Header("직업별 W스킬 아이콘")]
    /// <summary>
    /// 직업별 W스킬 아이콘
    /// </summary>
    public Sprite[] WSkillIconImages;

    [Header("직업별 E스킬 아이콘")]
    /// <summary>
    /// 직업별 E스킬 아이콘
    /// </summary>
    public Sprite[] ESkillIconImages;

    [Header("직업별 R스킬 아이콘")]
    /// <summary>
    /// 직업별 R스킬 아이콘
    /// </summary>
    public Sprite[] RSkillIconImages;

    // 바꾸기 위한 이미지들
    Image character_Class_Image;
    Image character_Class_Color;
    Image w_Skill_Image;
    Image e_Skill_Image;
    Image r_Skill_Image;

    // 버튼을 누를 때 나오는 이펙트
    Image w_Glow;
    Image e_Glow;
    Image r_Glow;
    PlayerInputActions inputActions;


    private void Awake()
    {
        // 각각 컴포넌트들 찾기
        PlayerClass playerClass = GameManager.Instance.PlayerClass;
        playerClass.onChangeClass += ChangeClassUI;

        Transform child = transform.GetChild(2);
        child = child.GetChild(1);
        character_Class_Image = child.GetComponent<Image>();

        child = transform.GetChild(2);
        child = child.GetChild(0);
        child = child.GetChild(1);
        child = child.GetChild(0);
        character_Class_Color = child.GetComponent<Image>();

        child = transform.GetChild(0);
        child = child.GetChild(0);
        child = child.GetChild(0);
        w_Skill_Image = child.GetComponent<Image>();

        child = transform.GetChild(0);
        child = child.GetChild(1);
        child = child.GetChild(0);
        e_Skill_Image = child.GetComponent<Image>();

        child = transform.GetChild(0);
        child = child.GetChild(2);
        child = child.GetChild(0);
        r_Skill_Image = child.GetComponent<Image>();

        child = transform.GetChild(0);
        child = child.GetChild(0);
        child = child.GetChild(0);
        child = child.GetChild(0);
        w_Glow = child.GetComponent<Image>();

        child = transform.GetChild(0);
        child = child.GetChild(1);
        child = child.GetChild(0);
        child = child.GetChild(0);
        e_Glow = child.GetComponent<Image>();

        child = transform.GetChild(0);
        child = child.GetChild(2);
        child = child.GetChild(0);
        child = child.GetChild(0);
        r_Glow = child.GetComponent<Image>();

        // UI용 InputActions 만들기
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        w_Glow.enabled = false;   
        e_Glow.enabled = false;   
        r_Glow.enabled = false;   
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.WButton.performed += OnWButton;
        inputActions.UI.WButton.canceled += OffWButton;
        inputActions.UI.EButton.performed += OnEButton;
        inputActions.UI.EButton.canceled += OffEButton;
        inputActions.UI.RButton.performed += OnRButton;
        inputActions.UI.RButton.canceled += OffRButton;
    }

    private void OnDisable()
    {
        inputActions.UI.RButton.canceled -= OffRButton;
        inputActions.UI.RButton.performed -= OnRButton;
        inputActions.UI.EButton.canceled -= OffEButton;
        inputActions.UI.EButton.performed -= OnEButton;
        inputActions.UI.WButton.canceled -= OffWButton;
        inputActions.UI.WButton.performed -= OnWButton;
        inputActions.UI.Disable();
    }

    /// <summary>
    /// w키 누를 때
    /// </summary>
    private void OnWButton(InputAction.CallbackContext _)
    {
        w_Glow.enabled = true;
    }

    /// <summary>
    /// w키 뗐을 때
    /// </summary>
    private void OffWButton(InputAction.CallbackContext _)
    {
        w_Glow.enabled = false;
    }

    /// <summary>
    /// e키 누를 때
    /// </summary>
    private void OnEButton(InputAction.CallbackContext _)
    {
        e_Glow.enabled = true;
    }

    /// <summary>
    /// e키 뗐을 때
    /// </summary>
    private void OffEButton(InputAction.CallbackContext _)
    {
        e_Glow.enabled = false;
    }

    /// <summary>
    /// r키 누를 때
    /// </summary>
    private void OnRButton(InputAction.CallbackContext _)
    {
        r_Glow.enabled = true;
    }

    /// <summary>
    /// r키 뗐을 때
    /// </summary>
    private void OffRButton(InputAction.CallbackContext _)
    {
        r_Glow.enabled = false;
    }

    /// <summary>
    /// 클래스에 따라서 이미지를 변경하는 함수
    /// </summary>
    void ChangeClassUI(CharacterClass currentClass)
    {
        switch (currentClass)
        {
            case CharacterClass.Fighter:
                ChangeClassNum(0);
                break;
            case CharacterClass.Berserker:
                ChangeClassNum(1);
                break;
            case CharacterClass.Hunter:
                ChangeClassNum(2);
                break;
            case CharacterClass.Magician:
                ChangeClassNum(3);
                break;
            case CharacterClass.Assassin:
                ChangeClassNum(4);
                break;
        }
    }

    /// <summary>
    /// 클래스별로 UI를 바꾸는 함수
    /// </summary>
    /// <param name="class_num">클래스 번호</param>
    void ChangeClassNum(int class_num)
    {
        character_Class_Image.sprite = characterClassImages[class_num];
        character_Class_Color.sprite = characterClassColors[class_num];
        character_Class_Color.material = characterClassMaterials[class_num];
        w_Skill_Image.sprite = WSkillIconImages[class_num];
        e_Skill_Image.sprite = ESkillIconImages[class_num];
        r_Skill_Image.sprite = RSkillIconImages[class_num];
    }
}
