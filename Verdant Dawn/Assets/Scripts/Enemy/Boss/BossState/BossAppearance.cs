using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearance : BossBase
{
    /// <summary>
    /// 무기 장착 애니메이션 시간
    /// </summary>
    float equipAnimTime = 2.0f;

    /// <summary>
    /// 무기 장착하고 걷는 시간
    /// </summary>
    float walkTime = 5.0f;

    /// <summary>
    /// 행동 사이사이의 쉬는 시간
    /// </summary>
    float idleTime = 0.5f;

    public override void Enter(BossController sender)
    {
        base.Enter(sender);

        sender.StartCoroutine(BossAppearanceCoroutine(sender));
    }

    public override void UpdateState(BossController sender)
    {
        base.UpdateState(sender);
    }

    public override void Exit(BossController sender)
    {
        sender.virtualCamera.Priority = 0;
    }

    /// <summary>
    /// 보스 입장씬 코루틴
    /// </summary>
    /// <param name="sender">입장하는 보스</param>
    IEnumerator BossAppearanceCoroutine(BossController sender)
    {
        // 장비하기
        animator.SetTrigger(Equip_Hash);

        yield return new WaitForSeconds(equipAnimTime);

        // 잠시 Idle 상태
        animator.SetTrigger(Idle_Hash);

        yield return new WaitForSeconds(idleTime);

        // 앞으로 걷기
        animator.SetTrigger(Walk_Hash);
        agent.SetDestination(sender.transform.position + sender.transform.forward * 20);

        yield return new WaitForSeconds(walkTime);

        // 포효하기
        agent.ResetPath();
        animator.SetTrigger(Roar_Hash);

        yield return new WaitForSeconds(roarAnimTime);

        // Boss입장 연출 끝
        sender.bossStateMachine.TransitionTo(sender.idle);
    }
}
