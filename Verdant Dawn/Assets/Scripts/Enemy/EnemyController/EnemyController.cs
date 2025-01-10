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
    /// 맞고 있는지 알려주는 변수(true면 맞고 있음, false면 안맞고 있음)
    /// </summary>
    [HideInInspector]
    public bool isHit;

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
    public IState<EnemyController> hit;
    public IState<EnemyController> die;

    /// <summary>
    /// 맞았을 때 넉백되는 양
    /// </summary>
    float knockBackPower;

    // 컴포넌트들
    Rigidbody rigid;
    Player player;
    PlayerStatus playerStatus;
    EnemyStatus status;

    protected virtual void Awake()
    {
        // State 만들기
        idle = new EnemyIdle();
        patrol = new EnemyPatrol();
        trace = new EnemyTrace();
        attack = new EnemyAttack();
        comeBack = new EnemyComeBack();
        hit = new EnemyHit();
        die = new EnemyDie();

        // StateMachine 만들기
        enemyStateMachine = new StateMachine<EnemyController>(this, idle);

        // 컴포넌트들 찾기
        rigid = GetComponent<Rigidbody>();
        player = GameManager.Instance.Player;
        playerStatus = GameManager.Instance.PlayerStatus;
        playerStatus.onDie += PlayerDie;

        // EnemyStatus에서 델리게이트 연결하기
        status = GetComponent<EnemyStatus>();
        status.onKnockBack += Hit;
        status.onDie += Die;
    }

    protected override void OnReset()
    {
        target = transform.position;
        status.HP = status.maxHP;
    }

    private void Update()
    {
        // State별로 Update함수 실행하기
        enemyStateMachine.Update();
    }

    private void FixedUpdate()
    {
        // 넉백 효과 주기
        if (isHit && status.IsAlive)
        {
            rigid.AddForce(player.transform.forward * knockBackPower, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Enemy 공격 이펙트1 소환 함수(Animation Clip용, 빈함수)
    /// </summary>
    protected virtual void AttackEffect1()
    {
    }

    /// <summary>
    /// Enemy 공격 이펙트2 소환 함수(Animation Clip용, 빈함수)
    /// </summary>
    protected virtual void AttackEffect2()
    {
    }

    /// <summary>
    /// 데미지가 들어왔을때 실행되는 함수
    /// </summary>
    void Hit(float hitPower)
    {
        if (status.IsAlive)
        {
            enemyStateMachine.TransitionTo(hit);
            knockBackPower = hitPower;
        }
    }

    /// <summary>
    /// 죽으면 실행되는 함수
    /// </summary>
    void Die()
    {
        enemyStateMachine.TransitionTo(die);
        playerStatus.ExperiencePoint += status.expPoint;
    }

    /// <summary>
    /// 플레이어가 죽으면 실행되는 함수
    /// </summary>
    void PlayerDie()
    {
        player = null;
    }
}
