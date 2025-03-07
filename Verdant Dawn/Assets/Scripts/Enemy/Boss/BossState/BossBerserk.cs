using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class BossBerserk : BossBase
{
    /// <summary>
    /// 포효 시간을 재기위한 변수
    /// </summary>
    float timeElapsed = 0.0f;

    /// <summary>
    /// 화면을 빨갛게 만들어 줄 intensity 수치
    /// </summary>
    float intensity = 0.5f;

    /// <summary>
    /// Intensity 조절용 컴포넌트
    /// </summary>
    Vignette vignette;

    public override void Enter(BossController sender)
    {
        base.Enter(sender);

        sender.StartCoroutine(BerserkCoroutine(sender));
    }

    public override void UpdateState(BossController sender)
    {
        base.UpdateState(sender);
    }

    public override void Exit(BossController sender)
    {
    }

    /// <summary>
    /// 광폭화 코루틴
    /// </summary>
    /// <param name="sender">보스</param>
    /// <returns></returns>
    IEnumerator BerserkCoroutine(BossController sender)
    {
        animator.SetTrigger(Roar_Hash);

        volume.profile.TryGet<Vignette>(out vignette);
        vignette.color.value = sender.berserkColor;

        while (timeElapsed < roarAnimTime)
        {
            // 보스 크기 키우기
            timeElapsed += Time.deltaTime;
            float scaleRatio = timeElapsed / roarAnimTime;
            sender.transform.localScale = (1 + scaleRatio) * Vector3.one;

            // 화면 색깔 바꾸기
            vignette.intensity.value = intensity * scaleRatio;

            yield return null;
        }

        sender.transform.localScale = 2 * Vector3.one;
        agent.speed = sender.berserkMoveSpeed;

        sender.bossStateMachine.TransitionTo(sender.idle);
    }
}
