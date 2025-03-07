using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackEffect : RecycleObject
{
    /// <summary>
    /// 공격이 비활성화 되는 시간
    /// </summary>
    [SerializeField]
    float disableTime;

    /// <summary>
    /// 넉백되는 힘의 양
    /// </summary>
    [SerializeField]
    float knockBackPower;

    /// <summary>
    /// 최소 공격 계수
    /// </summary>
    [SerializeField]
    float minimumAttackRatio;

    /// <summary>
    /// 최대 공격 계수
    /// </summary>
    [SerializeField]
    float maximumAttackRatio;

    /// <summary>
    /// 넉백이 발생하는지 여부를 알려주는 변수
    /// </summary>
    [SerializeField]
    bool isKnockBack;

    /// <summary>
    /// 애니메이션으로 콜라이더를 처리하는지 여부
    /// </summary>
    [SerializeField]
    bool isAnimation;

    /// <summary>
    /// 이 이펙트 모체의 Status
    /// </summary>
    [SerializeField]
    BossStatus bossStatus;

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
        if (!isAnimation)
            colliderComponent.enabled = true;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        colliderComponent.enabled = false;
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
                float attackRatio = Random.Range(minimumAttackRatio, maximumAttackRatio);
                damageable.TakeDamage(bossStatus.AttackPower * attackRatio, collisionPoint);

                if (isKnockBack)
                    damageable.KnockbackOnHit(knockBackPower);

                // 콜라이더 끄기(중복 충돌 방지)
                if (!isAnimation)
                    colliderComponent.enabled = false;
            }
        }
    }

}
