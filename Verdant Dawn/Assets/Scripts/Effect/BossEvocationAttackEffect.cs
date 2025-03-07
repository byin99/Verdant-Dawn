using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvocationAttackEffect : RecycleObject
{
    /// <summary>
    /// 공격이 비활성화 되는 시간
    /// </summary>
    [SerializeField]
    float disableTime;

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
    /// 이 이펙트의 모체
    /// </summary>
    [SerializeField]
    EnemyStatus enemyStatus;

    /// <summary>
    /// 자식 오브젝트의 움직임을 제어하는 컴포넌트
    /// </summary>
    RFX4_PhysicsMotion rFX4_PhysicsMotion;

    private void Awake()
    {
        // 컴포넌트들 찾기
        rFX4_PhysicsMotion = transform.GetChild(0).GetComponent<RFX4_PhysicsMotion>();
    }

    protected override void OnReset()
    {
        // 1초 뒤면 사라지게 만들기
        DisableTimer(disableTime);
        
        rFX4_PhysicsMotion.onTriggerEnter += OnTrigger;
    }

    /// <summary>
    /// 자식 오브젝트에서 Trigger가 발생했을 때 호출되는 함수
    /// </summary>
    /// <param name="other">Trigger한 콜라이더</param>
    void OnTrigger(Collider other)
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
                damageable.TakeDamage(enemyStatus.AttackPower * attackRatio, collisionPoint);
            }
        }
    }
}
