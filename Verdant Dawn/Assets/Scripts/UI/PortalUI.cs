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
    /// 포탈을 타는지 여부(true면 포탈을 타고있다, false면 포탈을 타고 있지 않다)
    /// </summary>
    [HideInInspector]
    public bool isPortal = false;

    /// <summary>
    /// 포탈 Text
    /// </summary>
    TextMeshProUGUI portalText;

    /// <summary>
    /// 포탈 Panel
    /// </summary>
    Image portalPanel;

    private void Awake()
    {
        portalText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        portalPanel = transform.GetChild(1).GetComponent<Image>();
    }

    /// <summary>
    /// 존끼리 이동할 때 호출되는 함수
    /// </summary>
    /// <param name="enemyType">이동하는 존</param>
    public void MoveToZone(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Ghoul:
                portalText.text = "구울들의 땅";
                break;

            case EnemyType.Skeleton:
                portalText.text = "해골들의 땅";
                break;

            case EnemyType.Mummy:
                portalText.text = "미라들의 땅";
                break;

            case EnemyType.Undead:
                portalText.text = "언데드들의 땅";
                break;
        }

        StartCoroutine(FadeInOut());
    }

    /// <summary>
    /// 화면 전환하는 코루틴
    /// </summary>
    IEnumerator FadeInOut()
    {
        isPortal = true;

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
        isPortal = false;

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
