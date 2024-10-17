public interface IEnemyState
{
    /// <summary>
    /// 적이 상태에 들어갔을 때 실행하는 함수
    /// </summary>
    public void Enter()
    {
    }

    /// <summary>
    /// 적이 상태중일 때 실행되는 함수
    /// </summary>
    public void UpdateState()
    {
    }

    /// <summary>
    /// 적이 상태에서 벗어났을 때 실행되는 함수
    /// </summary>
    public void Exit()
    {
    }
}

