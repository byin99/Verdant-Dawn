public interface IQuestReceiver
{
    /// <summary>
    /// 퀘스트를 받는 함수
    /// </summary>
    /// <param name="quest">받을 퀘스트</param>
    void ReceiveQuest(QuestData quest);

    /// <summary>
    /// 퀘스트의 보상을 수령하는 함수
    /// </summary>
    /// <param name="exp">보상 경험치</param>
    /// <param name="itemCode">보상 아이템 종류</param>
    /// <param name="itemCount">보상 아이템 개수</param>
    void GetQuestReward(float exp, ItemCode itemCode, uint itemCount);

    /// <summary>
    /// NPC와 상호작용하는 함수
    /// </summary>
    void InteractionToNPC();
}
