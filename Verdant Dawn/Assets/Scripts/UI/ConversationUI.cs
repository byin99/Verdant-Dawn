using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour
{
    /// <summary>
    /// 대화창 Text
    /// </summary>
    TextMeshProUGUI conversationUI;

    /// <summary>
    /// 대화창 끄기 버튼
    /// </summary>
    Button offButton;

    // 컴포넌트들
    CanvasGroup canvasGroup;
    NPC npc;
    AudioManager audioManager;

    private void Awake()
    {
        conversationUI = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        offButton = transform.GetChild(3).GetComponent<Button>();

        canvasGroup = GetComponent<CanvasGroup>();
        npc = GameManager.Instance.NPC;
        audioManager = GameManager.Instance.AudioManager;
    }

    private void Start()
    {
        HideConversationUI();

        offButton.onClick.AddListener(() =>
        {
            audioManager.PlaySound2D(AudioCode.Click, 1.0f);
            HideConversationUI();
        });

        npc.onConversation += () => 
        {
            audioManager.PlaySound2D(AudioCode.Interaction, 1.0f);
            ShowConversationUI();
        };

        npc.onChangeConversation += ChangeConversationText;
    }

    /// <summary>
    /// 대화창을 보여주는 함수
    /// </summary>
    void ShowConversationUI()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 대화창을 숨기는 함수
    /// </summary>
    void HideConversationUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 대화텍스트를 바꾸는 함수
    /// </summary>
    /// <param name="text">대화창에 띄울 텍스트</param>
    void ChangeConversationText(string text)
    {
        conversationUI.text = text;
    }
}
