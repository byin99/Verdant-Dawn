using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestProccessUI : MonoBehaviour
{
    /// <summary>
    /// 퀘스트 내용 텍스트
    /// </summary>
    TextMeshProUGUI questContent;

    // 컴포넌트들
    QuestData currentQuest;
    CanvasGroup canvasGroup;
    PlayerQuest player;
    NPC npc;

    private void Awake()
    {
        questContent = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        player = GameManager.Instance.PlayerQuest;
        npc = GameManager.Instance.NPC;
    }

    private void Start()
    {
        npc.onQuestProccess += ShowQuestProccessUI;
        player.onQuestClear += HideQuestProccessUI;
        player.onChangeQuest += ChangeProccessUI;
        player.onRefreshQuest += RefreshQuestProccessUI;
    }

    /// <summary>
    /// 퀘스트 진행 UI를 활성화하는 메서드
    /// </summary>
    void ShowQuestProccessUI()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 퀘스트 진행 UI를 비활성화하는 메서드
    /// </summary>
    void HideQuestProccessUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 퀘스트 진행 UI를 바꾸는 메서드
    /// </summary>
    /// <param name="questData">진행중인 퀘스트</param>
    void ChangeProccessUI(QuestData questData)
    {
        currentQuest = questData;
        if (questData is QuestData_Suppression)
        {
            QuestData_Suppression suppressionQuest = questData as QuestData_Suppression;
            questContent.text = $"{suppressionQuest.questDescription}\n" +
                $"{suppressionQuest.enemyType} {suppressionQuest.currentEnemyCount} / {suppressionQuest.enemyCount}";
        }

        else
        {
            questContent.text = $"{questData.questDescription}";
        }
    }

    /// <summary>
    /// 퀘스트 진행 UI를 갱신하는 함수
    /// </summary>
    void RefreshQuestProccessUI()
    {
        if (currentQuest is QuestData_Suppression)
        {
            QuestData_Suppression suppressionQuest = currentQuest as QuestData_Suppression;
            questContent.text = $"{suppressionQuest.questDescription}\n" +
                $"{suppressionQuest.enemyType} {suppressionQuest.currentEnemyCount} / {suppressionQuest.enemyCount}";
        }
    }
}
