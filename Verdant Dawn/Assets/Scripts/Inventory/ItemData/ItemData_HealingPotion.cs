using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 힐링포션 아이템용 ItemData(최대 HP에 비례한 즉시회복)
/// </summary>
[CreateAssetMenu(fileName = "New Item Data - HealingPotion", menuName = "Scriptable Object/Item Data/HealingPotion", order = 0)]
public class ItemData_HealingPotion : ItemData, IUsable
{
    [Header("힐링 포션 데이터")]

    /// <summary>
    /// 최대 HP에 비례해서 즉시 회복시켜 주는 양
    /// </summary>
    public float healRatio = 0.5f;

    /// <summary>
    /// 아이템 사용 함수(힐링 포션)
    /// </summary>
    /// <param name="target">포션을 사용할 주체</param>
    /// <returns>사용 성공 여부</returns>
    public bool Use(GameObject target)
    {
        bool result = false;

        IUsablePotion player = target.GetComponent<IUsablePotion>();
        if (player != null)
        {
            if (player.CanHeal)
            {
                GameManager.Instance.AudioManager.PlaySound2D(AudioCode.ItemUse);
                player.HealthHeal(healRatio);    // 최대 HP의 healRatio만큼 즉시 증가
                Factory.Instance.GetHealingPotion();
                result = true;
            }
        }

        return result;
    }
}