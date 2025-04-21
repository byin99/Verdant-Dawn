using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    /// <summary>
    /// 보스 HP바 색깔 Material
    /// </summary>
    public Material[] bossHPBarColor;

    /// <summary>
    /// 보스 HP가 바닥났을 때 보여주는 Sprite
    /// </summary>
    public Sprite bossHPBarEmpty;

    /// <summary>
    /// 보스 HP바
    /// </summary>
    Image bossHPBar;

    /// <summary>
    /// 보스 HP바 배경
    /// </summary>
    Image bossHPBarBackground;

    /// <summary>
    /// 보스 HP UI의 GlowBar
    /// </summary>
    Image bossGlowBar;

    /// <summary>
    /// 보스 HP 슬라이더
    /// </summary>
    Slider bossHPSlider;

    /// <summary>
    /// 보스 HP를 보여주는 Text
    /// </summary>
    TextMeshProUGUI bossHealthPoint;

    /// <summary>
    /// 보스 분할 HP 개수를 보여주는 Text
    /// </summary>
    TextMeshProUGUI bossHPNumber;

    /// <summary>
    /// 보스 분할 HP
    /// </summary>
    float bossSplitHP;

    /// <summary>
    /// 데미지를 입기 전 보스 HP
    /// </summary>
    float currentBossHP;

    /// <summary>
    /// 보스 분할 HP 개수
    /// </summary>
    int bossSplitHPNumber;

    /// <summary>
    /// 보스 HP바 인덱스
    /// </summary>
    int bossHPBarIndex = 1;

    /// <summary>
    /// 보스 HP를 바꾸는 코루틴 저장용 변수
    /// </summary>
    IEnumerator bossHPChangeCoroutine;

    // 컴포넌트들
    [HideInInspector]
    public BossStatus bossStatus;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        Transform child = transform.GetChild(0).GetChild(2).GetChild(0);
        bossHPBar = child.GetComponent<Image>();

        child = transform.GetChild(0).GetChild(1);
        bossHPBarBackground = child.GetComponent<Image>();

        child = transform.GetChild(0).GetChild(3);
        bossGlowBar = child.GetComponent<Image>();

        child = transform.GetChild(0);
        bossHPSlider = child.GetComponent<Slider>();

        child = transform.GetChild(0).GetChild(4);
        bossHealthPoint = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(0).GetChild(4).GetChild(0);
        bossHPNumber = child.GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        bossGlowBar.enabled = false;
    }

    /// <summary>
    /// 보스 UI를 보여주는 함수
    /// </summary>
    public void ShowBossHPBarUI()
    {
        InitializeUI();
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 보스 UI를 끄는 함수
    /// </summary>
    public void OffBossHPBarUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// UI 초기화
    /// </summary>
    void InitializeUI()
    {
        currentBossHP = bossStatus.MaxHP;
        bossSplitHP = bossStatus.MaxHP / 300;
        bossHPSlider.maxValue = bossSplitHP;
        bossHPSlider.value = bossSplitHP;
        bossSplitHPNumber = (int)(bossStatus.HP / bossSplitHP);
        bossStatus.onHealthChange += BossHPUIUpdate;
        bossHealthPoint.text = $"{(int)currentBossHP} / {(int)bossStatus.MaxHP}";
    }

    /// <summary>
    /// 보스 HP UI를 업데이트 함수
    /// </summary>
    /// <param name="hp">보스의 HP</param>
    void BossHPUIUpdate(float hp)
    { 
        // 보스 슬라이더 업데이트 하기
        if (bossHPChangeCoroutine != null)
            StopCoroutine(bossHPChangeCoroutine);
        bossHPChangeCoroutine = BossHPChangeCoroutine(hp);
        StartCoroutine(bossHPChangeCoroutine);
    }

    /// <summary>
    /// 보스 HP바 업데이트 함수
    /// </summary>
    void BossHPBarUpdate()
    {
        if (!bossStatus.IsAlive)
        {
            bossHPSlider.value = 0.0f;
            bossHPNumber.text = null;
            bossHPBarBackground.material = null;
            bossHPBarBackground.sprite = bossHPBarEmpty;
            bossHealthPoint.text = $"0 / {(int)bossStatus.MaxHP}";
            return;
        }

        // 보스 HP Text 업데이트 하기
        bossHealthPoint.text = $"{(int)currentBossHP} / {(int)bossStatus.MaxHP}";
        bossSplitHPNumber = (int)(currentBossHP / bossSplitHP);
        bossHPNumber.text = $"x{bossSplitHPNumber}";

        ChangeHPBar();

        if (bossSplitHPNumber == 1)
            bossHPBarBackground.material = bossHPBarColor[0];

        else if (bossSplitHPNumber == 0)
        {
            bossHPNumber.text = null;
            bossHPBar.material = bossHPBarColor[0];
            bossHPBarBackground.material = null;
            bossHPBarBackground.sprite = bossHPBarEmpty;
        }
    }

    void ChangeHPBar()
    {
        bossHPBar.material = bossHPBarColor[bossHPBarIndex];

        bossHPBarIndex++;
        bossHPBarIndex %= 6;

        bossHPBarBackground.material = bossHPBarColor[bossHPBarIndex];
    }

    /// <summary>
    /// 보스 HP를 바꾸는 코루틴
    /// </summary>
    /// <param name="hp">보스의 현재 HP</param>
    IEnumerator BossHPChangeCoroutine(float hp)
    {
        bossGlowBar.enabled = true;
        float deltaHP = currentBossHP - hp;

        while (currentBossHP > hp)
        {
            float deltaSplitHP = deltaHP / 10;

            if (bossHPSlider.value < deltaSplitHP)
            {
                currentBossHP -= deltaSplitHP;
                bossHPSlider.value = currentBossHP % bossSplitHP;
                BossHPBarUpdate();
            }

            else
            {
                currentBossHP -= deltaSplitHP;
                bossHPSlider.value = currentBossHP % bossSplitHP;
            }
            yield return null;
        }

        currentBossHP = hp;
        bossHPSlider.value = currentBossHP % bossSplitHP;
        bossGlowBar.enabled = false;
    }

    /// <summary>
    /// 보스 UI끄는 코루틴
    /// </summary>
    public IEnumerator OffBossUICoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        OffBossHPBarUI();
    }
}
