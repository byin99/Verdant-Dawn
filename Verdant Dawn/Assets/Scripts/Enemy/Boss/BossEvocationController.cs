using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BossEvocationController : RecycleObject
{
    /// <summary>
    /// 공격중인지를 기록하는 변수
    /// </summary>
    [HideInInspector]
    public bool isAttack;

    /// <summary>
    /// Dissolve용 Material
    /// </summary>
    [HideInInspector]
    public Material material;

    /// <summary>
    /// StateMachine
    /// </summary>
    public StateMachine<BossEvocationController> bossEvocationStateMachine;

    // State들
    public IState<BossEvocationController> idle;
    public IState<BossEvocationController> chase;
    public IState<BossEvocationController> attack;
    public IState<BossEvocationController> death;

    /// <summary>
    /// 공격 트랜스폼
    /// </summary>
    [HideInInspector]
    public Transform attackTransform;

    // 컴포넌트들
    EnemyStatus enemyStatus;
    Animator animator;
    NavMeshAgent agent;
    Collider evocationCollider;

    // 쉐이터 프로퍼티용 ID들 
    readonly int Fade_ID = Shader.PropertyToID("_Fade");

    private void Awake()
    {
        // State 만들기
        idle = new BossEvocationIdle();
        chase = new BossEvocationChase();
        attack = new BossEvocationAttack();
        death = new BossEvocationDeath();

        animator = GetComponent<Animator>();

        attackTransform = transform.GetChild(2);

        // Dissolve용 Material 찾기
        SkinnedMeshRenderer skinnedMeshRenderer;
        skinnedMeshRenderer = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        material = skinnedMeshRenderer.material;

        enemyStatus = GetComponent<EnemyStatus>();
        enemyStatus.onDie += Die;

        agent = GetComponent<NavMeshAgent>();

        evocationCollider = GetComponent<Collider>();
    }

    protected override void OnReset()
    {
        // StateMachine 만들기
        bossEvocationStateMachine = new StateMachine<BossEvocationController>(this, idle);
        agent.enabled = true;
        evocationCollider.enabled = true;

        material.SetFloat(Fade_ID, 1);
    }

    private void Update()
    {
        // State별로 Update함수 실행하기
        bossEvocationStateMachine.Update();
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
        bossEvocationStateMachine.TransitionTo(death);
        evocationCollider.enabled = false;
    }

    /// <summary>
    /// BossEvocationAttack1 소환 함수(Animation Clip용)
    /// </summary>
    void Attack1()
    {
        Factory.Instance.GetBossEvocationAttack1(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// BossEvocationAttack1 소환 함수(Animation Clip용)
    /// </summary>
    void Attack2()
    {
        Factory.Instance.GetBossEvocationAttack2(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
