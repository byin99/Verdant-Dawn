using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameUI : MonoBehaviour
{
    /// <summary>
    /// 레벨 업 하면 활성화 되는 틀
    /// </summary>
    CanvasGroup levelUpFrame;

    /// <summary>
    /// 맞으면 활성화 되는 틀
    /// </summary>
    CanvasGroup bloodFrame;

    /// <summary>
    /// Fighter클래스가 아이덴티티 스킬을 쓰면 활성화 되는 틀
    /// </summary>
    CanvasGroup fighterFrame;

    /// <summary>
    /// Berserker클래스가 아이덴티티 스킬을 쓰면 활성화 되는 틀
    /// </summary>
    CanvasGroup berserkerFrame;

    /// <summary>
    /// Hunter클래스가 아이덴티티 스킬을 쓰면 활성화 되는 틀
    /// </summary>
    CanvasGroup hunterFrame;

    /// <summary>
    /// Magician클래스가 아이덴티티 스킬을 쓰면 활성화 되는 틀
    /// </summary>
    CanvasGroup magicianFrame;

    /// <summary>
    /// Assassin클래스가 아이덴티티 스킬을 쓰면 활성화 되는 틀
    /// </summary>
    CanvasGroup assassinFrame;

    /// <summary>
    /// 블러드 코루틴 저장용
    /// </summary>
    IEnumerator bloodCoroutine;

    // 컴포넌트들
    PlayerStatus playerStatus;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        levelUpFrame = child.GetComponent<CanvasGroup>();

        child = transform.GetChild(1);
        bloodFrame = child.GetComponent<CanvasGroup>();

        child = transform.GetChild(2);
        fighterFrame = child.GetComponent<CanvasGroup>();

        child = transform.GetChild(3);
        berserkerFrame = child.GetComponent<CanvasGroup>();
        
        child = transform.GetChild(4);
        hunterFrame = child.GetComponent<CanvasGroup>();

        child = transform.GetChild(5);
        magicianFrame = child.GetComponent<CanvasGroup>();

        child = transform.GetChild(6);
        assassinFrame = child.GetComponent<CanvasGroup>();

        playerStatus = GameManager.Instance.PlayerStatus;
    }

    private void Start()
    {
        playerStatus.onLevelUp += ActivateLevelUp;
        playerStatus.onHit += ActivateBlood;
    }

    /// <summary>
    /// 레벨 업 틀을 활성화 시키는 함수
    /// </summary>
    void ActivateLevelUp()
    {
        StartCoroutine(OnLevelUpCoroutine());
    }

    /// <summary>
    /// Blood틀을 활성화 시키는 함수
    /// </summary>
    void ActivateBlood(float _, Vector3 __)
    {
        if (bloodCoroutine != null)
        {
            StopCoroutine(bloodCoroutine);
        }

        bloodCoroutine = OnBloodCoroutine();
        StartCoroutine(bloodCoroutine);
    }

    /// <summary>
    /// Fighter틀을 활성화 시키는 함수
    /// </summary>
    void ActivateFighter()
    {
        fighterFrame.alpha = 1.0f;
    }

    /// <summary>
    /// Berserker틀을 활성화 시키는 함수
    /// </summary>
    void ActivateBerserker()
    {
        berserkerFrame.alpha = 1.0f;
    }

    /// <summary>
    /// Hunter틀을 활성화 시키는 함수
    /// </summary>
    void ActivateHunter()
    {
        hunterFrame.alpha = 1.0f;
    }

    /// <summary>
    /// Magician틀을 활성화 시키는 함수
    /// </summary>
    void ActivateMagician()
    {
        magicianFrame.alpha = 1.0f;
    }

    /// <summary>
    /// Hunter틀을 활성화 시키는 함수
    /// </summary>
    void ActivateAssassin()
    {
        assassinFrame.alpha = 1.0f;
    }

    /// <summary>
    /// Fighter틀을 활성화 시키는 함수
    /// </summary>
    void DeactivateFighter()
    {
        fighterFrame.alpha = 0.0f;
    }

    /// <summary>
    /// Berserker틀을 활성화 시키는 함수
    /// </summary>
    void DeactivateBerserker()
    {
        berserkerFrame.alpha = 0.0f;
    }

    /// <summary>
    /// Hunter틀을 활성화 시키는 함수
    /// </summary>
    void DeactivateHunter()
    {
        hunterFrame.alpha = 0.0f;
    }

    /// <summary>
    /// Magician틀을 활성화 시키는 함수
    /// </summary>
    void DeactivateMagician()
    {
        magicianFrame.alpha = 0.0f;
    }

    /// <summary>
    /// Assassin틀을 활성화 시키는 함수
    /// </summary>
    void DeactivateAssassin()
    {
        assassinFrame.alpha = 0.0f;
    }

    /// <summary>
    /// 레벨업을 하면 실행되는 코루틴
    /// </summary>
    IEnumerator OnLevelUpCoroutine()
    {
        Factory.Instance.GetLevelUpEffect(playerStatus.transform.position, playerStatus.transform.rotation.eulerAngles);

        float timeElapsed = 0.0f;

        while (timeElapsed < 1.0f)
        {
            timeElapsed += Time.deltaTime;
            levelUpFrame.alpha = timeElapsed;
            yield return null;
        }

        levelUpFrame.alpha = 1.0f;

        yield return new WaitForSeconds(2.0f);

        while(timeElapsed > 0.0f)
        {
            timeElapsed -= Time.deltaTime;
            levelUpFrame.alpha = timeElapsed;
            yield return null;
        }

        levelUpFrame.alpha = 0.0f;
    }

    /// <summary>
    /// 피해를 입으면 실행되는 코루틴
    /// </summary>
    IEnumerator OnBloodCoroutine()
    {
        float timeElapsed = 0.0f;
        bloodFrame.alpha = 1.0f;

        yield return new WaitForSeconds(1.0f);

        while (timeElapsed > 0.0f)
        {
            timeElapsed -= Time.deltaTime;
            bloodFrame.alpha = timeElapsed;
            yield return null;
        }

        bloodFrame.alpha = 0.0f;
    }
}
