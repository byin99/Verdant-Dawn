using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextGenerator_P : MonoBehaviour
{
    private void Start()
    {
        IDamageable damageable = transform.parent.GetComponent<IDamageable>();
        damageable.onHit += DamageTextGenerate;
    }

    /// <summary>
    /// 데미지 텍스트 소환
    /// </summary>
    /// <param name="damage">데미지 양</param>
    /// <param name="damagePoint">데미지 소환 위치</param>
    private void DamageTextGenerate(float damage, Vector3 damagePoint)
    {
        Factory.Instance.MakeDamageText_P((int)damage, damagePoint);
    }
}
