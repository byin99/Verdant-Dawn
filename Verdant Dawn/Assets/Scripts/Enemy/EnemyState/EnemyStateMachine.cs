#define PrintLog
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine<T>
{
    /// <summary>
    /// 오브젝트의 정보를 보내주는 변수
    /// </summary>
    private T sender;

    /// <summary>
    /// 오브젝트의 현재 상태를 저장하는 프로퍼티
    /// </summary>
    public IEnemyState<T> Current {  get; private set; }

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="sender">오브젝트</param>
    /// <param name="defaultState">초기 상태</param>
    public EnemyStateMachine(T sender, IEnemyState<T> defaultState)
    {
        this.sender = sender;
        Current = defaultState;
        Current.Enter(sender);  // 초기 상태의 Enter()함수 실행
    }

    /// <summary>
    /// 상태가 전환될 때 실행되는 함수
    /// </summary>
    /// <param name="nextState">다음 상태</param>
    public void TransitionTo(IEnemyState<T> nextState)
    {
        // 전환되기 전 상태의 Exit() 실행
        Current.Exit(sender);
#if PrintLog
        Debug.Log($"[{sender}]의 [{Current}] State에서");
#endif

        // 바꿀 상태로 전화
        Current = nextState;
#if PrintLog
        Debug.Log($"[{sender}]의 [{Current}] State로 바뀜");
#endif

        // 바꾼 상태의 Enter() 실행
        Current.Enter(sender);
    }

    /// <summary>
    /// 각 상태별 Update 함수
    /// </summary>
    public void Update()
    {
        if (Current != null)
        {
            Current.UpdateState(sender);
        }
    }
}
