using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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
    /// Dissolve용 Material
    /// </summary>
    [HideInInspector]
    public Material[] materials;

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
    /// 원래 들어있는 Pool의 Transform
    /// </summary>
    Transform pool;

    /// <summary>
    /// 맞았을 때 넉백되는 양
    /// </summary>
    float knockBackPower;

    // 컴포넌트들
    Rigidbody rigid;
    Player player;
    EnemyStatus status;
    NavMeshAgent agent;
    Collider enemyCollider;
    protected AudioManager audioManager;

    // 쉐이터 프로퍼티용 ID들
    readonly int Fade_ID = Shader.PropertyToID("_Fade");

    protected virtual void Awake()
    {
        // State 만들기
        idle = new EnemyIdle();
        patrol = new EnemyPatrol();
        trace = new EnemyTrace();
        attack = new EnemyAttack();
        comeBack = new EnemyComeBack();
        hit = new EnemyHit();
        die = new EnemyDeath();

        // 컴포넌트들 찾기
        rigid = GetComponent<Rigidbody>();
        player = GameManager.Instance.Player;

        status = GetComponent<EnemyStatus>();
        agent = GetComponent<NavMeshAgent>();
        enemyCollider = GetComponent<Collider>();
        audioManager = GameManager.Instance.AudioManager;

        pool = transform.parent;
    }

    protected override void OnReset()
    {
        target = transform.position;

        // StateMachine 만들기
        enemyStateMachine = new StateMachine<EnemyController>(this, idle);
        status.HP = status.MaxHP;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat(Fade_ID, 1);
        }

        agent.enabled = true;
        enemyCollider.enabled = true;
        status.onKnockBack += Hit;
        status.onDie += Die;
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
        DropItems();
        enemyCollider.enabled = false;
        enemyStateMachine.TransitionTo(die);
    }

    /// <summary>
    /// Pool을 부모로 다시 돌리는 함수
    /// </summary>
    public void ReturnToPool()
    {
        transform.SetParent(pool);          // 부모를 풀로 재설정
    }

    // 아이템 드랍용--------------------------------------------------------------------------------------------------------------------------------------------
    [Serializable]
    public struct ItemDropInfo
    {
        public ItemCode code;       // 어떤 종류의 아이템인지
        [Range(0f, 1f)]
        public float dropRatio;     // 드랍될 확률은 얼마인지
        public uint dropCount;      // 최대 몇개를 드랍할 것인지
    }

    /// <summary>
    /// 아이템 드랍 정보창
    /// </summary>
    public ItemDropInfo[] dropItems;

    /// <summary>
    /// 아이템을 드랍하는 함수
    /// </summary>
    public void DropItems()
    {
        foreach (var item in dropItems)
        {
            if (item.dropRatio > Random.value)
            {
                uint count = (uint)Random.Range(0, (int)item.dropCount) + 1;
                Factory.Instance.MakeItems(item.code, count, transform.position, true);
            }
        }
    }
}
