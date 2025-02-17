using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossIdle : BossBase
{
    /// <summary>
    /// idle을 하는 시간
    /// </summary>
    float idleTime = 1.0f;

    /// <summary>
    /// 시간 누적용
    /// </summary>
    float timeElapsed = 0.0f;

    public override void Enter(BossController sender)
    {
        base.Enter(sender);

        // idle 모션 주기
        timeElapsed = 0.0f;
        animator.SetTrigger(Idle_Hash);
    }

    public override void UpdateState(BossController sender)
    {
        base.UpdateState(sender);

        timeElapsed += Time.deltaTime;

        // idle시간이 지나면 Attack 상태로 넘어가기
        if (timeElapsed > idleTime)
        {
            sender.bossStateMachine.TransitionTo(sender.chase);
        }
    }

    public override void Exit(BossController sender)
    {
        // 시간 초기화
        timeElapsed = 0.0f;
    }
}
