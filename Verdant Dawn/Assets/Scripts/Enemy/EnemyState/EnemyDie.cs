using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyDie : EnemyBase
{
    /// <summary>
    /// Dissolve 진행 시간(사망 연출 시간)
    /// </summary>
    public float dissolveDuration = 1.0f;

    public override void Enter(EnemyController sender)
    {
        base.Enter(sender);

        sender.StartCoroutine(Dissolve(sender));    // 사망 연출 시작
    }

    public override void UpdateState(EnemyController sender)
    {
    }

    public override void Exit(EnemyController sender)
    {
    }

    /// <summary>
    /// Dissolve 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Dissolve(EnemyController sender)
    {
        animator.SetTrigger(Death_Hash);                    // 죽는 애니메이션 실행

        float fadeNormalize = 1 / dissolveDuration;         // 나누기 연산을 줄이기 위해 미리 계산
        float timeElapsed = 0.0f;                           // 시간 누적용

        yield return new WaitForSeconds(4.0f);              // 사망 후 2초만 기다리기

        while (timeElapsed < dissolveDuration)              // 시간 될때까지 반복
        {
            timeElapsed += Time.deltaTime;                  // 시간 누적

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat(Fade_ID, 1 - (timeElapsed * fadeNormalize));  // fade값을 1 -> 0으로 점점 감소시키기
            }

            yield return null;
        }

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat(Fade_ID, 0);
        }

        sender.gameObject.SetActive(false);                // 재활용 하기 위해서 SetActive false만 하기 
    }
}
