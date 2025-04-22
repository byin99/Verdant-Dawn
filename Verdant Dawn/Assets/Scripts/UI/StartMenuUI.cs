using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    /// <summary>
    /// 클릭 사운드
    /// </summary>
    public AudioClip clickSound;

    /// <summary>
    /// 화면 전환 시 Fade In, Fade Out 하는 시간
    /// </summary>
    public float fadeTime = 1.0f;

    /// <summary>
    /// 게임 시작 버튼
    /// </summary>
    Button startButton;

    /// <summary>
    /// 조작키 설명 버튼
    /// </summary>
    Button keysButton;

    /// <summary>
    /// 로딩할 때 사용할 패널
    /// </summary>
    Image loadingPanel;

    /// <summary>
    /// 조작키 설명 나가기 버튼
    /// </summary>
    Button descriptionExitButton;

    /// <summary>
    /// 조작키 설명 캔버스 그룹
    /// </summary>
    CanvasGroup canvasGroup;

    /// <summary>
    /// 로딩할 씬 이름
    /// </summary>
    const string mainSceneName = "MainScene";

    private void Awake()
    {
        startButton = transform.GetChild(2).GetComponent<Button>();
        keysButton = transform.GetChild(3).GetComponent<Button>();
        canvasGroup = transform.GetChild(4).GetComponent<CanvasGroup>();
        descriptionExitButton = transform.GetChild(4).GetChild(3).GetComponent<Button>();
        loadingPanel = transform.GetChild(5).GetComponent<Image>();
    }

    private void Start()
    {
        startButton.onClick.AddListener(() => {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
            ChangeScene();
        });
        keysButton.onClick.AddListener(() => {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
            ShowOperationKeyDecription();
        });
        descriptionExitButton.onClick.AddListener(() => {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
            ExitOperationKeyDecription();
        });

        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 씬을 바꾸는 함수(Start 버튼 클릭 시 실행됨)
    /// </summary>
    void ChangeScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    /// <summary>
    /// 조작키 설명 UI를 보여주는 함수
    /// </summary>
    void ShowOperationKeyDecription() 
    {
        StartCoroutine(ShowOperationKeyCoroutine());
    }

    /// <summary>
    /// 조작키 설명 UI를 끄는 함수
    /// </summary>
    void ExitOperationKeyDecription()
    {
        StartCoroutine(ExitOperationKeyCoroutine());
    }
    
    /// <summary>
    /// 씬을 바꾸는 코루틴
    /// </summary>
    IEnumerator LoadSceneCoroutine()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            loadingPanel.color = new Color(0, 0, 0, timeElapsed);
            yield return null;
        }

        loadingPanel.color = new Color(0, 0, 0, 1);

        SceneManager.LoadScene(mainSceneName);
    }

    /// <summary>
    /// 조작키 설명 UI를 보여주는 코루틴
    /// </summary>
    IEnumerator ShowOperationKeyCoroutine()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = timeElapsed;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 조작키 설명 UI를 끄는 코루틴
    /// </summary>
    IEnumerator ExitOperationKeyCoroutine()
    {
        float timeElapsed = fadeTime;

        while (timeElapsed > 0.0f)
        {
            timeElapsed -= Time.deltaTime;
            canvasGroup.alpha = timeElapsed;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
