using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 플레이어 선언
    /// </summary>
    Player player;

    /// <summary>
    /// 플레이어를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public Player Player
    {
        get
        {
            if (player == null)
            {
                player = FindAnyObjectByType<Player>();
            }
            return player;
        }
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>(); // 플레이어 찾기
    }
}
