using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEvocationBase : IState<BossEvocationController>
{
    /// <summary>
    /// 플레이어와의 거리
    /// </summary>
    protected float playerDistance;

    // 컴포넌트들
    protected Animator animator;
    protected NavMeshAgent agent;
    protected Rigidbody rigid;
    protected Player player;

    // 애니메이터 해시값들
    protected int Idle_Hash = Animator.StringToHash("Idle");
    protected int Run_Hash = Animator.StringToHash("Run");
    protected int Attack1_Hash = Animator.StringToHash("Attack1");
    protected int Attack2_Hash = Animator.StringToHash("Attack2");
    protected int Hit_Hash = Animator.StringToHash("Hit");
    protected int Death_Hash = Animator.StringToHash("Death");

    // 쉐이터 프로퍼티용 ID들
    protected readonly int Fade_ID = Shader.PropertyToID("_Fade");

    public virtual void Enter(BossEvocationController sender)
    {
        if (animator == null)
        {
            animator = sender.GetComponent<Animator>();
        }

        if (agent == null)
        {
            agent = sender.GetComponent<NavMeshAgent>();
        }

        if (rigid == null)
        {
            rigid = sender.GetComponent<Rigidbody>();
        }
        
        if (player == null)
        {
            player = GameManager.Instance.Player;
        }
    }

    public virtual void UpdateState(BossEvocationController sender)
    {
        playerDistance = (sender.transform.position - player.transform.position).sqrMagnitude;
    }

    public virtual void Exit(BossEvocationController sender)
    {
    }
}
