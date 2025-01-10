using System.Collections;

/// <summary>
/// 쿨타임 보여주는 UI 인터페이스
/// </summary>
public interface ICoolTime
{
    /// <summary>
    /// 쿨타임 보여주는 함수
    /// </summary>
    public void ShowCoolTime();

    /// <summary>
    /// 쿨타임을 보여주는 코루틴 함수
    /// </summary>
    public IEnumerator CoolTimeCoroutine();
}