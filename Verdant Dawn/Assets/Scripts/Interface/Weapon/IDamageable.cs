using System;
using UnityEngine;

/// <summary>
/// 데미지를 입는 오브젝트가 가지는 인터페이스
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// 데미지를 입었을 때 실행되는 델리게이트
    /// </summary>
    public event Action<float, Vector3> onHit;

    /// <summary>
    /// 데미지를 입었을 때 넉백되면 실행되는 델리게이트
    /// </summary>
    public event Action<float> onKnockBack;

    /// <summary>
    /// 데미지를 입는 함수
    /// </summary>
    /// <param name="damage">데미지 량</param>
    /// <param name="damagePoint">데미지 위치</param>
    void TakeDamage(float damage, Vector3 damagePoint);

    /// <summary>
    /// 데미지를 입었을 때 넉백시키는 힘을 전달하는 함수
    /// </summary>
    /// <param name="hitPower">넉백시키는힘의 양</param>
    void KnockbackOnHit(float hitPower);
}
