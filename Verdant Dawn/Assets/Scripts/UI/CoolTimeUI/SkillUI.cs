using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    /// <summary>
    /// 플레이어
    /// </summary>
    protected PlayerAttack player;

    // 컴포넌트들
    protected Image skillPanel;
    protected TextMeshProUGUI skillText;


    protected virtual void Awake()
    {
        player = GameManager.Instance.PlayerAttack;
        Transform child = transform.GetChild(0);
        skillPanel = child.GetComponent<Image>();
        child = transform.GetChild(1);
        skillText = child.GetComponent<TextMeshProUGUI>();
    }
}
