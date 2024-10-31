using UnityEngine;

/// <summary>
/// 무기가 상속받는 인터페이스
/// </summary>
public interface IEquipable
{
    /// <summary>
    /// 무기를 장비하는 함수
    /// </summary>
    /// <param name="target">장비받을 대상</param>
    GameObject Equip(GameObject target);

    /// <summary>
    /// 무기를 해제하는 함수
    /// </summary>
    /// <param name="target">장비 해제할 대상</param>
    void UnEquip(GameObject target);
}