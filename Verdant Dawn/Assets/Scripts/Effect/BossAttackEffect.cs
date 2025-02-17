using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackEffect : RecycleObject
{
    /// <summary>
    /// 공격이 비활성화 되는 시간
    /// </summary>
    public float disableTime;

    /// <summary>
    /// 넉백되는 힘의 양
    /// </summary>
    public float knockBackPower;

    /// <summary>
    /// 이 이펙트의 모체
    /// </summary>
    public EnemyStatus enemyStatus;

    /// <summary>
    /// 이 이펙트가 가지는 콜라이더
    /// </summary>
    Collider colliderComponent;

    private void Awake()
    {
        // 컴포넌트들 찾기
        colliderComponent = GetComponent<Collider>();
    }

    protected override void OnReset()
    {
        // 1초 뒤면 사라지게 만들기
        DisableTimer(disableTime);

        // 콜라이더 활성화 시키기
        colliderComponent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 타겟 태그와 일치하는지 확인
        if (other.gameObject.CompareTag("Player"))
        {
            // 타겟에 데미지 처리
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Vector3 collisionPoint = other.ClosestPoint(transform.position);
                damageable.TakeDamage(enemyStatus.AttackPower, collisionPoint);
                damageable.KnockbackOnHit(knockBackPower);

                // 콜라이더 끄기(중복 충돌 방지)
                colliderComponent.enabled = false;
            }
        }
    }

}
