using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이덴티티 포션 아이템용 ItemData(플레이어의 아이덴티티 게이지를 채워줌)
/// </summary>
[CreateAssetMenu(fileName = "New Item Data - HealingPotion", menuName = "Scriptable Object/Item Data - IdentityPotion", order = 3)]
public class ItemData_IdentityPotion : ItemData, IUsable
{
    /// <summary>
    /// 아이템 사용 함수(아이덴티티 포션)
    /// </summary>
    /// <param name="target">아이덴티티 포션을 사용할 주체</param>
    /// <returns>사용 성공 여부</returns>
    public bool Use(GameObject target)
    {
        bool result = false;

        IUsablePotion player = target.GetComponent<IUsablePotion>();
        if (player != null)
        {
            player.FillIdentityGauge();    // 아이덴티티 게이지를 채워줌
            Factory.Instance.GetIdentityPotion();
            result = true;
        }

        return result;
    }
}
