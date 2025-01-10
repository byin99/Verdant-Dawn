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
    PlayerAttack player;


    private void Awake()
    {
        // 각각 컴포넌트들 찾기
        PlayerClass playerClass = GameManager.Instance.PlayerClass;
        playerClass.onChangeClass += ChangeClassUI;

        Transform child = transform.GetChild(3);
        child = child.GetChild(1);
        character_Class_Image = child.GetComponent<Image>();

        child = transform.GetChild(3);
        child = child.GetChild(0);
        child = child.GetChild(1);
        child = child.GetChild(0);
        character_Class_Color = child.GetComponent<Image>();

        child = transform.GetChild(1);
        child = child.GetChild(0);
        child = child.GetChild(0);
        w_Skill_Image = child.GetComponent<Image>();

        child = transform.GetChild(1);
        child = child.GetChild(1);
        child = child.GetChild(0);
        e_Skill_Image = child.GetComponent<Image>();

        child = transform.GetChild(1);
        child = child.GetChild(2);
        child = child.GetChild(0);
        r_Skill_Image = child.GetComponent<Image>();

        child = transform.GetChild(1);
        child = child.GetChild(0);
        child = child.GetChild(0);
        child = child.GetChild(0);
        w_Glow = child.GetComponent<Image>();

        child = transform.GetChild(1);
        child = child.GetChild(1);
        child = child.GetChild(0);
        child = child.GetChild(0);
        e_Glow = child.GetComponent<Image>();

        child = transform.GetChild(1);
        child = child.GetChild(2);
        child = child.GetChild(0);
        child = child.GetChild(0);
        r_Glow = child.GetComponent<Image>();

        player = GameManager.Instance.PlayerAttack;
    }

    private void Start()
    {
        w_Glow.enabled = false;   
        e_Glow.enabled = false;   
        r_Glow.enabled = false;   
    }

    private void OnEnable()
    {
        player.onCharge += OnChargeSkill;
        player.offCharge += OffChargeSkill;
        player.onComboSkill += OnComboSkill;
        player.offComboSkill += OffComboSkill;
        player.onUltimate += OnUltimateSkill;
        player.offUltimate += OffUltimateSkill;
    }

    private void OnDisable()
    {
        player.offUltimate -= OffUltimateSkill;
        player.onUltimate -= OnUltimateSkill;
        player.offComboSkill -= OffComboSkill;
        player.onComboSkill -= OnComboSkill;
        player.offCharge -= OffChargeSkill;
        player.onCharge -= OnChargeSkill;
    }

    /// <summary>
    /// W키 누를 때
    /// </summary>
    void OnChargeSkill()
    {
        w_Glow.enabled = true;
    }

    /// <summary>
    /// W키 뗐을 때
    /// </summary>
    void OffChargeSkill()
    {
        w_Glow.enabled = false;
    }

    /// <summary>
    /// E키 누를 때
    /// </summary>
    void OnComboSkill()
    {
        e_Glow.enabled = true;
    }

    /// <summary>
    /// E키 뗐을 때
    /// </summary>
    void OffComboSkill()
    {
        e_Glow.enabled = false;
    }

    /// <summary>
    /// R키 눌렀을 때
    /// </summary>
    void OnUltimateSkill()
    {
        r_Glow.enabled = true;
    }

    /// <summary>
    /// R키 뗐을 때
    /// </summary>
    void OffUltimateSkill()
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
