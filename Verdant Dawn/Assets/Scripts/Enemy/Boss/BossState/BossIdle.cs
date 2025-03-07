using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossIdle : BossBase
{
    /// <summary>
    /// idle을 하는 시간
    /// </summary>
    float idleTime = 3.0f;

    /// <summary>
    /// 시간 누적용
    /// </summary>
    float timeElapsed = 0.0f;

    /// <summary>
    /// 첫번째 소환 공격 HP 비율
    /// </summary>
    float firstEvocationHPRatio = 0.7f;

    /// <summary>
    /// 두번째 소환 공격 HP 비율
    /// </summary>
    float secondEvocationHPRatio = 0.4f;

    /// <summary>
    /// 마지막 소환 공격 HP 비율
    /// </summary>
    float lastEvocationHPRatio = 0.1f;

    /// <summary>
    /// 광폭화 HP 비율
    /// </summary>
    float berserkHPRatio = 0.3f;

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

        // idle시간이 지나면 attack 상태로 넘어가기
        if (timeElapsed > idleTime)
        {
            float bossHPRatio = bossStatus.HP / bossStatus.MaxHP;
            if (!sender.isFirstEvocation && bossHPRatio < firstEvocationHPRatio)
            {
                sender.isFirstEvocation = true;
                sender.bossStateMachine.TransitionTo(sender.firstEvocation);
            }

            else if (!sender.isSecondEvocation && bossHPRatio < secondEvocationHPRatio)
            {
                sender.isSecondEvocation = true;
                sender.bossStateMachine.TransitionTo(sender.secondEvocation);
            }

            else if (!sender.isBerserk && bossHPRatio < berserkHPRatio)
            {
                sender.isBerserk = true;
                sender.bossStateMachine.TransitionTo(sender.berserk);
            }

            else if (!sender.isLastEvocation && bossHPRatio < lastEvocationHPRatio)
            {
                sender.isLastEvocation = true;
                sender.bossStateMachine.TransitionTo(sender.lastEvocation);
            }

            else if (sender.isFirstEvocation && sender.isSecondEvocation && sender.isLastEvocation && sender.isBerserk && !bossStatus.IsAlive)
            {
                sender.bossStateMachine.TransitionTo(sender.death);
            }

            else
            {
                sender.bossStateMachine.TransitionTo(sender.chase);
            }
        }
    }

    public override void Exit(BossController sender)
    {
        // 시간 초기화
        timeElapsed = 0.0f;
    }
}
