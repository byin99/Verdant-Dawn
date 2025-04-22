using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestRewardUI : MonoBehaviour
{
    /// <summary>
    /// 현재 보상받는 퀘스트
    /// </summary>
    QuestData currentQuestData;

    /// <summary>
    /// 보상 아이템 아이콘
    /// </summary>
    Image itemIcon;

    /// <summary>
    /// 보상 아이템 개수
    /// </summary>
    TextMeshProUGUI itemCount;

    /// <summary>
    /// 보상 경험치 량
    /// </summary>
    TextMeshProUGUI expAmount;

    /// <summary>
    /// 보상 수락 버튼
    /// </summary>
    Button acceptButton;

    // 컴포넌트들
    CanvasGroup canvasGroup;
    ItemDataManager itemDataManager;
    PlayerQuest player;
    AudioManager audioManager;
    NPC npc;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = transform.GetChild(2).GetComponent<Image>();
        itemCount = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        expAmount = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        acceptButton = transform.GetChild(6).GetComponent<Button>();

        itemDataManager = GameManager.Instance.ItemDataManager;
        player = GameManager.Instance.PlayerQuest;
        audioManager = GameManager.Instance.AudioManager;
        npc = GameManager.Instance.NPC;
    }

    private void Start()
    {
        acceptButton.onClick.AddListener(() => 
        {
            audioManager.PlaySound2D(AudioCode.ItemGet);
            OnAcceptButton(); 
        });

        npc.onQuestReward += ShowQuestRewardUI;
        player.onChangeQuest += ChangeRewardUI;
    }

    /// <summary>
    /// QuestRewardUI를 활성화하는 메서드
    /// </summary>
    void ShowQuestRewardUI()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// QuestRewardUI를 비활성화하는 메서드
    /// </summary>
    void HideQuestRewardUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 퀘스트 보상 UI를 바꾸는 메서트
    /// </summary>
    /// <param name="questData">보상으로 바꿀 퀘스트 정보</param>
    void ChangeRewardUI(QuestData questData)
    {
        currentQuestData = questData;
        itemIcon.sprite = itemDataManager[questData.questItemCode].itemIcon;
        itemCount.text = $"x {questData.itemCount}";
        expAmount.text = $"{questData.questExpReward} EXP";
    }

    /// <summary>
    /// 수락 버튼 클릭 시 호출되는 메서드
    /// </summary>
    void OnAcceptButton()
    {
        currentQuestData.QuestClear(player.gameObject);
        npc.NextQuest();
        HideQuestRewardUI();
    }
}
