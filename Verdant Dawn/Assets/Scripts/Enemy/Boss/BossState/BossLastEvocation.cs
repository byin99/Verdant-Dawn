using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLastEvocation : BossBase
{
    /// <summary>
    /// 공중으로 날거나 착지하는 시간
    /// </summary>
    float airTime = 2.0f;

    /// <summary>
    /// 시간을 재는 변수
    /// </summary>
    float timeElapsed = 0.0f;

    /// <summary>
    /// 하늘로 올라가고 내려가는 비율
    /// </summary>
    float flyRatio = 10.0f;

    /// <summary>
    /// 소환 시간
    /// </summary>
    float evocationTime = 40.0f;

    /// <summary>
    /// 소환수 죽은 수
    /// </summary>
    int evocationDeathCount = 0;

    /// <summary>
    /// 소환된 소환수 수
    /// </summary>
    int evocationMaxCount = 20;

    /// <summary>
    /// 소환 성공 여부
    /// </summary>
    bool isSuccessEvocation = false;

    /// <summary>
    /// 안전지대 이펙트
    /// </summary>
    SafetyZoneEffect safetyZoneEffect;

    public override void Enter(BossController sender)
    {
        base.Enter(sender);

        sender.StartCoroutine(EvocationCoroutine(sender));
    }

    public override void UpdateState(BossController sender)
    {
    }

    public override void Exit(BossController sender)
    {
    }

    /// <summary>
    /// 랜덤한 위치에 몹을 소환하는 함수
    /// </summary>
    /// <param name="sender">보스 Controller</param>
    void RandomEvocation(BossController sender)
    {
        for (int i = 0; i < evocationMaxCount; i++)
        {
            float randomNum1 = Random.Range(-30.0f, 30.0f);
            float randomNum2 = Random.Range(-30.0f, 30.0f);

            BossEvocationController evocation = Factory.Instance.GetEvilWatcher(sender.transform.position + new Vector3(randomNum1, 0, randomNum2));
            evocation.transform.LookAt(sender.transform.position);

            EnemyStatus status = evocation.GetComponent<EnemyStatus>();
            status.onDie += EvocationDie;
        }
    }

    /// <summary>
    /// 소환수가 죽으면 실행되는 델리게이트
    /// </summary>
    void EvocationDie()
    {
        evocationDeathCount++;
        bossEvocationUI.UpdateTargetCount(evocationDeathCount, evocationMaxCount);
    }

    /// <summary>
    /// Evocation 코루틴
    /// </summary>
    /// <param name="sender">보스 Controller</param>
    IEnumerator EvocationCoroutine(BossController sender)
    {
        // Evocation 시작
        animator.SetTrigger(Roar_Hash);
        Factory.Instance.GetBossEvocation_B(sender.transform.position);
        RandomEvocation(sender);

        yield return new WaitForSeconds(roarAnimTime);

        // 하늘로 날아가기
        agent.enabled = false;
        animator.SetTrigger(Fly_Hash);
        while (timeElapsed < airTime)
        {
            rigid.transform.position = new Vector3(rigid.transform.position.x, timeElapsed * flyRatio, rigid.transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 소환 패턴 시간 재기
        timeElapsed = 0.0f;
        bossEvocationUI.UpdateTargetCount(evocationDeathCount, evocationMaxCount);
        bossEvocationUI.ShowBossEvocationUI();
        while (timeElapsed < evocationTime)
        {
            timeElapsed += Time.deltaTime;
            bossEvocationUI.UpdateTimer(evocationTime - timeElapsed);

            // 소환수를 다 잡았다면
            if (!isSuccessEvocation && evocationDeathCount == evocationMaxCount)
            {
                // 안전지대 소환
                isSuccessEvocation = true;
                float randomNum1 = Random.Range(-15.0f, 15.0f);
                float randomNum2 = Random.Range(-15.0f, 15.0f);
                safetyZoneEffect = Factory.Instance.GetSafetyZoneEffect(new Vector3(sender.transform.position.x + randomNum1, 0, sender.transform.position.z + randomNum2));
            }

            yield return null;
        }

        // 하늘에서 내려오기
        timeElapsed = 0.0f;
        bossEvocationUI.OffBossEvocationUI();
        while (timeElapsed < airTime)
        {
            rigid.transform.position = new Vector3(rigid.transform.position.x, (airTime - timeElapsed) * flyRatio, rigid.transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(roarAnimTime);

        // 마지막 광범위한 공격
        animator.SetTrigger(Roar_Hash);
        Factory.Instance.GetBossAttack4_B(new Vector3(sender.transform.position.x, 0, sender.transform.position.z + 2));

        yield return new WaitForSeconds(roarAnimTime);

        //Evocation 끝
        if (safetyZoneEffect != null)
            safetyZoneEffect.gameObject.SetActive(false);

        if (player.gameObject.layer == LayerMask.NameToLayer("Invincible"))
            player.gameObject.layer = LayerMask.NameToLayer("Player");

        agent.enabled = true;
        sender.bossStateMachine.TransitionTo(sender.staggered);
    }
}
