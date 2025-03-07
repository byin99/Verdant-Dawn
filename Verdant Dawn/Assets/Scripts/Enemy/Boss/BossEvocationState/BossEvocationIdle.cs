using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvocationIdle : BossEvocationBase
{
    float idleTime = 2.0f;
    float timeElapsed = 0.0f;

    public override void Enter(BossEvocationController sender)
    {
        base.Enter(sender);

        animator.SetTrigger(Idle_Hash);
        timeElapsed = 0.0f;
    }

    public override void UpdateState(BossEvocationController sender)
    {
        base.UpdateState(sender);

        timeElapsed += Time.deltaTime;

        if (timeElapsed > idleTime)
        {
            sender.bossEvocationStateMachine.TransitionTo(sender.chase);
        }
    }

    public override void Exit(BossEvocationController sender)
    {
        base.Exit(sender);
    }
}
