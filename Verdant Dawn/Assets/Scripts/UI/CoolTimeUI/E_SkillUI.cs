using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_SkillUI : SkillUI, ICoolTime
{
    private void Start()
    {
        player.finishComboSkill += ShowCoolTime;
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

        while (player.ComboRemainTime > 0)
        {
            skillText.text = $"{Mathf.CeilToInt(player.ComboRemainTime)}s";
            skillPanel.fillAmount = (player.ComboRemainTime / player.comboCoolTime);
            yield return null;
        }

        skillPanel.enabled = false;
        skillText.enabled = false;
    }
}
