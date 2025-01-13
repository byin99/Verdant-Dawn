using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdle : EnemyBase
{
    /// <summary>
    /// 순찰 쉬는 시간
    /// </summary>
    private float patrolBreakTime = 5.0f;

    /// <summary>
    /// 순찰 쉬는 시간을 재기 위한 변수
    /// </summary>
    private float timeElapsed = 0.0f;

    public override void Enter(EnemyController sender)
    {
        base.Enter(sender);

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat(Fade_ID, 1);
        }

        animator.SetTrigger(Idle_Hash); // Idle 애니메이션 주기
        timeElapsed = 0.0f;             // 쉬는 시간 초기화
    }

    public override void UpdateState(EnemyController sender)
    {
        base.UpdateState(sender);
        timeElapsed += Time.deltaTime;      // 쉬는 시간 재기
        if (timeElapsed > patrolBreakTime)  // 쉬는 시간이 넘었다면
        {
            sender.enemyStateMachine.TransitionTo(sender.patrol);   // Patrol 상태로 전환
        }
    }

    public override void Exit(EnemyController sender)
    {
    }
}
