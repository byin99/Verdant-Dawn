using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyZoneEffect : RecycleObject
{
    /// <summary>
    /// 무적 레이어의 번호
    /// </summary>
    int InvincibleLayer;

    /// <summary>
    /// 플레이어 레이어의 번호
    /// </summary>
    int PlayerLayer;

    private void Awake()
    {
        InvincibleLayer = LayerMask.NameToLayer("Invincible");
        PlayerLayer = LayerMask.NameToLayer("Player");
    }

    /// <summary>
    /// 플레이어가 안전지대에 들어왔을 때 레이어를 바꾸는 함수
    /// </summary>
    /// <param name="other">트리거가 발생한 Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PlayerLayer)
        {
            other.gameObject.layer = InvincibleLayer;
        }
    }

    /// <summary>
    /// 플레이어가 안전지대에서 나갔을 때 레이어를 바꾸는 함수
    /// </summary>
    /// <param name="other">트리거가 끝난 Collider</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == InvincibleLayer)
        {
            other.gameObject.layer = PlayerLayer;
        }
    }
}
