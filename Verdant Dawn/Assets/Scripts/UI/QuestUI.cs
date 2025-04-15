using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    /// <summary>
    /// 이 퀘스트의 데이터
    /// </summary>
    QuestData questData;

    /// <summary>
    /// 거절 버튼
    /// </summary>
    Button cancelButton;

    /// <summary>
    /// 수락 버튼
    /// </summary>
    Button acceptButton;

    /// <summary>
    /// 퀘스트 이름
    /// </summary>
    TextMeshProUGUI questTitle;

    /// <summary>
    /// 퀘스트 내용
    /// </summary>
    TextMeshProUGUI questContent;

    /// <summary>
    /// 퀘스트 보상 아이템 이미지
    /// </summary>
    Image questRewardItemImage;

    /// <summary>
    /// 퀘스트 보상 아이템 개수
    /// </summary>
    TextMeshProUGUI questRewardItemCount;

    /// <summary>
    /// 퀘스트 보상 경험치량
    /// </summary>
    TextMeshProUGUI questRewardExpCount;

    // 컴포넌트들
    CanvasGroup canvasGroup;
    PlayerQuest player;
    ItemDataManager itemDataManager;
    NPC npc;

    private void Awake()
    {
        cancelButton = transform.GetChild(2).GetComponent<Button>();
        acceptButton = transform.GetChild(3).GetComponent<Button>();
        questTitle = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        questContent = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        questRewardItemImage = transform.GetChild(6).GetComponent<Image>();
        questRewardItemCount = transform.GetChild(7).GetComponent<TextMeshProUGUI>();
        questRewardExpCount = transform.GetChild(9).GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();
        player = GameManager.Instance.PlayerQuest;
        itemDataManager = GameManager.Instance.ItemDataManager;
        npc = GameManager.Instance.NPC;
    }

    private void Start()
    {
        acceptButton.onClick.AddListener(AcceptQuest);
        cancelButton.onClick.AddListener(CancelQuest);

        npc.onQuestStart += ShowQuestUI;
        npc.onQuestChanged += ChangeQuest;

        HideQuestUI();
    }

    /// <summary>
    /// 퀘스트 UI를 변경하는 함수
    /// </summary>
    /// <param name="name">퀘스트 이름</param>
    /// <param name="content">퀘스트 내용</param>
    void ChangeQuest(QuestData currentQuest)
    {
        questData = currentQuest;
        questTitle.text = currentQuest.questTitle;
        questContent.text = currentQuest.questContent;
        questRewardItemImage.sprite = itemDataManager[currentQuest.questItemCode].itemIcon;
        questRewardItemCount.text = $"x {currentQuest.itemCount}";
        questRewardExpCount.text = $"{currentQuest.questExpReward} EXP";
    }

    /// <summary>
    /// 퀘스트 UI를 보여주는 함수
    /// </summary>
    void ShowQuestUI()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 퀘스트 UI를 숨기는 함수
    /// </summary>
    void HideQuestUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 퀘스트 수락 버튼을 눌렀을 때 호출되는 함수
    /// </summary>
    void AcceptQuest()
    {
        npc.GiveQuest(player);
        HideQuestUI();
    }

    /// <summary>
    /// 퀘스트 거절 버튼을 눌렀을 때 호출되는 함수
    /// </summary>
    void CancelQuest()
    {
        HideQuestUI();
    }
}
