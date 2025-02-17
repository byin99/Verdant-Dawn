using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : RecycleObject
{
    /// <summary>
    /// 공격중인지를 기록하는 변수
    /// </summary>
    [HideInInspector]
    public bool isAttack = false;

    /// <summary>
    /// StateMachine
    /// </summary>
    public StateMachine<BossController> bossStateMachine;

    // State들
    public IState<BossController> idle;
    public IState<BossController> appearance;
    public IState<BossController> chase;
    public IState<BossController> evocation;
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

    private void Awake()
    {
        // State 만들기
        idle = new BossIdle();
        appearance = new BossAppearance();
        chase = new BossChase();
        evocation = new BossEvocation();
        attack = new BossAttack();
        fly = new BossFly();
        staggered = new BossStaggered();
        berserk = new BossBerserk();
        death = new BossDeath();

        animator = GetComponent<Animator>();

        attackTransform = transform.GetChild(4);
    }

    protected override void OnReset()
    {
        bossStateMachine = new StateMachine<BossController>(this, appearance);
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
    /// BossAttack1 소환 함수(Animation Clip용)
    /// </summary>
    void AttackEffect1()
    {
        Factory.Instance.GetBossAttack1(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// BossRoar 소환 함수(Animation Clip용)
    /// </summary>
    void RoarEffect()
    {
        Factory.Instance.GetBossRoar(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
