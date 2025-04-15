using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IQuestGiver
{
    /// <summary>
    /// 퀘스트를 시작하는 델리게이트
    /// </summary>
    public event Action<QuestState> onQuestStateChanged;

    /// <summary>
    /// 퀘스트를 바꾸는 델리게이트
    /// </summary>
    public event Action<QuestData> onQuestChanged;

    /// <summary>
    /// 상호 작용이 가능할 때 실행되는 델리게이트
    /// </summary>
    public event Action onInteraction;

    /// <summary>
    /// 상호 작용이 불가능할 때 실행되는 델리게이트
    /// </summary>
    public event Action offInteraction;

    /// <summary>
    /// 퀘스트 UI를 보여주는 델리게이트
    /// </summary>
    public event Action onQuestStart;

    /// <summary>
    /// 퀘스트 진행 UI를 보여주는 델리게이트
    /// </summary>
    public event Action onQuestProccess;

    /// <summary>
    /// 퀘스트 보상 UI를 보여주는 델리게이트
    /// </summary>
    public event Action onQuestReward;

    /// <summary>
    /// 대화창을 여는 델리게이트
    /// </summary>
    public event Action onConversation;

    /// <summary>
    /// 대화창을 바꾸는 델리게이트
    /// </summary>
    public event Action<string> onChangeConversation;

    /// <summary>
    /// 퀘스트가 모두 종료했을 때 띄우는 대화창
    /// </summary>
    public string endComment = "마을을 구해주셔서 감사합니다.";

    /// <summary>
    /// 현재 퀘스트 상태
    /// </summary>
    QuestState currentState = QuestState.NotStarted;

    /// <summary>
    /// 현재 퀘스트 상태를 알려주는 프로퍼티
    /// 상태가 변경되면 델리게이트를 실행
    /// </summary>
    QuestState CurrentState
    {
        get => currentState;
        set
        {
            if (currentState != value)
            {
                currentState = value;
                onQuestStateChanged?.Invoke(currentState);
            }
        }
    }

    /// <summary>
    /// 현재 퀘스트
    /// </summary>
    QuestData currentQuest = null;

    /// <summary>
    /// 현재 퀘스트를 알려주는 프로퍼티
    /// 상태가 변경되면 델리게이트를 실행
    /// </summary>
    QuestData CurrentQuest
    {
        get => currentQuest;
        set
        {
            if (currentQuest != value)
            {
                currentQuest = value;
                onQuestChanged?.Invoke(currentQuest);
            }
        }
    }

    /// <summary>
    /// 상호 작용이 가능한지 기록하는 변수(true면 상호 작용 가능, false면 상호 작용 불가능)
    /// </summary>
    bool canInteraction = false;

    /// <summary>
    /// 상호작용이 가능한지 알려주는 프로퍼티(읽기 전용)
    /// </summary>
    public bool CanInteraction => canInteraction;

    // 컴포넌트들
    QuestManager questManager;

    private void Awake()
    {
        questManager = GameManager.Instance.QuestManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player가 TriggerEnter 하면
        if (other.gameObject.CompareTag("Player"))
        {
            // 상호 작용 텍스트 보여주기
            onInteraction?.Invoke();
            canInteraction = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Player가 TriggerExit 하면
        if (other.gameObject.CompareTag("Player"))
        {
            // 상호 작용 텍스트 끄기
            offInteraction?.Invoke();
            canInteraction = false;
        }
    }

    /// <summary>
    /// 플레이어와의 상호작용 하는 함수
    /// </summary>
    public void InteractionToPlayer()
    {
        if (CanInteraction)
        {
            switch (CurrentState)
            {
                case QuestState.NotStarted:
                    CurrentQuest = questManager.questDatas[GameManager.Instance.QuestManager.currentQuestCount];
                    onQuestStart?.Invoke();
                    break;

                case QuestState.InProgress:
                    onChangeConversation?.Invoke(CurrentQuest.questProccessComent);
                    onConversation?.Invoke();
                    break;

                case QuestState.ReadyToComplete:
                    onQuestReward?.Invoke();
                    break;

                case QuestState.Completed:
                    onChangeConversation?.Invoke(endComment);
                    onConversation?.Invoke();
                    break;
            }
        }
    }

    /// <summary>
    /// 퀘스트를 주는 함수
    /// </summary>
    /// <param name="player">퀘스트를 받는 사람</param>
    public void GiveQuest(IQuestReceiver player)
    {
        player.ReceiveQuest(currentQuest);
        onQuestProccess?.Invoke();
        CurrentState = QuestState.InProgress;
    }
    
    /// <summary>
    /// 다음 퀘스트로 바꾸는 함수
    /// </summary>
    public void NextQuest()
    {
        questManager.currentQuestCount++;

        if (questManager.questDatas.Length > questManager.currentQuestCount)
        {
            CurrentState = QuestState.NotStarted;

        }
        else
        {
            CurrentState = QuestState.Completed;
        }
    }

    /// <summary>
    /// 퀘스트가 클리어 가능하면 실행되는 함수
    /// </summary>
    public void QuestCanCompleted()
    {
        CurrentState = QuestState.ReadyToComplete;
    }
}
