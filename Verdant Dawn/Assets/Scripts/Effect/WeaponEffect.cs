using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : RecycleObject
{
    /// <summary>
    /// 공격력 계수
    /// </summary>
    public float attackDamageRatio;

    /// <summary>
    /// 무기 기본공격 마다 미는힘
    /// </summary>
    public float knockBackPower;

    // 컴포넌트들
    PlayerStatus status;
    Collider colliderComponent;

    private void Awake()
    {
        // 컴포넌트들 찾기
        status = GameManager.Instance.PlayerStatus;
        colliderComponent = GetComponent<Collider>();
    }

    
    protected override void OnReset()
    {
        // 1초 뒤면 사라지게 만들기
        DisableTimer(1.0f);

        // 콜라이더 활성화 시키기
        colliderComponent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 타겟 태그와 일치하는지 확인
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 타겟에 데미지 처리
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Vector3 collisionPoint = other.ClosestPoint(transform.position);
                damageable.TakeDamage(status.AttackPower, collisionPoint);
                damageable.KnockbackOnHit(knockBackPower);

                // 콜라이더 끄기(중복 충돌 방지)
                colliderComponent.enabled = false;
            }
        }
    }
}
