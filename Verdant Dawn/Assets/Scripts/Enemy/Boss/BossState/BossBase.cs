using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class BossBase : IState<BossController>
{
    /// <summary>
    /// 플레이어와의 거리
    /// </summary>
    protected float playerDistance;

    /// <summary>
    /// 포효 애니메이션 시간
    /// </summary>
    protected readonly float roarAnimTime = 2.5f;

    // 컴포넌트들
    protected Rigidbody rigid;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected Player player;
    protected Material[] materials;
    protected BossStatus bossStatus;
    protected BossEvocationUI bossEvocationUI;
    protected BossHPUI bossHPUI;
    protected BossEffect bossEffect;
    protected Volume volume;
    protected AudioManager audioManager;

    // 애니메이터 해시값들
    protected readonly int Idle_Hash = Animator.StringToHash("Idle");
    protected readonly int Walk_Hash = Animator.StringToHash("Walk");
    protected readonly int Roar_Hash = Animator.StringToHash("Roar");
    protected readonly int Equip_Hash = Animator.StringToHash("Equip");
    protected readonly int Fly_Hash = Animator.StringToHash("Fly");
    protected readonly int TwoHit_Hash = Animator.StringToHash("2Hit");
    protected readonly int TwoHitF_Hash = Animator.StringToHash("2Hit_F");
    protected readonly int ThreeHit_Hash = Animator.StringToHash("3Hit");
    protected readonly int ThreeHitF_Hash = Animator.StringToHash("3Hit_F");
    protected readonly int Hit_Hash = Animator.StringToHash("Hit");
    protected readonly int Death_Hash = Animator.StringToHash("Death");

    // 쉐이터 프로퍼티용 ID들
    protected readonly int Fade_ID = Shader.PropertyToID("_Fade");

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

        if (bossStatus == null)
        {
            bossStatus = sender.GetComponent<BossStatus>();
        }

        if (bossEvocationUI == null)
        {
            bossEvocationUI = UIManager.Instance.BossEvocationUI;
        }

        if (bossHPUI == null)
        {
            bossHPUI = UIManager.Instance.BossHPUI;
        }

        if (volume == null)
        {
            volume = GameManager.Instance.Volume;
        }

        if (audioManager == null)
        {
            audioManager = GameManager.Instance.AudioManager;
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
