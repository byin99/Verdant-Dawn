using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentityUI : MonoBehaviour
{
    /// <summary>
    /// 아이덴티티 게이지 슬라이더
    /// </summary>
    Slider identityGauge;

    // 컴포넌트들
    PlayerStatus playerStatus;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        identityGauge = child.GetComponent<Slider>();

        playerStatus = GameManager.Instance.PlayerStatus;
        playerStatus.onChangeIdentityGauge += ChangeGauge;
    }

    /// <summary>
    /// 아이덴티티 게이지 바꾸기
    /// </summary>
    /// <param name="gauge">바꾸는 Gauge양</param>
    void ChangeGauge(float gauge)
    {
        identityGauge.value = gauge;
    }
}
