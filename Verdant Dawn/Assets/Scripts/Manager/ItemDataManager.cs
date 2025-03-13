using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    /// <summary>
    /// ItemData들
    /// </summary>
    public ItemData[] itemDatas;

    /// <summary>
    /// ItemData를 가져오기 위한 인덱서
    /// </summary>
    /// <param name="code">아이템 코드</param>
    /// <returns>아이템 코드가 맞는 ItemData</returns>
    public ItemData this[ItemCode code] => itemDatas[(int)code];

    /// <summary>
    /// ItemData를 가져오기 위한 인덱서
    /// </summary>
    /// <param name="index">인덱스</param>
    /// <returns>인덱스와 맞는 ItemData</returns>
    public ItemData this[uint index] => itemDatas[index];
}
