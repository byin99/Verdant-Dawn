using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 마나포션 아이템용 ItemData(일정 시간동안 최대 MP양을 늘려줌)
/// </summary>
[CreateAssetMenu(fileName = "New Item Data - HealingPotion", menuName = "Scriptable Object/Item Data/ManaPotion", order = 1)]
public class ItemData_ManaPotion : ItemData, IUsable
{
    [Header("마나 포션 데이터")]

    /// <summary>
    /// 최대 마나 증가율
    /// </summary>
    public float maxManaRatio = 0.5f;

    /// <summary>
    /// 버프 지속시간
    /// </summary>
    public float buffTime = 300.0f;

    /// <summary>
    /// 아이템 사용 함수(마나 포션)
    /// </summary>
    /// <param name="target">포션을 사용할 주체</param>
    /// <returns>사용 성공 여부</returns>
    public bool Use(GameObject target)
    {
        bool result = false;

        IUsablePotion player = target.GetComponent<IUsablePotion>();
        if (player != null)
        {
            ItemData itemData = GameManager.Instance.ItemDataManager[ItemCode.ManaPotion];
            player.BoostMaxMana(itemData, maxManaRatio, buffTime);    // 최대 MP의 maxManaRatio만큼 증가
            Factory.Instance.GetManaPotion();
            result = true;
        }

        return result;
    }
}
