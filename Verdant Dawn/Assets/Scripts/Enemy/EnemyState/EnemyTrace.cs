using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrace : EnemyBase
{
    /// <summary>
    /// 추적 상태 속도
    /// </summary>
    private float runSpeed = 5.0f;

    /// <summary>
    /// 순찰지역으로 다시 돌아가는 거리(제곱)
    /// </summary>
    private float returnDistance = 400.0f;

    public override void Enter(EnemyController sender)
    {
        base.Enter(sender);

        animator.SetBool(Chase_Hash, true); // 추적 애니메이션 적용
        agent.speed = runSpeed;             // 추적 속도 적용
        agent.isStopped = false;            // agent 다시 사용하기
    }

    public override void UpdateState(EnemyController sender)
    {
        // 추적 시작
        agent.SetDestination(player.transform.position);

        // 공격 거리 되면
        float playerDistance = (sender.transform.position - player.transform.position).sqrMagnitude;
        if (playerDistance < attackDistance)
        {
            sender.enemyStateMachine.TransitionTo(sender.attack);   // Attack 상태로 전환
        }

        // 순찰 지역과 너무 멀어지면
        float targetDistance = (sender.transform.position - sender.target).sqrMagnitude;
        if (targetDistance > returnDistance)
        {
            sender.enemyStateMachine.TransitionTo(sender.comeBack); // ComeBack 상태로 전환
        }
        
    }

    public override void Exit(EnemyController sender)
    {
        animator.SetBool(Chase_Hash, false);    // 추적 애니메이션 끄기
        agent.isStopped = true;                 // agent 중지
    }
}
