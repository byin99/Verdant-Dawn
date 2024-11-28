using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// SkillBarUI
    /// </summary>
    SkillBarUI skillBarUI;

    /// <summary>
    /// SKillBarUI를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public SkillBarUI SkillBarUI
    {
        get
        {
            if (skillBarUI == null)
            {
                skillBarUI = FindAnyObjectByType<SkillBarUI>();
            }
            return skillBarUI;
        }
    }

    protected override void OnInitialize()
    {
        skillBarUI = FindAnyObjectByType<SkillBarUI>(); // SkillBarUI 찾기
    }
}
