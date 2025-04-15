using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatus : MonoBehaviour
{
    /// <summary>
    /// 퀘스트 이미지들
    /// </summary>
    public Sprite[] questSprites;

    /// <summary>
    /// 퀘스트 진행 이미지
    /// </summary>
    SpriteRenderer questImage;

    // 컴포넌트들
    NPC npc;

    private void Awake()
    {
        questImage = transform.GetComponent<SpriteRenderer>();
        npc = transform.parent.GetComponent<NPC>();
    }

    private void Start()
    {
        ShowQuestStatus();
        npc.onQuestStateChanged += ChangeImage;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    /// <summary>
    /// 퀘스트 상태를 보여주는 함수
    /// </summary>
    void ShowQuestStatus()
    {
        questImage.enabled = true;
    }

    /// <summary>
    /// 퀘스트 상태를 숨기는 함수
    /// </summary>
    void HideQuestStatus()
    {
        questImage.enabled = false;
    }

    /// <summary>
    /// 퀘스트 상태 이미지를 바꾸는 함수
    /// </summary>
    /// <param name="questState">현재 퀘스트 상태</param>
    void ChangeImage(QuestState questState)
    {
        switch (questState)
        {
            case QuestState.NotStarted:
                questImage.sprite = questSprites[0];
                questImage.color = Color.green;
                break;

            case QuestState.InProgress:
                questImage.sprite = questSprites[1];
                questImage.color = Color.white;
                break;

            case QuestState.ReadyToComplete:
                questImage.sprite = questSprites[2];
                questImage.color = Color.yellow;
                break;

            case QuestState.Completed:
                HideQuestStatus();
                break;
        }
    }
}
