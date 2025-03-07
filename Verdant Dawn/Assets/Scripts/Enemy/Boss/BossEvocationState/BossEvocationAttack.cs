using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvocationAttack : BossEvocationBase
{
    /// <summary>
    /// 공격 애니메이션 시간
    /// </summary>
    float attackAnimTime = 3.5f;

    /// <summary>
    /// 공격 시간 누적용 변수
    /// </summary>
    float timeElapsed = 0.0f;

    /// <summary>
    /// 회전 정도(10이면 0.1초 정도)
    /// </summary>
    float turnSmooth = 10.0f;

    /// <summary>
    /// 플레이어를 목표로 하는 회전
    /// </summary>
    Quaternion targetRotation;

    public override void Enter(BossEvocationController sender)
    {
        base.Enter(sender);

        sender.isAttack = true;
        sender.StartCoroutine(AttackCoroutine(sender));
        timeElapsed = 0.0f;
    }

    public override void UpdateState(BossEvocationController sender)
    {
        base.UpdateState(sender);

        timeElapsed += Time.deltaTime;

        if (sender.isAttack && timeElapsed > attackAnimTime)
        {
            sender.isAttack = false;
            sender.bossEvocationStateMachine.TransitionTo(sender.idle);
        }
    }

    public override void Exit(BossEvocationController sender)
    {
        base.Exit(sender);

        timeElapsed = 0.0f;
    }

    /// <summary>
    /// 랜덤 공격하는 함수
    /// </summary>
    void RandomAttack()
    {
        int random = Random.Range(0, 2);

        switch (random)
        {
            case 0:
                animator.SetTrigger(Attack1_Hash);
                attackAnimTime = 1.333f;
                break;
            case 1:
                animator.SetTrigger(Attack2_Hash);
                attackAnimTime = 1.667f;
                break;
        }
    }

    /// <summary>
    /// 공격 코루틴
    /// </summary>
    /// <param name="sender">보스</param>
    IEnumerator AttackCoroutine(BossEvocationController sender)
    {
        Vector3 direction = (player.transform.position - sender.transform.position).normalized;     // player가 enemy에서 어디에있는지 방향계산하기
        targetRotation = Quaternion.LookRotation(direction);                                        // 목표로하는 회전 설정
        while ((direction - sender.transform.forward).sqrMagnitude > 0.01f)                         // 플레이어를 쳐다볼때까지
        {
            sender.transform.rotation = Quaternion.Slerp(sender.transform.rotation, targetRotation, Time.deltaTime * turnSmooth);   // 돌기
            yield return null;
        }

        RandomAttack();
    }
}
