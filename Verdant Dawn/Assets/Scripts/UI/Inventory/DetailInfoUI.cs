using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CanvasGroup))]
public class DetailInfoUI : MonoBehaviour
{
    /// <summary>
    /// 알파값이 변하는 속도
    /// </summary>
    public float alphaChangeSpeed = 10.0f;

    /// <summary>
    /// 일시정지 모드 상태(true면 일시정지모드, false면 일반모드)
    /// </summary>
    public bool isPause = false;

    /// <summary>
    /// 아이템 아이콘
    /// </summary>
    Image icon;

    /// <summary>
    /// 아이템 이름 텍스트
    /// </summary>
    TextMeshProUGUI itemName;

    /// <summary>
    /// 아이템 설명 텍스트
    /// </summary>
    TextMeshProUGUI description;

    /// <summary>
    /// 일시 정지 모드 확인 및 설정용 프로퍼티
    /// </summary>
    public bool IsPaused
    {
        get => isPause;
        set
        {
            isPause = value;
            if (isPause)
            {
                Close();    // 일시 정지 모드로 들어가면 무조건 닫기
            }
        }
    }

    //컴포넌트들
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;

        Transform child = transform.GetChild(0);
        icon = child.GetComponent<Image>();
        child = transform.GetChild(1);
        itemName = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        description = child.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 상세정보창을 여는 함수
    /// </summary>
    /// <param name="itemData">열릴 때 표시할 아이템 데이터</param>
    public void Open(ItemData itemData)
    {
        // 일시정지 상태가 아니고, 아이템 데이터 필수
        if (!isPause && itemData != null)
        {
            icon.sprite = itemData.itemIcon;
            itemName.text = itemData.itemName;
            description.text = itemData.itemDescription;

            canvasGroup.alpha = 0.01f;      // MovePosition를 실행시키기 위해 0보다 커야 함
            MovePosition(Mouse.current.position.ReadValue());   // 커서 위치로 창을 옮기기

            StopAllCoroutines();
            StartCoroutine(FadeIn());
        }
    }

    /// <summary>
    /// 상세정보창을 닫는 함수
    /// </summary>
    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    /// <summary>
    /// 상세 정보창의 위치를 옮기는 함수
    /// </summary>
    /// <param name="screen"></param>
    public void MovePosition(Vector2 screen)
    {
        if (canvasGroup.alpha > 0.0f)
        {
            RectTransform rect = (RectTransform)transform;
            Canvas canvas = GetComponentInParent<Canvas>(); // 상위 Canvas 가져오기
            Camera uiCamera = canvas.worldCamera; // UI용 카메라 가져오기

            Vector3 worldPoint;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, screen, uiCamera, out worldPoint))
            {
                rect.position = worldPoint; // 변환된 월드 좌표 적용
            }
        }
    }

    /// <summary>
    /// 상세 정보창을 서서히 켜는 코루틴
    /// </summary>
    IEnumerator FadeIn()
    {
        // canvasGroup.alpha가 1이 될때까지 계속 증가
        while (canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha += Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }

    /// <summary>
    /// 상세 정보창을 서서히 끄는 코루틴
    /// </summary>
    IEnumerator FadeOut()
    {
        // canvasGroup.alpha가 1이 될때까지 계속 감소
        while (canvasGroup.alpha > 0.0f)
        {
            canvasGroup.alpha -= Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
    }
}
