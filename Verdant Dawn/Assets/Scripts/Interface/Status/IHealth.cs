using System;

public interface IHealth
{
    /// <summary>
    /// HP의 변화를 알리는 델리게이트
    /// </summary>
    public event Action<float> onHealthChange;

    /// <summary>
    /// 죽으면 실행되는 델리게이트
    /// </summary>
    public event Action onDie;

    /// <summary>
    /// 현재 HP를 확인하기 위한 프로퍼티
    /// </summary>
    public float HP { get; }

    /// <summary>
    /// 최대 HP를 확인하기 위한 프로퍼티
    /// </summary>
    public float MaxHP { get; }

    /// <summary>
    /// 살아있는지 확인하기 위한 프로퍼티
    /// </summary>
    public bool IsAlive { get; }

    /// <summary>
    /// 죽으면 실행되는 함수
    /// </summary>
    public void Die();
}
