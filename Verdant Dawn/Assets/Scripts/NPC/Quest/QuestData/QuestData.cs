using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Quest Data", order = 1)]
public class QuestData : ScriptableObject, IQuest
{
    [Header("퀘스트 기본 정보")]
    public string questTitle = "퀘스트 이름";
    public string questContent = "퀘스트 내용";
    public string questDescription = "퀘스트 설명";
    public string questProccessComent = "퀘스트 클리어 설명";

    [Header("퀘스트 보상")]
    public float questExpReward = 100f;
    public ItemCode questItemCode = ItemCode.HealingPotion;
    public uint itemCount = 1;

    /// <summary>
    /// 퀘스트 클리어 시 호출되는 함수
    /// </summary>
    public bool QuestClear(GameObject target)
    {
        bool result = false;

        IQuestReceiver player = target.GetComponent<IQuestReceiver>();

        if (player != null)
        {
            // 퀘스트 클리어 처리
            player.GetQuestReward(questExpReward, questItemCode, itemCount);
            result = true;
        }

        return result;
    }
}
