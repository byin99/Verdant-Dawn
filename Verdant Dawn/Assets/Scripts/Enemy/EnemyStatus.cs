using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour, IHealth, IBattle, IDamageable
{
    /// <summary>
    /// HP가 바뀌면 실행되는 델리게이트
    /// </summary>
    public event Action<float> onHealthChange;

    /// <summary>
    /// 공격을 받으면 실행되는 델리게이트
    /// </summary>
    public event Action<float, Vector3> onHit;

    /// <summary>
    /// 넉백되는 공격을 받으면 실행되는 델리게이트
    /// </summary>
    public event Action<float> onKnockBack;

    /// <summary>
    /// 죽으면 실행되는 델리게이트
    /// </summary>
    public event Action onDie;

    [Header("Enemy 능력치")]
    [SerializeField]
    /// <summary>
    /// 적의 종류
    /// </summary>
    EnemyType enemyType;

    /// <summary>
    /// 적 현재 체력
    /// </summary>
    float hp;

    [SerializeField]
    /// <summary>
    /// 적 최대 체력
    /// </summary>
    float maxHP;

    [SerializeField]
    /// <summary>
    /// 적 공격력
    /// </summary>
    float attackPower;

    [SerializeField]
    /// <summary>
    /// 적 방어력
    /// </summary>
    float defencePower;

    /// <summary>
    /// 적이 죽었을때 얻는 경험치 양
    /// </summary>
    public float expPoint;

    /// <summary>
    /// 적의 HP를 확인하고 설정하기 위한 프로퍼티
    /// </summary>
    public float HP
    {
        get => hp;
        set
        {
            if (IsAlive)    // 살아있을 때만 적용
            {
                hp = value;
                if (hp <= 0.0f) // 0 이하로 내려가면 사망처리
                {
                    Die();
                }

                hp = Mathf.Clamp(hp, 0.0f, maxHP);
                onHealthChange?.Invoke(hp / maxHP);
            }
        }
    }

    /// <summary>
    /// 적의 최대 체력을 알려주는 프로퍼티(읽기 전용)
    /// </summary>
    public float MaxHP => maxHP;

    /// <summary>
    /// 적이 살아있음을 알려주는 프로퍼티
    /// </summary>
    public bool IsAlive => hp > 0.0f;

    /// <summary>
    /// 적의 공격력을 알려주는 프로퍼티(읽기 전용)
    /// </summary>
    public float AttackPower => attackPower;

    /// <summary>
    /// 적의 방어력을 알려주는 프로퍼티(읽기 전용)
    /// </summary>
    public float DefensePower => defencePower;

    // 컴포넌트들
    PlayerStatus playerStatus;
    PlayerQuest playerQuest;

    private void Awake()
    {
        playerStatus = GameManager.Instance.PlayerStatus;
        playerQuest = GameManager.Instance.PlayerQuest;
    }

    private void OnEnable()
    {
        hp = maxHP;
        onHealthChange?.Invoke(hp / maxHP);
    }

    /// <summary>
    /// 데미지를 입는 함수
    /// </summary>
    /// <param name="damage">데미지 량</param>
    /// <param name="damagePoint">데미지 위치</param>
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
            onHit?.Invoke(realDamage, damagePoint);
            playerStatus.IdentityGauge += 1;
        }
    }

    /// <summary>
    /// 적이 죽을 때 실행되는 함수
    /// </summary>
    public void Die()
    {
        playerStatus.ExperiencePoint += expPoint;
        playerQuest.onEnemyKill?.Invoke(enemyType);
        onDie?.Invoke();
    }

    /// <summary>
    /// 데미지를 입었을 때 넉백시키는 힘의 양을 전달받는 함수
    /// </summary>
    /// <param name="hitPower">넉백시키는 힘의 양</param>
    public void KnockbackOnHit(float hitPower)
    {
        onKnockBack?.Invoke(hitPower);
    }
}
