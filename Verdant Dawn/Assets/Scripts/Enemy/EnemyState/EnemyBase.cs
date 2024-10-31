using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 컴포넌트를 모아놓기 위한 상태들의 부모 클래스
public class EnemyBase : IState<EnemyController>
{
    // 컴포넌트들
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Player player;
    protected Material[] materials;

    /// <summary>
    /// 플레이어를 감지하는 거리(제곱)
    /// </summary>
    protected float detectDistance = 25.0f;

    /// <summary>
    /// 현재상태를 공격상태로 변경하는 거리
    /// </summary>
    protected float attackDistance = 4.0f;

    // 해쉬 값들
    protected readonly int Move_Hash = Animator.StringToHash("Move");
    protected readonly int Chase_Hash = Animator.StringToHash("Chase");
    protected readonly int Attack_Hash = Animator.StringToHash("Attack");
    protected readonly int Death_Hash = Animator.StringToHash("Death");

    // 쉐이터 프로퍼티용 ID들
    protected readonly int Fade_ID = Shader.PropertyToID("_Fade");

    public virtual void Enter(EnemyController sender)
    {
        if (agent == null)
        {
            agent = sender.GetComponent<NavMeshAgent>();    // NavMeshAgent 찾기
        }

        if (animator == null)
        {
            animator = sender.GetComponent<Animator>();     // Animator 찾기
        }

        if (player == null)
        {
            player = GameManager.Instance.Player;           // Player 찾기
        }

        if (materials == null)
        {
            // material 찾기
            materials = new Material[sender.skinnedMeshRenderers.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = sender.skinnedMeshRenderers[i].material;
            }
        }
    }

    public virtual void UpdateState(EnemyController sender)
    {
        float playerDistance = (sender.transform.position - player.transform.position).sqrMagnitude;    // 플레이어와의 거리 계산

        if (playerDistance < attackDistance)    // 공격할 수 있는 거리가 되면
        {
            sender.enemyStateMachine.TransitionTo(sender.attack);   // Attack 상태로 전환
        }

        else if (playerDistance < detectDistance)   // 플레이어를 감지했으면
        {
            sender.enemyStateMachine.TransitionTo(sender.trace);    // Trace 상태로 전환
        }

    }

    public virtual void Exit(EnemyController sender)
    {
    }
}
