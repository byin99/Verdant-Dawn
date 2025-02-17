using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBase : IState<BossController>
{
    /// <summary>
    /// 플레이어와의 거리
    /// </summary>
    protected float playerDistance;

    // 컴포넌트들
    protected Rigidbody rigid;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected Player player;

    // 애니메이터 해시값들
    protected int Idle_Hash = Animator.StringToHash("Idle");
    protected int Walk_Hash = Animator.StringToHash("Walk");
    protected int Roar_Hash = Animator.StringToHash("Roar");
    protected int Equip_Hash = Animator.StringToHash("Equip");
    protected int Fly_Hash = Animator.StringToHash("Fly");
    protected int TwoHit_Hash = Animator.StringToHash("2Hit");
    protected int TwoHitF_Hash = Animator.StringToHash("2Hit_F");
    protected int ThreeHit_Hash = Animator.StringToHash("3Hit");
    protected int ThreeHitF_Hash = Animator.StringToHash("3Hit_F");
    protected int Death_Hash = Animator.StringToHash("Death");

    public virtual void Enter(BossController sender)
    {
        if (rigid == null)
        {
            rigid = sender.GetComponent<Rigidbody>();
        }

        if (animator == null)
        {
            animator = sender.GetComponent<Animator>();
        }

        if (agent == null)
        {
            agent = sender.GetComponent<NavMeshAgent>();
        }

        if (player == null)
        {
            player = GameManager.Instance.Player;
        }
    }

    public virtual void UpdateState(BossController sender)
    {
        playerDistance = (sender.transform.position - player.transform.position).sqrMagnitude;
    }

    public virtual void Exit(BossController sender)
    {
    }

}
