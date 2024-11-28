using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_SkillUI : SkillUI, ICoolTime
{
    private void Start()
    {
        player.offCharge += ShowCoolTime;
        skillText.enabled = false;
        skillPanel.enabled = false;
    }

    public void ShowCoolTime()
    {
        StartCoroutine(CoolTimeCoroutine());
    }

    public IEnumerator CoolTimeCoroutine()
    {
        skillText.enabled = true;
        skillPanel.enabled = true;

        while (player.ChargeRemainTime > 0.0f)
        {
            skillText.text = $"{Mathf.CeilToInt(player.ChargeRemainTime)}s";  
            skillPanel.fillAmount = (player.ChargeRemainTime / player.chargeCoolTime);
            yield return null;
        }

        skillPanel.enabled = false;
        skillText.enabled = false;
    }
}
