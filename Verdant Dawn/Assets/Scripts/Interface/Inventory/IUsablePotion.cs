using System;

public interface IUsablePotion
{
    /// <summary>
    /// 버프가 시작되면 실행되는 델리게이트
    /// </summary>
    public event Action<ItemData> onBuff;

    /// <summary>
    /// 버프가 끝나면 실행되는 델리게이트
    /// </summary>
    public event Action offBuff;

    /// <summary>
    /// 버프시간이 업데이트되면 실행되는 델리게이트
    /// </summary>
    public event Action<float> onUpdateBuffTime;

    /// <summary>
    /// 힐이 가능한지 알려주는 프로퍼티
    /// </summary>
    bool CanHeal { get; }

    /// <summary>
    /// HP를 즉시 회복시키는 함수
    /// </summary>
    /// <param name="healRatio">회복 비율</param>
    void HealthHeal(float healRatio);

    /// <summary>
    /// 최대 마나량을 증가시키는 함수
    /// </summary>
    /// <param name="manaRatio">증가율</param>
    /// <param name="buffTime">버프 시간</param>
    void BoostMaxMana(ItemData itemData, float manaRatio, float buffTime);

    /// <summary>
    /// 최대 마나량을 복구시키는 함수
    /// </summary>
    /// <param name="manaRatio">복구율</param>
    void RestoreMaxMana(float manaRatio);

    /// <summary>
    /// 아이덴티티 게이지를 풀로 채워주는 함수
    /// </summary>
    void FillIdentityGauge();
}
