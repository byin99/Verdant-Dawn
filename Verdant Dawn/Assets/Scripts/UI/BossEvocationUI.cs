using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossEvocationUI : MonoBehaviour
{
    /// <summary>
    /// 목표까지 남은 수를 보여주는 Text
    /// </summary>
    TextMeshProUGUI targetCountText;

    /// <summary>
    /// 남은 시간을 보여주는 Text
    /// </summary>
    TextMeshProUGUI timerText;

    /// <summary>
    /// 타이머가 빨간색인지 아닌지
    /// </summary>
    bool isRed = false;

    // 컴포넌트들
    CanvasGroup canvasGroup;

    private void Awake()
    {
        Transform child = transform.GetChild(4);
        targetCountText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(5);
        timerText = child.GetComponent<TextMeshProUGUI>();

        canvasGroup = transform.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        OffBossEvocationUI();
    }

    /// <summary>
    /// 타이머를 업데이트 하는 함수
    /// </summary>
    /// <param name="currentTime">현재 시간</param>
    public void UpdateTimer(float currentTime)
    {
        if (!isRed && currentTime < 10.0f)
        {
            timerText.color = Color.red;
            isRed = true;
        }

        timerText.text = $"{currentTime:F2}";
    }

    /// <summary>
    /// 소환수 카운트를 업데이트 하는 함수
    /// </summary>
    /// <param name="currentCount">현재 잡은 소환수 수</param>
    /// <param name="maxCount">총 소환수 수</param>
    public void UpdateTargetCount(int currentCount, int maxCount)
    {
        targetCountText.text = $"{currentCount} / {maxCount}";
    }

    /// <summary>
    /// Evocation UI를 보여주는 함수
    /// </summary>
    public void ShowBossEvocationUI()
    {
        canvasGroup.alpha = 1.0f; 
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        timerText.color = Color.white;
        isRed = false;
    }

    /// <summary>
    /// Evocation UI를 끄는 함수
    /// </summary>
    public void OffBossEvocationUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
