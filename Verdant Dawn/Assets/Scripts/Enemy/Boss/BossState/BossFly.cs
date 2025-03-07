using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BossFly : BossBase
{
    /// <summary>
    /// 하늘에서 대기하는 시간
    /// </summary>
    float waitingTime = 1.0f;

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
    /// 하늘에서 떨어지는 비율
    /// </summary>
    float landingRatio = 40.0f;

    /// <summary>
    /// 보스 빨간장판
    /// </summary>
    BossEffect bossRedFloorboard;

    public override void Enter(BossController sender)
    {
        base.Enter(sender);

        timeElapsed = 0.0f;
        agent.enabled = false;

        // 공중 공격 시작
        sender.StartCoroutine(FlyAttackCoroutine(sender));
    }

    public override void UpdateState(BossController sender)
    {
        base.UpdateState(sender);
    }

    public override void Exit(BossController sender)
    {
        agent.enabled = true;
    }

    /// <summary>
    /// 공중 공격 코루틴
    /// </summary>
    /// <param name="sender">보스</param>
    IEnumerator FlyAttackCoroutine(BossController sender)
    {
        // 날기 시작
        animator.SetTrigger(Fly_Hash);

        yield return new WaitForSeconds(waitingTime);

        // 하늘로 올라가기
        while (timeElapsed < airTime)
        {
            rigid.transform.position = new Vector3(rigid.transform.position.x, timeElapsed * flyRatio, rigid.transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 하늘에서 잠깐 대기하기
        yield return new WaitForSeconds(waitingTime);

        // 플레이어 위로 이동하기
        rigid.transform.position = new Vector3(player.transform.position.x, rigid.transform.position.y, player.transform.position.z);
        
        if (sender.isBerserk)
        {
            bossRedFloorboard = Factory.Instance.GetBossRedFloorboard_B(player.transform.position);
        }
        
        else
        {
            bossRedFloorboard = Factory.Instance.GetBossRedFloorboard(player.transform.position);
        
        }
        timeElapsed = 0.0f;

        // 공중에서 플레이어에게 내리꼿기
        while (timeElapsed < airTime)
        {
            rigid.transform.position = new Vector3(rigid.transform.position.x, (airTime - timeElapsed) * landingRatio, rigid.transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 이펙트 및 데미지 주기
        if (sender.isBerserk)
        {
            Factory.Instance.GetBossAttack3_B(bossRedFloorboard.transform.position);
        }

        else
        {
            Factory.Instance.GetBossAttack3(bossRedFloorboard.transform.position);

        }

        // 빨간 장판 없애기
        bossRedFloorboard.gameObject.SetActive(false);

        // 공중 공격 끝
        sender.bossStateMachine.TransitionTo(sender.idle);
    }
}
