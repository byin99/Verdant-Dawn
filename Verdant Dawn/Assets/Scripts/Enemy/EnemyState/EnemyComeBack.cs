using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComeBack : EnemyBase
{
    /// <summary>
    /// 순찰 복귀 속도
    /// </summary>
    private float returnSpeed = 20.0f;
    public override void Enter(EnemyController sender)
    {
        base.Enter(sender);

        agent.speed = returnSpeed;              // 순찰 복귀 속도로 바꾸기
        animator.SetBool(Chase_Hash, true);     // 달리기 애니메이션 주기
        agent.isStopped = false;                // agent 사용하기 
        agent.SetDestination(sender.target);    // 복귀 시작
    }

    public override void UpdateState(EnemyController sender)
    {
        // 복귀에 완료했다면
        if (agent.remainingDistance < 0.2f)
        {
            sender.enemyStateMachine.TransitionTo(sender.idle); // Idle 상태로 전환
        }
    }

    public override void Exit(EnemyController sender)
    {
        animator.SetBool(Chase_Hash, false);    // 달리기 애니메이션 끄기
        agent.isStopped = true;                 // agent 사용 중지
    }
}
