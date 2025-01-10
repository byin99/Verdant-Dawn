using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    /// <summary>
    /// 바가 바뀔 때마다 빛나는 시간
    /// </summary>
    public float glowTime;

    /// <summary>
    /// HP바
    /// </summary>
    Slider hpSlider;

    /// <summary>
    /// MP바
    /// </summary>
    Slider mpSlider;

    /// <summary>
    /// EXP바
    /// </summary>
    Slider expSlider;

    /// <summary>
    /// HP텍스트
    /// </summary>
    TextMeshProUGUI hpText;

    /// <summary>
    /// MP텍스트
    /// </summary>
    TextMeshProUGUI mpText;

    /// <summary>
    /// EXP텍스트
    /// </summary>
    TextMeshProUGUI expText;

    /// <summary>
    /// Level텍스트
    /// </summary>
    TextMeshProUGUI levelText;

    // 컴포넌트들
    PlayerStatus status;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        hpSlider = child.GetComponent<Slider>();
        child = child.GetChild(4);
        hpText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1);
        mpSlider = child.GetComponent<Slider>();
        child = child.GetChild(4);
        mpText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        expSlider = child.GetComponent<Slider>();
        child = child.GetChild(4);
        expText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        child = child.GetChild(5);
        levelText = child.GetComponent<TextMeshProUGUI>();

        status = GameManager.Instance.PlayerStatus;
        status.onHealthChange += ChangeHealthPoint;
        status.onManaChange += ChangeManaPoint;
        status.onExpChange += ChangeExperiencePoint;
        status.onLevelUp += ChangeLevelText;
    }

    private void Start()
    {
        ChangeLevelText();
    }

    /// <summary>
    /// HP바를 바꾸는 함수
    /// </summary>
    /// <param name="health">바뀌는 HP</param>
    void ChangeHealthPoint(float health)
    {
        StartCoroutine(GlowBar(hpSlider));
        hpSlider.value = health;
        hpText.text = $"{status.HP:f0} / {status.MaxHP:f0}";
    }

    /// <summary>
    /// MP바를 바꾸는 함수
    /// </summary>
    /// <param name="mana">바뀌는 MP</param>
    void ChangeManaPoint(float mana)
    {
        StartCoroutine(GlowBar(mpSlider));
        mpSlider.value = mana;
        mpText.text = $"{status.MP:f0} / {status.MaxMP:f0}";
    }

    /// <summary>
    /// EXP바를 바꾸는 함수
    /// </summary>
    /// <param name="experience">바뀌는 EXP</param>
    void ChangeExperiencePoint(float experience)
    {
        StartCoroutine(GlowBar(expSlider));
        expSlider.value = experience;
        expText.text = $"{experience:f0} %";
    }

    /// <summary>
    /// LevelText를 바꾸는 함수
    /// </summary>
    void ChangeLevelText()
    {
        levelText.text = $"Level {status.Level}";
    }

    /// <summary>
    /// HP나 MP바가 빛나게 만드는 코루틴
    /// </summary>
    /// <param name="bar">빛날 바</param>
    IEnumerator GlowBar(Slider bar)
    {
        // Bar빛나게 만들기
        Image glowBar = bar.transform.GetChild(3).GetComponent<Image>();
        glowBar.enabled = true;

        // 일정시간이 지난 후
        float timeElapsed = 0.0f;
        while (timeElapsed < glowTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Bar빛나지 않게 만들기
        glowBar.enabled = false;
    }
}
