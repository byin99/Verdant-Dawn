using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_SkillEffect : RecycleObject
{
    /// <summary>
    /// 스킬 활성화 시간
    /// </summary>
    [SerializeField]
    float disableTime;

    /// <summary>
    /// 스킬 계수
    /// </summary>
    [SerializeField]
    float skillDamageRatio;

    /// <summary>
    /// 적을 미는 힘
    /// </summary>
    [SerializeField]
    float knockBackPower;

    /// <summary>
    /// 적이 맞고 나서 다시 맞을 때까지의 시간
    /// </summary>
    [SerializeField]
    float skillDelayTime;

    /// <summary>
    /// 데미지를 주는 이펙인지 설정하는 변수(true면 데미지를 줌, false면 데미지를 주지 않음)
    /// </summary>
    [SerializeField]
    bool isDamage;

    /// <summary>
    /// 적이 연속적으로 맞는지 설정하는 변수(true면 연속적으로 맞음, false면 연속적으로 맞지 않음)
    /// </summary>
    [SerializeField]
    bool isContinuous;

    /// <summary>
    /// 애니메이션에서 Collider의 활성화 여부를 처리하면 true, 아니먄 false
    /// </summary>
    [SerializeField]
    bool isAnimation;

    /// <summary>
    /// R스킬 코루틴
    /// </summary>
    IEnumerator rSkillCoroutine;

    // 컴포넌트들
    PlayerStatus status;
    Collider colliderComponent;

    private void Awake()
    {
        if (isDamage)
        {
            colliderComponent = GetComponent<Collider>();
            status = GameManager.Instance.PlayerStatus;
        }
    }

    protected override void OnReset()
    {
        DisableTimer(disableTime);

        if (isDamage)
        {
            colliderComponent.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDamage && other.gameObject.CompareTag("Enemy"))
        {
            // 지속적인 데미지를 줄 수 있는 이펙트라면 코루틴 시작
            if (isContinuous)
            {
                rSkillCoroutine = ContinuousDamage(other);
                StartCoroutine(rSkillCoroutine);
            }

            else
            {
                // 타겟에 데미지 처리
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector3 collisionPoint = other.ClosestPoint(transform.position);
                    damageable.TakeDamage(status.AttackPower * skillDamageRatio, collisionPoint);
                    damageable.KnockbackOnHit(knockBackPower);

                    // 콜라이더 끄기(중복 충돌 방지)
                    if (!isAnimation)
                    {
                        colliderComponent.enabled = false;
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (rSkillCoroutine != null && isContinuous)
        {
            StopCoroutine(rSkillCoroutine);
        }
    }

    /// <summary>
    /// 지속적인 데미지를 주는 코루틴
    /// </summary>
    /// <param name="other">데미지를 받는 콜라이더</param>
    IEnumerator ContinuousDamage(Collider other)
    {
        while (true)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Vector3 collisionPoint = other.ClosestPoint(transform.position);
                damageable.TakeDamage(status.AttackPower * skillDamageRatio, collisionPoint);
                damageable.KnockbackOnHit(knockBackPower);
            }
            yield return new WaitForSeconds(skillDelayTime);
        }
    }
}
