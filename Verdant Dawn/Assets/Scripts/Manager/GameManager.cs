using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// Player
    /// </summary>
    Player player;

    /// <summary>
    /// PlayerMovement
    /// </summary>
    PlayerMovement movement;

    /// <summary>
    /// PlayerClass
    /// </summary>
    PlayerClass playerClass;

    /// <summary>
    /// Player를 공유받을 프로퍼티(읽기 전용)
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

    /// <summary>
    /// PlayerMovement를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public PlayerMovement Movement
    {
        get
        {
            if (movement == null)
            {
                movement = FindAnyObjectByType<PlayerMovement>();
            }
            return movement;
        }
    }

    /// <summary>
    /// PlayerClass를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public PlayerClass PlayerClass
    {
        get
        {
            if (playerClass == null)
            {
                playerClass = FindAnyObjectByType<PlayerClass>();
            }
            return playerClass;
        }
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();             // Player 찾기
        movement = FindAnyObjectByType<PlayerMovement>();   // PlayerMovement 찾기
        playerClass = FindAnyObjectByType<PlayerClass>();   // PlayerClass 찾기
    }
}
