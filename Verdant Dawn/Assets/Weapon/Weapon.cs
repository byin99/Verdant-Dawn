using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Asset", menuName = "Weapon Asset", order = 0)]
public class Weapon : ScriptableObject, IEquipable
{
    /// <summary>
    /// 무기 프리펩
    /// </summary>
    public GameObject weapon;

    /// <summary>
    /// 무기 장비 부위
    /// </summary>
    public EquipType equipType;

    /// <summary>
    /// 무기 생성 위치
    /// </summary>
    public Transform weaponGenerationTransform;

    /// <summary>
    /// 무기를 장비하는 함수
    /// </summary>
    /// <param name="target">무기를 장비하는 타겟</param>
    public GameObject Equip(GameObject target)
    {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            return equipTarget.EquipItem(equipType, this);
        }
        return null;
    }

    /// <summary>
    /// 무기를 해제하는 함수
    /// </summary>
    /// <param name="target">무기를 장비하거나 해제하는 타겟</param>
    public void UnEquip(GameObject target)
    {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            equipTarget.UnEquipItem(equipType);
        }
    }
}
