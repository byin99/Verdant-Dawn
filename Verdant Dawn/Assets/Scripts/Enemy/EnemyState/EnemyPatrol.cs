using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyBase
{
    /// <summary>
    /// 순찰 이동 속도
    /// </summary>
    private float walkSpeed = 2.0f;

    public override void Enter(EnemyController sender)
    {
        base.Enter(sender);

        animator.SetBool(Move_Hash, true);      // 순찰 애니에이션 적용

        agent.speed = walkSpeed;                // 순찰 속도 적용
        agent.isStopped = false;                // agent를 다시 사용
        sender.target = RandomPatrol(sender);   // 순찰 지점 재설정
        agent.SetDestination(sender.target);    // 순찰 시작
    }

    public override void UpdateState(EnemyController sender)
    {
        base.UpdateState(sender);

        // 순찰 지점에 도착했다면
        if (agent.remainingDistance < 0.2f)
        {
            sender.enemyStateMachine.TransitionTo(sender.idle); // Idle 상태로 전환
        }
    }

    public override void Exit(EnemyController sender)
    {
        animator.SetBool(Move_Hash, false); // 순찰 애니메이션 끄기
        agent.isStopped = true;             // agent 중지
    }

    /// <summary>
    /// 순찰 지역은 임의로 설정하는 함수
    /// </summary>
    /// <param name="sender">오브젝트</param>
    /// <returns>랜덤으로 설정한 순찰 지역</returns>
    public Vector3 RandomPatrol(EnemyController sender)
    {
        // 랜덤으로 숫자 2개 저장하기
        float p1 = Random.Range(-10.0f, 10.0f); 
        float p2 = Random.Range(-10.0f, 10.0f);

        // 랜덤으로 만든 순찰 지역 저장하기
        Vector3 randomPosition = new Vector3(sender.transform.position.x + p1, sender.transform.position.y, sender.transform.position.z + p2);

        // 순찰 지점이 너무 가까우면 순찰 지역 재설정
        if ((randomPosition-sender.transform.position).sqrMagnitude < 1.0f)
        {
            randomPosition = RandomPatrol(sender);
        }

        return randomPosition;
    }
}
