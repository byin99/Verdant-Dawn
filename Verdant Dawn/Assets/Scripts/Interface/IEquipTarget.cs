using UnityEngine;

/// <summary>
/// 무기를 들 수 있는 오브젝트가 상속받는 인터페이스
/// </summary>
public interface IEquipTarget
{
    /// <summary>
    /// 무기를 장비하는 함수
    /// </summary>
    /// <param name="part">장비할 부위</param>
    public GameObject EquipItem(EquipType part, Weapon weapon);

    /// <summary>
    /// 무기를 해제하는 함수
    /// </summary>
    /// <param name="part">해제할 부위</param>
    public void UnEquipItem(EquipType part);

    /// <summary>
    /// 무기를 장비하거나 해제할 부위의 Transform을 반환하는 함수
    /// </summary>
    /// <param name="part">장비하거나 해제할 부위</param>
    /// <returns>부위의 Transform</returns>
    public Transform GetEquipParentTransform(EquipType part);
}
