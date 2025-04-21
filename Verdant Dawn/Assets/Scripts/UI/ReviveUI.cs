using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ReviveUI : MonoBehaviour
{
    /// <summary>
    /// 부활 UI의 FadeIn과 FadeOut에 걸리는 시간
    /// </summary>
    public float fadeTime = 1.0f;

    /// <summary>
    /// 부활 버튼
    /// </summary>
    Button reviveButton;

    // 컴포넌트들
    CanvasGroup canvasGroup;
    Player player;
    PlayerStatus playerStatus;
    Vignette vignette;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        reviveButton = transform.GetChild(0).GetComponent<Button>();
        player = GameManager.Instance.Player;
        playerStatus = GameManager.Instance.PlayerStatus;
        GameManager.Instance.Volume.profile.TryGet<Vignette>(out vignette);
        
    }

    private void Start()
    {
        reviveButton.onClick.AddListener(() =>
        {
            HideReviveUI();
            player.onRevive?.Invoke();
        });

        playerStatus.onDie += ShowReviveUI;
    }

    /// <summary>
    /// 부활 UI를 보여주는 함수
    /// </summary>
    void ShowReviveUI()
    {
        StartCoroutine(ShowReviveUICoroutine());
    }

    /// <summary>
    /// 부활 UI를 숨기는 함수
    /// </summary>
    void HideReviveUI()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        vignette.intensity.value = 0.0f;
    }

    /// <summary>
    /// 부활 UI를 보여주는 코루틴
    /// </summary>
    IEnumerator ShowReviveUICoroutine()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = timeElapsed;
            vignette.intensity.value = timeElapsed;
            yield return null;
        }

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
