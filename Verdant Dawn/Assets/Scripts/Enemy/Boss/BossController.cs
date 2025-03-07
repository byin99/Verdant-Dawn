using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : RecycleObject
{
    /// <summary>
    /// 광폭화 됐을 때 화면 색깔
    /// </summary>
    public Color berserkColor;

    /// <summary>
    /// 광폭화 전 이동 속도
    /// </summary>
    public float normalMoveSpeed;

    /// <summary>
    /// 광폭화 이동 속도
    /// </summary>
    public float berserkMoveSpeed;

    /// <summary>
    /// 공격중 여부(true면 공격중, false면 공격중이 아님)
    /// </summary>
    [HideInInspector]
    public bool isAttack = false;

    /// <summary>
    /// 보스 광폭화 여부(true면 광폭화 중, false면 광폭화 중이 아님)
    /// </summary>
    [HideInInspector]
    public bool isBerserk = false;

    /// <summary>
    /// 첫번째 소환 공격 여부(true면 첫번째 소환 공격 완료, false면 첫번째 소환 공격 미완)
    /// </summary>
    [HideInInspector]
    public bool isFirstEvocation = false;

    /// <summary>
    /// 두번째 소환 공격 여부(true면 두번째 소환 공격 완료, false면 두번째 소환 공격 미완)
    /// </summary>
    [HideInInspector]
    public bool isSecondEvocation = false;

    /// <summary>
    /// 마지막 소환 공격 여부(true면 마지막 소환 공격 완료, false면 마지막 소환 공격 미완)
    /// </summary>
    [HideInInspector]
    public bool isLastEvocation = false;

    /// <summary>
    /// Dissolve적용하기 위한 컴포넌트
    /// </summary>
    [HideInInspector]
    public SkinnedMeshRenderer skinnedMeshRenderer;

    /// <summary>
    /// Dissolve용 Material
    /// </summary>
    public Material[] materials;

    /// <summary>
    /// StateMachine
    /// </summary>
    public StateMachine<BossController> bossStateMachine;

    // State들
    public IState<BossController> idle;
    public IState<BossController> appearance;
    public IState<BossController> chase;
    public IState<BossController> firstEvocation;
    public IState<BossController> secondEvocation;
    public IState<BossController> lastEvocation;
    public IState<BossController> attack;
    public IState<BossController> fly;
    public IState<BossController> staggered;
    public IState<BossController> berserk;
    public IState<BossController> death;

    /// <summary>
    /// 공격 트랜스폼
    /// </summary>
    [HideInInspector]
    public Transform attackTransform;

    // 컴포넌트들
    Animator animator;
    NavMeshAgent agent;

    private void Awake()
    {
        // State 만들기
        idle = new BossIdle();
        appearance = new BossAppearance();
        chase = new BossChase();
        firstEvocation = new BossFirstEvocation();
        secondEvocation = new BossSecondEvocation();
        lastEvocation = new BossLastEvocation();
        attack = new BossAttack();
        fly = new BossFly();
        staggered = new BossStaggered();
        berserk = new BossBerserk();
        death = new BossDeath();

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        attackTransform = transform.GetChild(4);

        skinnedMeshRenderer = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
    }

    protected override void OnReset()
    {
        bossStateMachine = new StateMachine<BossController>(this, appearance);
        agent.speed = normalMoveSpeed;
    }

    private void Update()
    {
        // State별로 Update함수 실행하기
        bossStateMachine.Update();
    }

    private void OnAnimatorMove()
    {
        if (isAttack)
        {
            transform.position += animator.deltaPosition;
        }
    }

    /// <summary>
    /// 죽으면 실행되는 함수
    /// </summary>
    void Die()
    {
        if (isFirstEvocation && isSecondEvocation && isLastEvocation && isBerserk)
        {
            ChangeMaterial();
            bossStateMachine.TransitionTo(death);
        }
    }

    /// <summary>
    /// Material을 바꾸는 함수
    /// </summary>
    public void ChangeMaterial()
    {
        Material[] newMaterials = skinnedMeshRenderer.materials;
        newMaterials[0] = materials[0];
        newMaterials[1] = materials[1];
        skinnedMeshRenderer.materials = newMaterials;
    }

    /// <summary>
    /// BossAttack1 소환 함수(Animation Clip용)
    /// </summary>
    void AttackEffect1()
    {
        if (isBerserk)
            Factory.Instance.GetBossAttack1_B(attackTransform.position, attackTransform.rotation.eulerAngles);
        else
            Factory.Instance.GetBossAttack1(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// BossAttack2 소환 함수(Animation Clip용)
    /// </summary>
    void AttackEffect2()
    {
        if (isBerserk)
            Factory.Instance.GetBossAttack2_B(attackTransform.position, attackTransform.rotation.eulerAngles);
        else
            Factory.Instance.GetBossAttack2(attackTransform.position, attackTransform.rotation.eulerAngles); ;
    }

    /// <summary>
    /// BossRoar 소환 함수(Animation Clip용)
    /// </summary>
    void RoarEffect()
    {
        Factory.Instance.GetBossRoar(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
