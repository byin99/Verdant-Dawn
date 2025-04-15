using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionStatus : MonoBehaviour
{
    /// <summary>
    /// 상호 작용 텍스트
    /// </summary>
    TextMeshPro interactionText;

    // 컴포넌트들
    NPC npc;

    private void Awake()
    {
        interactionText = transform.GetComponent<TextMeshPro>();

        npc = transform.parent.parent.GetComponent<NPC>();
    }

    private void Start()
    {
        HideInteractionText();

        npc.onInteraction += ShowInteractionText;
        npc.offInteraction += HideInteractionText;
    }

    /// <summary>
    /// 상호 작용 텍스트를 보여주는 함수
    /// </summary>
    void ShowInteractionText()
    {
        interactionText.enabled = true;
    }

    /// <summary>
    /// 상호 작용 텍스트를 숨기는 함수
    /// </summary>
    void HideInteractionText()
    {
        interactionText.enabled = false;
    }
}
