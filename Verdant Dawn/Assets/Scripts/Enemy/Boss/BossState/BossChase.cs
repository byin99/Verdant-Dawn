using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChase : BossBase
{
    /// <summary>
    /// 플레이어를 공격하는 거리(제곱)
    /// </summary>
    float attackDistance = 49.0f;

    /// <summary>
    /// 플레이어에게 날아가는 거리(제곱)
    /// </summary>
    float flyDistance = 400.0f;

    public override void Enter(BossController sender)
    {
        base.Enter(sender);

        animator.SetTrigger(Walk_Hash);
    }

    public override void UpdateState(BossController sender)
    {
        base.UpdateState(sender);

        // 추적 시작
        agent.SetDestination(player.transform.position);

        if (playerDistance < attackDistance)
        {
            sender.bossStateMachine.TransitionTo(sender.attack);
        }

        else if (playerDistance > flyDistance)
        {
            sender.bossStateMachine.TransitionTo(sender.fly);
        }
    }

    public override void Exit(BossController sender)
    {
        agent.ResetPath();
    }
}
