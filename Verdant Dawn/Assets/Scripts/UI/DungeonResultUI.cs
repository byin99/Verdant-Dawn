using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonResultUI : MonoBehaviour
{
    /// <summary>
    /// 글자를 Fade In, Fade Out 하는 시간
    /// </summary>
    public float fadeTime = 1.0f;

    /// <summary>
    /// 클리어 텍스트
    /// </summary>
    TextMeshProUGUI clearText;

    // 컴포넌트들
    AudioManager audioManager;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        clearText = child.GetComponent<TextMeshProUGUI>();
        audioManager = GameManager.Instance.AudioManager;
    }

    /// <summary>
    /// 던전을 클리어하면 실행되는 함수
    /// </summary>
    public void ClearDungeon()
    {
        audioManager.PlaySound2D(AudioCode.DungeonClear);
        StartCoroutine(ClearDungeonCoroutine());
    }

    /// <summary>
    /// 클리어 코루틴
    /// </summary>
    IEnumerator ClearDungeonCoroutine()
    {
        float timeElapsed = 0.0f;
        while(timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            clearText.color = new Color(1, 1, 1, timeElapsed);
            yield return null;
        }
        clearText.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(fadeTime);

        while(timeElapsed > 0.0f)
        {
            timeElapsed -= Time.deltaTime;
            clearText.color = new Color(1, 1, 1, timeElapsed);
            yield return null;
        }
        clearText.color = new Color(1, 1, 1, 0);
    }
}
