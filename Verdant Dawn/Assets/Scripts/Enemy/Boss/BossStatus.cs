using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour, IHealth, IBattle, IDamageable
{
    /// <summary>
    /// HP가 변하면 실행되는 델리게이트
    /// </summary>
    public event Action<float> onHealthChange;

    /// <summary>
    /// 보스가 죽으면 실행되는 델리게이트
    /// </summary>
    public event Action onDie;

    /// <summary>
    /// 보스가 맞으면 실행되는 델리게이트
    /// </summary>
    public event Action<float, Vector3> onHit;

    /// <summary>
    /// 넉백은 당하지 않음(실행되지 않는 델리게이트)
    /// </summary>
    public event Action<float> onKnockBack;

    [Header("보스 능력치")]
    /// <summary>
    /// 보스 현재 체력
    /// </summary>
    float hp;

    [SerializeField]
    /// <summary>
    /// 보스 최대 체력
    /// </summary>
    float maxHP;

    [SerializeField]
    /// <summary>
    /// 보스 공격력
    /// </summary>
    float attackPower;

    [SerializeField]
    /// <summary>
    /// 보스 방어력
    /// </summary>
    float defencePower;

    // 컴포넌트들
    Collider bossCollider;
    AudioManager audioManager;

    /// <summary>
    /// 보스의 HP를 확인하고 설정하기 위한 프로퍼티
    /// </summary>
    public float HP
    {
        get => hp;
        set
        {
            if (IsAlive)    // 살아있을 때만 적용
            {
                hp = value;

                if (hp <= 0.0f)
                {
                    bossCollider.enabled = false;
                }

                hp = Mathf.Clamp(hp, 0.0f, maxHP);
                onHealthChange?.Invoke(hp);
            }
        }
    }

    /// <summary>
    /// 보스의 MaxHP를 확인하기 위한 프로퍼티(읽기 전용)
    /// </summary>
    public float MaxHP => maxHP;

    /// <summary>
    /// 보스가 살아있는지 확인하기 위한 프로퍼티
    /// </summary>
    public bool IsAlive => hp > 0.0f;

    /// <summary>
    /// 보스의 공격력을 확인하기 위한 프로퍼티
    /// </summary>
    public float AttackPower => attackPower;

    /// <summary>
    /// 보스의 방어력을 확인하기 위한 프로퍼티
    /// </summary>
    public float DefensePower => defencePower;

    // 컴포넌트들
    PlayerStatus playerStatus;

    private void Awake()
    {
        playerStatus = GameManager.Instance.PlayerStatus;
        bossCollider = GetComponent<Collider>();
        audioManager = GameManager.Instance.AudioManager;
    }

    private void OnEnable()
    {
        hp = maxHP;
        onHealthChange?.Invoke(hp / maxHP);
    }

    /// <summary>
    /// 죽으면 실행되는 함수(사용 안함)
    /// </summary>
    public void Die()
    {
        onDie?.Invoke();
    }

    /// <summary>
    /// 보스는 넉백을 당하지 않음(빈 함수)
    /// </summary>
    public void KnockbackOnHit(float hitPower)
    {
        onKnockBack?.Invoke(hitPower);  // 오류문 제거용 코드
    }

    /// <summary>
    /// 데미지를 입는 함수
    /// </summary>
    /// <param name="damage">입는 데미지 양</param>
    /// <param name="damagePoint">입는 데미지 위치</param>
    public void TakeDamage(float damage, Vector3 damagePoint)
    {
        if (IsAlive)
        {
            float realDamage = damage - defencePower;

            if (realDamage < 0)
            {
                realDamage = 0;
            }

            HP -= realDamage;
            AudioSource.PlayClipAtPoint(audioManager[AudioCode.PlayerToEnemy], damagePoint);
            onHit?.Invoke(realDamage, damagePoint);
            playerStatus.IdentityGauge += 1;
        }
    }
}
