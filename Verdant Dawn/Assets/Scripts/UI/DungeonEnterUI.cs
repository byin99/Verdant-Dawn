using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonEnterUI : MonoBehaviour
{
    /// <summary>
    /// 보스 이미지
    /// </summary>
    Image bossImage;

    /// <summary>
    /// 보스 이름 텍스트
    /// </summary>
    TextMeshProUGUI bossName;

    /// <summary>
    /// 던전 이름 텍스트
    /// </summary>
    TextMeshProUGUI dungeonName;

    /// <summary>
    /// 입장 취소 버튼
    /// </summary>
    Button cancelButton;

    /// <summary>
    /// 입장 버튼
    /// </summary>
    Button acceptButton;

    /// <summary>
    /// 포탈 정보
    /// </summary>
    Portal currentPortal;

    // 컴포넌트들
    CanvasGroup canvasGroup;
    PlayerPortal player;
    AudioManager audioManager;

    private void Awake()
    {
        Transform child = transform.GetChild(2);
        bossImage = child.GetComponent<Image>();

        child = transform.GetChild(3);
        bossName = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(4);
        dungeonName = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(6);
        cancelButton = child.GetComponent<Button>();

        child = transform.GetChild(7);
        acceptButton = child.GetComponent<Button>();

        canvasGroup = GetComponent<CanvasGroup>();

        player = GameManager.Instance.PlayerPortal;

        audioManager = GameManager.Instance.AudioManager;
    }

    private void Start()
    {
        cancelButton.onClick.AddListener(() =>
        {
            audioManager.PlaySound2D(AudioCode.Click, 1.0f);
            HideDungeonEnterUI();
            player.offDungeonPortal?.Invoke();
        });

        acceptButton.onClick.AddListener(() =>
        {
            audioManager.PlaySound2D(AudioCode.Click, 1.0f);
            HideDungeonEnterUI();
            player.GetPortal(currentPortal);
        });

        player.onDungeonPortal += (portal) =>
        {
            audioManager.PlaySound2D(AudioCode.Interaction, 1.0f);
            currentPortal = portal;
            DungeonMapInfo mapInfo = portal.connectMap as DungeonMapInfo;
            RefreshDungeonEnterUI(mapInfo);
            ShowDungeonEnterUI();
        };
    }

    /// <summary>
    /// 던전 입장 UI를 보여주는 함수
    /// </summary>
    void ShowDungeonEnterUI()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 던전 입장 UI를 숨기는 함수
    /// </summary>
    void HideDungeonEnterUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    ///  맵 정보를 받아와서 UI를 갱신하는 함수
    /// </summary>
    /// <param name="mapInfo">던전 Info</param>
    void RefreshDungeonEnterUI(DungeonMapInfo mapInfo)
    {
        bossImage.material = mapInfo.bossImage;
        bossName.text = mapInfo.bossName;
        dungeonName.text = mapInfo.mapName;
    }
}
