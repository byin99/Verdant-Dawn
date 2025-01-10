using System;

public interface IMana
{
    /// <summary>
    /// MP의 변화를 알리는 델리게이트
    /// </summary>
    public event Action<float> onManaChange;

    /// <summary>
    /// 현재 MP를 확인하기 위한 프로퍼티
    /// </summary>
    public float MP { get; }

    /// <summary>
    /// 최대 MP를 확인하기 위한 프로퍼티
    /// </summary>
    public float MaxMP { get; }
}
