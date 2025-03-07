using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvocationChase : BossEvocationBase
{
    /// <summary>
    /// 공격 가능한 거리
    /// </summary>
    float attackDistance = 100.0f;

    public override void Enter(BossEvocationController sender)
    {
        base.Enter(sender);

        animator.SetTrigger(Run_Hash);
    }

    public override void UpdateState(BossEvocationController sender)
    {
        base.UpdateState(sender);

        agent.SetDestination(player.transform.position);

        // 공격 가능한 거리가 되면 공격 상태로 전이
        if (playerDistance < attackDistance)
        {
            sender.bossEvocationStateMachine.TransitionTo(sender.attack);
        }
    }

    public override void Exit(BossEvocationController sender)
    {
        agent.ResetPath();
    }
}
