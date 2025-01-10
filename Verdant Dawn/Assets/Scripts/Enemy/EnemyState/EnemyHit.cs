using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : EnemyBase
{
    /// <summary>
    /// 맞고 나서 행동불능 시간
    /// </summary>
    float hittingTime = 1.5f;

    /// <summary>
    /// 맞을 때 밀려나는 시간
    /// </summary>
    float hitTime = 0.1f;

    /// <summary>
    /// 시간 중첩용 변수(Hit 상태용)
    /// </summary>
    float timeElapsed = 0.0f;

    public override void Enter(EnemyController sender)
    {
        base.Enter(sender);

        // agent를 꺼서 rigid함수 사용하기
        agent.enabled = false;
        sender.isHit = true;

        // 맞을 때 플레이어 쳐다보기
        sender.transform.LookAt(player.transform);

        animator.SetTrigger(Hit_Hash);  // Hit 애니메이션 주기
        timeElapsed = 0.0f;
    }

    public override void UpdateState(EnemyController sender)
    {
        // Hit 상태 딜레이 주기
        timeElapsed += Time.deltaTime;

        if (timeElapsed > hitTime && sender.isHit)
        {
            // 맞는 시간 줄이기
            sender.isHit = false;
            agent.enabled = true;
        }

        else if (timeElapsed > hittingTime)
        {
            playerDistance = (sender.transform.position - player.transform.position).sqrMagnitude;    // 플레이어와의 거리 계산

            if (playerDistance < attackDistance)    // 공격할 수 있는 거리가 되면
            {
                sender.enemyStateMachine.TransitionTo(sender.attack);   // Attack 상태로 전환
            }
            else
            {
                sender.enemyStateMachine.TransitionTo(sender.trace);    // Trace 상태로 전환
            }
        }
    }

    public override void Exit(EnemyController sender)
    {
    }
}
