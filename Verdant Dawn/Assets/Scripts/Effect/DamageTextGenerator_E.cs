using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextGenerator_E : MonoBehaviour
{
    private void Start()
    {
        IDamageable damageable = transform.parent.GetComponent<IDamageable>();
        damageable.onHit += DamageEffectGenerate;
    }

    /// <summary>
    /// 데미지 텍스트, 이펙트 소환
    /// </summary>
    /// <param name="damage">데미지 양</param>
    private void DamageEffectGenerate(float damage, Vector3 damagePoint)
    {
        Factory.Instance.MakeDamageText_E((int)damage, damagePoint);
        Factory.Instance.GetHitEnemyEffect(damagePoint);
    }
}
