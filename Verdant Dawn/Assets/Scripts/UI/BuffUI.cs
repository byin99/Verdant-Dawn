using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuffUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 아이템 데이터
    /// </summary>
    ItemData itemData;

    /// <summary>
    /// 버프 타이머
    /// </summary>
    TextMeshProUGUI buffTimer;

    // 컴포넌트들
    CanvasGroup canvasGroup;
    PlayerStatus player;
    DetailInfoUI detailInfoUI;

    private void Awake()
    {
        Transform child = transform.GetChild(2);
        buffTimer = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        detailInfoUI = child.GetComponent<DetailInfoUI>();

        canvasGroup = GetComponent<CanvasGroup>();

        player = GameManager.Instance.PlayerStatus;
    }

    private void Start()
    {
        CloseBuffUI();

        player.onBuff += ShowBuffUI;
        player.offBuff += CloseBuffUI;
        player.onUpdateBuffTime += OnRefreshBuffTime;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detailInfoUI.Open(itemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        detailInfoUI.Close();
    }

    /// <summary>
    /// BuffUI 보여주는 함수
    /// </summary>
    void ShowBuffUI(ItemData itemData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        this.itemData = itemData;
    }

    /// <summary>
    /// BuffUI 닫는 함수
    /// </summary>
    void CloseBuffUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 버프 시간 최적화
    /// </summary>
    /// <param name="buffTime">남은 버프 시간</param>
    void OnRefreshBuffTime(float buffTime)
    {
        if (buffTime > 60.0f)
        {
            buffTimer.text = $"{(int)(buffTime / 60)}분"; 
        }

        else
        {
            buffTimer.text = $"{(int)buffTime}초";
        }
    }
}
