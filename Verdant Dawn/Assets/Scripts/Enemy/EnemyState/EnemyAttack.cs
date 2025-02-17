using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyBase
{
    /// <summary>
    /// 공격 쿨타임
    /// </summary>
    private float attackCoolTime = 3.0f;

    /// <summary>
    /// 공격 쿨타임을 재기 위한 변수
    /// </summary>
    private float timeElasped = 0.0f;

    /// <summary>
    /// 회전 정도(10이면 0.1초 정도)
    /// </summary>
    private float turnSmooth = 10.0f;

    /// <summary>
    /// 플레이어를 목표로 하는 회전
    /// </summary>
    private Quaternion targetRotation;

    public override void Enter(EnemyController sender)
    {
        base.Enter(sender);

        // 플레이어 공격 코루틴 실행
        sender.StartCoroutine(LookAtPlayerAndAttack(sender));
    }

    public override void UpdateState(EnemyController sender)
    {
        float playerDistance = (sender.transform.position - player.transform.position).sqrMagnitude;

        // 공격 쿨타임이 끝났다면
        if (timeElasped > attackCoolTime)
        {
            if (playerDistance < attackDistance)    // 공격 거리가 된다면
            {
                sender.StartCoroutine(LookAtPlayerAndAttack(sender));   // 다시 공격
            }
            else                                    // 공격 거리보다 멀다면
            {
                sender.enemyStateMachine.TransitionTo(sender.trace);    // Trace 상태로 전환
            }
            
        }
        timeElasped += Time.deltaTime;  // 쿨타임 재기
    }

    public override void Exit(EnemyController sender)
    {
        timeElasped = 0.0f; // 공격 쿨타임 시간 초기화
    }

    IEnumerator LookAtPlayerAndAttack(EnemyController sender)
    {
        timeElasped = 0.0f;     // 공격 쿨타임 변수 초기화
        Vector3 direction = (player.transform.position - sender.transform.position).normalized;     // player가 enemy에서 어디에있는지 방향계산하기
        targetRotation = Quaternion.LookRotation(direction);                                        // 목표로하는 회전 설정
        while ((direction - sender.transform.forward).sqrMagnitude > 0.01f)                         // 플레이어를 쳐다볼때까지
        {
            sender.transform.rotation = Quaternion.Slerp(sender.transform.rotation, targetRotation, Time.deltaTime * turnSmooth);   // 돌기
            yield return null;
        }
        animator.SetTrigger(Attack_Hash);   // 공격하기
    }
}
