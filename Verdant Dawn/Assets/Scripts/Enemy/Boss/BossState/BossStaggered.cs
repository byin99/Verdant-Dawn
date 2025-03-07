using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStaggered : BossBase
{
    /// <summary>
    /// 무력화 시간
    /// </summary>
    float staggeredTime = 10.0f;

    public override void Enter(BossController sender)
    {
        base.Enter(sender);

        sender.StartCoroutine(StaggeredCoroutine(sender));
    }

    public override void UpdateState(BossController sender)
    { 
    }

    public override void Exit(BossController sender)
    {
    }

    /// <summary>
    /// 무력화 코루틴
    /// </summary>
    /// <param name="sender">Boss Controller</param>
    IEnumerator StaggeredCoroutine(BossController sender)
    {
        animator.SetTrigger(Hit_Hash);
        if (sender.isBerserk)
        {
            bossEffect = Factory.Instance.GetBossStaggeredEffect_B(sender.transform.position);
        }
        else
        {
            bossEffect = Factory.Instance.GetBossStaggeredEffect(sender.transform.position);
        }

        yield return new WaitForSeconds(staggeredTime);

        animator.SetTrigger(Roar_Hash);
        bossEffect.gameObject.SetActive(false);

        yield return new WaitForSeconds(roarAnimTime);

        sender.bossStateMachine.TransitionTo(sender.idle);
    }
}
