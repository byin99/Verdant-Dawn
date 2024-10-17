using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    /// <summary>
    /// 재활용 오브젝트가 비활성화 될 때 실행되는 델리게이트
    /// </summary>
    public Action onDisable = null;

    protected virtual void OnEnable()
    {
        StopAllCoroutines();    // 이전에 실행 중이던 코루틴 모두 정지

        OnReset();              // 각 클래스별 리셋 처리
    }

    protected virtual void OnDisable()
    {
        onDisable?.Invoke();    // onDisable이 null이 아니면 실행하라
    }

    /// <summary>
    /// 재활용 될떄 처리되는 함수(빈 함수)
    /// </summary>
    protected virtual void OnReset()
    {

    }

    /// <summary>
    /// 일정 시간 후에 게임오브젝트를 자동으로 비활성화시키는 함수
    /// </summary>
    /// <param name="time">기다릴 시간</param>
    protected void DisableTimer(float time = 0.0f)
    {
        StartCoroutine(LifeOver(time));
    }

    IEnumerator LifeOver(float time = 0.0f)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
