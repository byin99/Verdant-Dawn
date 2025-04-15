public enum QuestState
{
    NotStarted,         // 퀘스트 시작 전
    InProgress,         // 진행 중
    ReadyToComplete,    // 클리어 직전 (NPC에게 보고만 하면 되는 상태 등)
    Completed,          // 퀘스트 완료
}
