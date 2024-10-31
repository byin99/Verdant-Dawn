public interface IState<T>
{
    /// <summary>
    /// 상태에 들어갔을 때 실행하는 함수
    /// </summary>
    public void Enter(T sender);

    /// <summary>
    /// 상태 중일 때 실행하는 함수
    /// </summary>
    public void UpdateState(T sender);

    /// <summary>
    /// 상태에서 다른 상태로 넘어갈 때 실행하는 함수
    /// </summary>
    public void Exit(T sender);
}

