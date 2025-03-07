using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : BossBase
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

    public override void Enter(BossController sender)
    {
        base.Enter(sender);

        sender.isAttack = true;
        sender.StartCoroutine(AttackCoroutine(sender));
    }

    public override void UpdateState(BossController sender)
    {
        base.UpdateState(sender);

        timeElapsed += Time.deltaTime;

        if (sender.isAttack && timeElapsed > attackAnimTime)
        {
            sender.isAttack = false;
            sender.bossStateMachine.TransitionTo(sender.idle);
        }
    }

    public override void Exit(BossController sender)
    {
        timeElapsed = 0.0f;
    }

    /// <summary>
    /// 랜덤으로 공격하는 함수(4개)
    /// </summary>
    void RandomAttack()
    {
        int randomNumber = Random.Range(0, 4);
        timeElapsed = 0.0f;

        switch (randomNumber)
        {
            case 0:
                animator.SetTrigger(TwoHit_Hash);
                attackAnimTime = 2.167f;
                break;

            case 1:
                animator.SetTrigger(TwoHitF_Hash);
                attackAnimTime = 2.167f;
                break;

            case 2:
                animator.SetTrigger(ThreeHit_Hash);
                attackAnimTime = 3.5f;
                break;

            case 3:
                animator.SetTrigger(ThreeHitF_Hash);
                attackAnimTime = 3.5f;
                break;
        }
    }

    /// <summary>
    /// 공격 코루틴
    /// </summary>
    /// <param name="sender">보스</param>
    IEnumerator AttackCoroutine(BossController sender)
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
