using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_SkillUI : SkillUI
{
    private void Start()
    {
        player.finishUltimate += ShowCoolTime;
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

        while (player.UltimateRemainTime > 0)
        {
            skillText.text = $"{Mathf.CeilToInt(player.UltimateRemainTime)}s";
            skillPanel.fillAmount = (player.UltimateRemainTime / player.ultimateCoolTime);
            yield return null;
        }

        skillPanel.enabled = false;
        skillText.enabled = false;
    }
}
