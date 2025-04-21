using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortalUI : MonoBehaviour
{
    /// <summary>
    /// 화면이 어두워져있는 시간
    /// </summary>
    public float fadeTime = 1.0f;

    /// <summary>
    /// 포탈 Text
    /// </summary>
    TextMeshProUGUI portalText;

    /// <summary>
    /// 포탈 Panel
    /// </summary>
    Image portalPanel;

    // 컴포넌트들
    PlayerPortal playerPortal;


    private void Awake()
    {
        portalText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        portalPanel = transform.GetChild(1).GetComponent<Image>();
        playerPortal = GameManager.Instance.PlayerPortal;
    }

    private void Start()
    {
        playerPortal.onLocalPortal += OnPortalUI;
    }

    /// <summary>
    /// 포탈을 타는 함수
    /// </summary>
    /// <param name="mapInfo">맵 정보</param>
    void OnPortalUI(MapInfo mapInfo)
    {
        portalText.text = mapInfo.mapName;

        StartCoroutine(FadeInOut());
    }

    /// <summary>
    /// 화면 전환하는 코루틴
    /// </summary>
    IEnumerator FadeInOut()
    {
        // 화면 Fade Out
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            portalPanel.color = new Color(0, 0, 0, timeElapsed);
            yield return null;
        }
        portalPanel.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(fadeTime);

        // 화면 Fade In
        portalText.color = new Color(1, 1, 1, 1);
        while (timeElapsed > 0.0f)
        {
            timeElapsed -= Time.deltaTime;
            portalPanel.color = new Color(0, 0, 0, timeElapsed);
            yield return null;
        }
        portalPanel.color = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(fadeTime);

        // 글자 Fade Out
        timeElapsed = 1.0f;
        while (timeElapsed > 0.0f)
        {
            timeElapsed -= Time.deltaTime;
            portalText.color = new Color(1, 1, 1, timeElapsed);
            yield return null;
        }
    }
}
