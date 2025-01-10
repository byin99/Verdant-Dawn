using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEffect : RecycleObject
{
    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    protected override void OnReset()
    {
        DisableTimer(3.0f);
    }

    private void Update()
    {
        transform.position = player.transform.position;
    }
}
