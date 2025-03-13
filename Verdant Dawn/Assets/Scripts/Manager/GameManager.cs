using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// Player
    /// </summary>
    Player player;

    /// <summary>
    /// PlayerMovement
    /// </summary>
    PlayerMovement playerMovement;

    /// <summary>
    /// PlayerInputController
    /// </summary>
    PlayerInputController playerInputController;

    /// <summary>
    /// PlayerClass
    /// </summary>
    PlayerClass playerClass;

    /// <summary>
    /// PlayerAttack
    /// </summary>
    PlayerAttack playerAttack;

    /// <summary>
    /// PlayerStatus
    /// </summary>
    PlayerStatus playerStatus;

    /// <summary>
    /// PlayerInventory
    /// </summary>
    PlayerInventory playerInventory;

    /// <summary>
    /// ItemDataManager
    /// </summary>
    ItemDataManager itemDataManager;

    /// <summary>
    /// Intensity 조절을 위한 컴포넌트
    /// </summary>
    [SerializeField]
    Volume volume;

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
    public PlayerMovement PlayerMovement
    {
        get
        {
            if (playerMovement == null)
            {
                playerMovement = FindAnyObjectByType<PlayerMovement>();
            }
            return playerMovement;
        }
    }

    /// <summary>
    /// PlayerInputController를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public PlayerInputController PlayerInputController
    {
        get
        {
            if (playerInputController == null)
            {
                playerInputController = FindAnyObjectByType<PlayerInputController>();
            }
            return playerInputController;
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

    /// <summary>
    /// PlayerAttack을 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public PlayerAttack PlayerAttack
    {
        get
        {
            if (playerAttack == null)
            {
                playerAttack = FindAnyObjectByType<PlayerAttack>();
            }
            return playerAttack;
        }
    }

    /// <summary>
    /// PlayerStatus를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public PlayerStatus PlayerStatus
    {
        get
        {
            if (playerStatus == null)
            {
                playerStatus = FindAnyObjectByType<PlayerStatus>();
            }
            return playerStatus;
        }
    }

    /// <summary>
    /// PlayerInventory를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public PlayerInventory PlayerInventory
    {
        get
        {
            if (playerInventory == null)
            {
                playerInventory = FindAnyObjectByType<PlayerInventory>();
            }
            return playerInventory;
        }
    }

    /// <summary>
    /// ItemDataManager를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public ItemDataManager ItemDataManager
    {
        get
        {
            if (itemDataManager == null)
            {
                itemDataManager = GetComponent<ItemDataManager>();
            }
            return itemDataManager;
        }
    }

    /// <summary>
    /// Volume를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public Volume Volume
    {
        get => volume;
    }

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        player = FindAnyObjectByType<Player>();                                 // Player 찾기
        playerMovement = FindAnyObjectByType<PlayerMovement>();                 // PlayerMovement 찾기
        playerInputController = FindAnyObjectByType<PlayerInputController>();   // PlayerInputController 찾기
        playerClass = FindAnyObjectByType<PlayerClass>();                       // PlayerClass 찾기
        playerAttack = FindAnyObjectByType<PlayerAttack>();                     // PlayerAttack 찾기
        playerStatus = FindAnyObjectByType<PlayerStatus>();                     // PlayerStatus 찾기
        playerInventory = FindAnyObjectByType<PlayerInventory>();               // PlayerInventory 찾기

        // 플레이어 초기화
        playerInventory?.Initialize();
    }
}
