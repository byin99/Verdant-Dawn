using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollUI : MonoBehaviour, ICoolTime
{
    /// <summary>
    /// 플레이어
    /// </summary>
    PlayerMovement player;

    // 컴포넌트들
    CanvasGroup canvasGroup;
    Image rollPanel;
    TextMeshProUGUI rollText;


    private void Awake()
    {
        player = GameManager.Instance.PlayerMovement;
        canvasGroup = GetComponent<CanvasGroup>();
        Transform child = transform.GetChild(0);
        child = child.transform.GetChild(0);
        rollPanel = child.GetComponent<Image>();
        child = transform.GetChild(1);
        rollText = child.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        player.onRoll += ShowCoolTime;  // 델리게이트에 함수 연결하기
        canvasGroup.alpha = 0.0f;       // canvasGroup alpha값 조절하기
    }

    /// <summary>
    /// 구르기를 사용하면 실행되는 함수
    /// </summary>
    public void ShowCoolTime()
    {
        StartCoroutine(CoolTimeCoroutine());    // 구르기 코루틴 시작하기
    }

    /// <summary>
    /// 구르기 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoolTimeCoroutine()
    {
        canvasGroup.alpha = 1.0f;       // canvasGroup alpha값 조절하기
        while (player.RollRemainTime > 0.0f)    // 구르기 쿨타임 동안
        {
            rollText.text = $"{Mathf.CeilToInt(player.RollRemainTime)}s";  // 남은 시간 보여주기
            rollPanel.fillAmount = (player.RollRemainTime / player.rollCoolTime);   // 시간 만큼 Panel Image조절하기
            yield return null;
        }
        canvasGroup.alpha = 0.0f;       // canvasGroup alpha값 조절하기
    }
}
