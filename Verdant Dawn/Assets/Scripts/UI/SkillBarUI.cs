using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : MonoBehaviour
{
    /// <summary>
    /// 스킬 사용이 끝나고 기다려주는 시간
    /// </summary>
    public float showUITime = 0.2f;

    // 컴포넌트들
    PlayerAttack attack;
    CanvasGroup canvasGroup;
    Slider slider;
    TextMeshProUGUI timeText;
    TextMeshProUGUI successText;
    TextMeshProUGUI failText;

    IEnumerator chargingSkill = null;

    private void Awake()
    {
        attack = GameManager.Instance.PlayerAttack;
        canvasGroup = GetComponent<CanvasGroup>();

        Transform child = transform.GetChild(0);
        slider = child.GetComponent<Slider>();

        child = transform.GetChild(1);
        timeText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        successText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        failText = child.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// W스킬 시작하면 실행되는 함수
    /// </summary>
    public void StartChargingSkill()
    {
        if (chargingSkill != null)
        {
            StopCoroutine(chargingSkill);
        }
        chargingSkill = StartCharging();
        StartCoroutine(chargingSkill);
    }

    /// <summary>
    /// W스킬 끝나면 실행되는 함수
    /// </summary>
    public void StopChargingSKill(bool isChargeSuccessful)
    {
        if (chargingSkill != null)
        {
            StopCoroutine(chargingSkill);
        }
        chargingSkill = StopCharging(isChargeSuccessful);
        StartCoroutine(chargingSkill);
    }

    /// <summary>
    /// W스킬(차징스킬)을 사용하면 실행되는 코루틴 
    /// </summary>
    IEnumerator StartCharging()
    {
        successText.gameObject.SetActive(false);
        failText.gameObject.SetActive(false);
        canvasGroup.alpha = 1.0f;
        slider.value = 0.0f;
        while (slider.value < 1.0f)
        {
            timeText.text = $"{attack.ChargingTimeElapsed:f1}초";
            slider.value = attack.ChargingTimeElapsed / attack.chargingTime;
            yield return null;
        }
        slider.value = 1.0f;
        chargingSkill = null;
    }

    /// <summary>
    /// W스킬(차징스킬)을 멈추면 실행되는 코루틴 
    /// </summary>
    IEnumerator StopCharging(bool isChargeSuccessful)
    {
        if (isChargeSuccessful)
        {
            successText.gameObject.SetActive(true);
        }
        else
        {
            failText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(showUITime);

        while (canvasGroup.alpha > 0.1f)
        {
            canvasGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
        chargingSkill = null;
    }

    
}
