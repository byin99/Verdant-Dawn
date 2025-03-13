using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEffect : RecycleObject
{
    /// <summary>
    /// 이펙트 없애는 시간
    /// </summary>
    public float disableTime = 1.5f;

    /// <summary>
    /// 플레이어 Transform
    /// </summary>
    Transform playerTransform;

    protected override void OnReset()
    {
        base.OnReset();
        DisableTimer(disableTime);

        playerTransform = GameManager.Instance.Player.transform;
    }

    private void Update()
    {
        transform.position = playerTransform.position;
        transform.rotation = playerTransform.rotation;
    }
}
