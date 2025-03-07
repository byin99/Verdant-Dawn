using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : RecycleObject
{
    /// <summary>
    /// 최소 공격 계수
    /// </summary>
    public float minimumAttackRatio;

    /// <summary>
    /// 최대 공격 계수
    /// </summary>
    public float maximumAttackRatio;

    /// <summary>
    /// 무기 기본공격 마다 미는힘
    /// </summary>
    public float knockBackPower;

    // 컴포넌트들
    PlayerStatus status;

    // 이미 맞은 적을 추적하는 HashSet
    HashSet<IDamageable> damagedEnemies = new HashSet<IDamageable>();

    private void Awake()
    {
        // 컴포넌트들 찾기
        status = GameManager.Instance.PlayerStatus;
    }

    
    protected override void OnReset()
    {
        // 1초 뒤면 사라지게 만들기
        DisableTimer(1.0f);

        damagedEnemies.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 타겟 태그와 일치하는지 확인
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 타겟에 데미지 처리
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            // 이미 맞은 적이면 무시
            if (damageable == null || damagedEnemies.Contains(damageable))
                return;

            // 맞은 적을 HashSet에 추가
            damagedEnemies.Add(damageable);

            Vector3 collisionPoint = other.ClosestPoint(transform.position);
            float attackRatio = Random.Range(minimumAttackRatio, maximumAttackRatio);
            damageable.TakeDamage(status.AttackPower * attackRatio, collisionPoint);
            damageable.KnockbackOnHit(knockBackPower);
        }
    }
}
