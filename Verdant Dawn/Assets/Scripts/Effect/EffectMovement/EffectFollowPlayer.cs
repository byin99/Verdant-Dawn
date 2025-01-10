using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFollowPlayer : MonoBehaviour
{
    /// <summary>
    /// 공격 트랜스폼
    /// </summary>
    Transform attackTransform;

    private void Awake()
    {
        Player player = GameManager.Instance.Player;
        attackTransform = player.transform.GetChild(2);
    }

    private void Update()
    {
        // 플레이어 attackTransform에서 움직이게 만들기
        transform.position = attackTransform.position;
        transform.rotation = attackTransform.rotation;
    }
}
