using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStatus : MonoBehaviour, IHealth, IMana, IBattle, IDamageable
{
    /// <summary>
    /// 플레이어 HP의 변화를 알리는 델리게이트
    /// </summary>
    public event Action<float> onHealthChange;

    /// <summary>
    /// 플레이어 MP의 변화를 알리는 델리게이트
    /// </summary>
    public event Action<float> onManaChange;

    /// <summary>
    /// 플레이어 EXP의 변화를 알리는 델리게이트
    /// </summary>
    public event Action<float> onExpChange;

    /// <summary>
    /// 플레이어가 공격을 당했을 때 실행되는 델리게이트
    /// </summary>
    public event Action<float, Vector3> onHit;

    /// <summary>
    /// 플레이어가 공격을 당했을 때 넉백되면 실행되는 델리게이트
    /// </summary>
    public event Action<float> onKnockBack;

    /// <summary>
    /// 플레이어가 레벨 업을 하면 실행되는 델리게이트
    /// </summary>
    public event Action onLevelUp;

    /// <summary>
    /// 플레이어가 죽으면 실행되는 델리게이트
    /// </summary>
    public event Action onDie;

    /// <summary>
    /// 스테이터스가 바뀌면 실행되는 델리게이트(UI용)
    /// </summary>
    public event Action onStatus;

    /// <summary>
    /// 플레이어 최대 HP
    /// </summary>
    float maxHP = 100.0f;

    /// <summary>
    /// 플레이어 최대 MP
    /// </summary>
    float maxMP = 100.0f;

    /// <summary>
    /// 플레이어 HP
    /// </summary>
    float hp = 100.0f;

    /// <summary>
    /// 플레이어 MP
    /// </summary>
    float mp = 100.0f;

    /// <summary>
    /// 플레이어 기본 공격력
    /// </summary>
    float baseAttackPower = 10;

    /// <summary>
    /// 플레이어 기본 방어력
    /// </summary>
    float baseDefensePower = 5;

    /// <summary>
    /// 플레이어 무기 공격력
    /// </summary>
    [HideInInspector]
    public float equipAttackPower;

    /// <summary>
    /// 플레이어 장비 방어력
    /// </summary>
    [HideInInspector]
    public float equipDefensePower;

    /// <summary>
    /// 플레이어 레벨
    /// </summary>
    int level = 1;

    /// <summary>
    /// 경험치
    /// </summary>
    float experiencePoint = 0.0f;

    /// <summary>
    /// 최대 경험치양
    /// </summary>
    float maxExperiencePoint = 100.0f;

    /// <summary>
    /// 맞고 있는지 기록하는 변수(true면 맞고있음, false면 안 맞고 있음)
    /// </summary>
    [HideInInspector] 
    public bool isHit;

    /// <summary>
    /// 마나가 가득 차 있는지 판단하는 변수(true면 가득 차있음, false면 가득 차있지 않음)
    /// </summary>
    bool isManaFull;

    /// <summary>
    /// 맞는 동안 시간을 재기위한 변수
    /// </summary>
    float timeElapsed = 0.0f;

    /// <summary>
    /// 맞아서 넉백되는 시간
    /// </summary>
    float hittingTime = 0.1f;

    /// <summary>
    /// 맞는 시간
    /// </summary>
    float hitTime = 1.0f;

    /// <summary>
    /// 넉백되는 힘
    /// </summary>
    float knockBackPower;

    /// <summary>
    /// 마나가 차기위해 필요한 딜레이 시간
    /// </summary>
    [SerializeField]
    float manaDelayTime;

    /// <summary>
    /// 마나를 채우기 위해 시간을 재는 변수
    /// </summary>
    float manaRechargeTimer;

    /// <summary>
    /// 맞은 부위
    /// </summary>
    Vector3 hitPoint;

    /// <summary>
    /// 플레이어의 HP를 확인하고 설정하기 위한 프로퍼티(설정은 private)
    /// </summary>
    public float HP
    {
        get => hp;
        private set
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
    /// 플레이어의 최대 HP를 확인하기 위한 프로퍼티
    /// </summary>
    public float MaxHP => maxHP;

    /// <summary>
    /// 플레이어의 MP를 확인하고 설정하기 위한 프로퍼티(설정은 private)
    /// </summary>
    public float MP
    {
        get => mp;
        private set
        {
            if (IsAlive)    // 살아있을 때만 적용
            {
                if (value > maxMP)
                {
                    isManaFull = true;
                }
                else
                {
                    isManaFull = false;
                    manaRechargeTimer = 0.0f;
                }

                mp = Mathf.Clamp(value, 0.0f, maxMP);   // 최대/최소치를 벗어날 수 없음
                onManaChange?.Invoke(mp / maxMP);
            }
        }
    }

    /// <summary>
    /// 최대 MP를 확인하기 위한 프로퍼티
    /// </summary>
    public float MaxMP => maxMP;

    /// <summary>
    /// 플레이어의 공격력을 확인하기 위한 프로퍼티
    /// </summary>
    public float AttackPower => baseAttackPower + equipAttackPower;

    /// <summary>
    /// 플레이어의 방어력을 확인하기 위한 프로퍼티
    /// </summary>
    public float DefensePower => baseDefensePower + equipDefensePower;

    /// <summary>
    /// 플레이어의 레벨을 확인하기 위한 프로퍼티
    /// </summary>
    public int Level => level;

    /// <summary>
    /// 경럼치의 변화를 확인하기 위한 프로퍼티
    /// </summary>
    public float ExperiencePoint
    {
        get => experiencePoint;
        set
        {
            // 살아있을 때만 처리
            if (IsAlive)
            {
                if (level == 50)
                {
                    experiencePoint = 0;
                    onExpChange?.Invoke(0);
                }

                else
                {
                    bool isLevelUp = false;

                    // 경험치가 다 찼다면
                    while (value > maxExperiencePoint - 0.01f)
                    {
                        value -= maxExperiencePoint;
                        isLevelUp = true;
                        LevelUp();
                    }

                    if (isLevelUp)
                    {
                        onLevelUp?.Invoke();
                        onStatus?.Invoke();
                    }

                    experiencePoint = value;
                    onExpChange?.Invoke(experiencePoint / maxExperiencePoint * 100.0f);
                }
            }
        }
    }

    // -----------------------------------------------------------------------------------------------------

    /// <summary>
    /// 아이덴 스킬을 쓰면 실행되는 델리게이트
    /// </summary>
    public event Action onIdentitySkill;

    /// <summary>
    /// 아이덴 스킬이 끝나면 실행되는 델리게이트(중간에 꺼도 실행됨)
    /// </summary>
    public event Action offIdentitySkill;

    /// <summary>
    /// 아이덴티티 게이지가 변하면 실행되는 델리게이트
    /// </summary>
    public event Action<float> onChangeIdentityGauge;

    /// <summary>
    /// 아이덴티티 게이지
    /// </summary>
    float identityGauge;

    /// <summary>
    /// 아이덴티티 줄어드는 딜레이 타임
    /// </summary>
    float identityDelayTime = 3;

    /// <summary>
    /// 아이덴티티 스킬을 사용할 수 있으면 true, 아니면 false를 기록하는 변수
    /// </summary>
    bool canIdentity;

    /// <summary>
    /// 아이덴티티 스킬을 사용중이면 true, 아니면 false
    /// </summary>
    [HideInInspector]
    public bool isIdentity;

    public float IdentityGauge
    {
        get => identityGauge;
        set
        {
            if (IsAlive && !canIdentity)
            {
                if (value > 99.99f)
                {
                    canIdentity = true;
                }

                identityGauge = Mathf.Clamp(value, 0.0f, 100);
                onChangeIdentityGauge?.Invoke(identityGauge);
            }
        }
    }

    /// <summary>
    /// 플레이어의 생존 여부를 확인하기 위한 프로퍼티
    /// </summary>
    public bool IsAlive => hp > 0.0f;

    // 컴포넌트들
    Animator animator;
    Rigidbody rigid;
    NavMeshAgent agent;
    Player player;

    // 애니메이터용 해시값
    readonly int Hit_Hash = Animator.StringToHash("Hit");
    readonly int Die_Hash = Animator.StringToHash("Die");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        level = 1;
        HP = maxHP;
        MP = maxMP;
        ExperiencePoint = 0.0f;
        IdentityGauge = 0.0f;
    }

    private void FixedUpdate()
    {
        if (isHit && IsAlive)
        {
            timeElapsed += Time.fixedDeltaTime;

            // 넉백 효과 주기
            rigid.AddForce(-transform.forward * knockBackPower, ForceMode.Impulse);

            if (timeElapsed > hittingTime)
            {
                rigid.isKinematic = true;
            }

            if (timeElapsed > hitTime)
            {
                agent.enabled = true;
                agent.ResetPath();
                isHit = false;
            }
        }

        // 마나를 시간당 채우기
        if (!isManaFull && manaRechargeTimer < manaDelayTime)
        {
            manaRechargeTimer += Time.fixedDeltaTime;
            if (manaRechargeTimer > manaDelayTime)
            {
                MP += maxMP * 0.2f;
                manaRechargeTimer = 0.0f;
            }
        }
    }

    /// <summary>
    /// 클래스가 바뀔 때마다 능력치를 바꿔주는 함수
    /// </summary>
    /// <param name="currentClass"></param>
    public void ChangeClassStatus(CharacterClass currentClass)
    {
        switch (currentClass)
        {
            case CharacterClass.Fighter:
                equipAttackPower = 10.0f;
                equipDefensePower = 7.0f;
                break;

            case CharacterClass.Berserker:
                equipAttackPower = 10.0f;
                equipDefensePower = 8.0f;
                break;

            case CharacterClass.Hunter:
                equipAttackPower = 15.0f;
                equipDefensePower = 5.0f;
                break;

            case CharacterClass.Magician:
                equipAttackPower = 15.0f;
                equipDefensePower = 5.0f;
                break;

            case CharacterClass.Assassin:
                equipAttackPower = 8.0f;
                equipDefensePower = 4.0f;
                break;
        }
        onStatus?.Invoke();
    }

    /// <summary>
    /// 플레이어가 죽으면 실행되는 함수
    /// </summary>
    public void Die()
    {
        animator.SetTrigger(Die_Hash);
        onDie?.Invoke();
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
            animator.SetTrigger(Hit_Hash);
            float realDamage = damage - DefensePower;

            if (realDamage < 0)
            {
                realDamage = 0;
            }

            HP -= realDamage;
            hitPoint = damagePoint;
            hitPoint.y = 0;
            onHit?.Invoke(realDamage, damagePoint);
        }
    }

    /// <summary>
    /// 데미지를 입었을 때 넉백시키는 힘의 양을 전달받는 함수
    /// </summary>
    /// <param name="hitPower">넉백시키는 힘의 양</param>
    public void KnockbackOnHit(float hitPower)
    {
        if (player.CanKnockBack)
        {
            isHit = true;
            timeElapsed = 0.0f;

            transform.LookAt(hitPoint);
            agent.enabled = false;
            rigid.isKinematic = false;

            knockBackPower = hitPower;
            onKnockBack?.Invoke(hitPower);
        }
    }

    /// <summary>
    /// 아이덴티티 스킬을 사용하면 실행되는 함수
    /// </summary>
    public void IdentitySkill()
    {
        if (canIdentity)
        {
            onIdentitySkill?.Invoke();
            StartCoroutine(StartIdentitySkill());
        }
        else if (isIdentity)
        {
            StopAllCoroutines();
            OffIDentitySkill();
            isIdentity = false;
            offIdentitySkill?.Invoke();
        }
        
    }

    /// <summary>
    /// 레벨업을 하는 함수
    /// </summary>
    void LevelUp()
    {
        level++;
        baseAttackPower *= 1.2f;
        baseDefensePower *= 1.2f;
        maxHP *= 1.2f;
        HP = maxHP;
        MP = maxMP;
        maxExperiencePoint *= 1.2f;
    }

    IEnumerator StartIdentitySkill()
    {
        canIdentity = false;
        isIdentity = true;
        OnIdentitySkill();

        while (IdentityGauge > 0.1f)
        {
            float timeElapsed = 0.0f;
            IdentityGauge -= 10.0f;
            while (timeElapsed < identityDelayTime)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

        OffIDentitySkill();
        offIdentitySkill?.Invoke();
        isIdentity = false;
    }

    /// <summary>
    /// MP를 바꾸는 함수
    /// </summary>
    /// <param name="change">바뀐 MP<</param>
    public void ManaChange(float change)
    {
        MP = change;
    }

    /// <summary>
    /// 아이덴티티 스킬을 사용하면 능력치가 오르게 하는 함수
    /// </summary>
    void OnIdentitySkill()
    {
        baseAttackPower *= 2.0f;
        baseDefensePower *= 2.0f;
    }

    /// <summary>
    /// 아이덴티티 스킬이 끝나면 능력치를 돌아오게 하는 함수
    /// </summary>
    void OffIDentitySkill()
    {
        baseAttackPower *= 0.5f;
        baseDefensePower *= 0.5f;
    }
}
