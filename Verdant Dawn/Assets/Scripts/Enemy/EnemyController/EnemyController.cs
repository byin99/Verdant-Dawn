using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : RecycleObject
{
    /// <summary>
    /// 순찰 목표 지점
    /// </summary>
    [HideInInspector] 
    public Vector3 target;

    /// <summary>
    /// Dissolve를 위한 skinnedMeshRenderer들
    /// </summary>
    [HideInInspector]
    public SkinnedMeshRenderer[] skinnedMeshRenderers;

    /// <summary>
    /// StateMachine
    /// </summary>
    public StateMachine<EnemyController> enemyStateMachine;

    // State들
    public IState<EnemyController> idle;
    public IState<EnemyController> patrol;
    public IState<EnemyController> trace;
    public IState<EnemyController> attack;
    public IState<EnemyController> comeBack;
    public IState<EnemyController> die;

    protected virtual void Awake()
    {
        // State 만들기
        idle = new EnemyIdle();
        patrol = new EnemyPatrol();
        trace = new EnemyTrace();
        attack = new EnemyAttack();
        comeBack = new EnemyComeBack();
        die = new EnemyDie();

        // StateMachine 만들기
        enemyStateMachine = new StateMachine<EnemyController>(this, idle);
    }

    private void Update()
    {
        // State별로 Update함수 실행하기
        enemyStateMachine.Update();
    }
}
