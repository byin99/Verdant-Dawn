using UnityEngine;

public interface IQuestGiver
{
    /// <summary>
    /// 상호작용이 가능한지를 알려주는 프로퍼티(읽기 전용)
    /// </summary>
    public bool CanInteraction { get; }

    /// <summary>
    /// 플레이어와의 상호작용하는 함수
    /// </summary>
    void InteractionToPlayer();

    /// <summary>
    /// 퀘스트를 주는 함수
    /// </summary>
    /// <param name="player">퀘스트를 받는 주체</param>
    void GiveQuest(IQuestReceiver player);

    /// <summary>
    /// 다음 퀘스트로 바꿔주는 함수
    /// </summary>
    void NextQuest();

    /// <summary>
    /// 퀘스트가 클리어 가능하면 실행되는 함수
    /// </summary>
    void QuestCanCompleted();
}
