using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// SkillBarUI
    /// </summary>
    SkillBarUI skillBarUI;

    /// <summary>
    /// BossHPUI
    /// </summary>
    BossHPUI bossHPUI;

    /// <summary>
    /// BossEvocationUI
    /// </summary>
    BossEvocationUI bossEvocationUI;

    /// <summary>
    /// InventoryUI
    /// </summary>
    InventoryUI inventoryUI;

    /// <summary>
    /// DungeonResultUI
    /// </summary>
    DungeonResultUI dungeonResultUI;

    /// <summary>
    /// SKillBarUI를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public SkillBarUI SkillBarUI
    {
        get
        {
            if (skillBarUI == null)
            {
                skillBarUI = FindAnyObjectByType<SkillBarUI>();
            }
            return skillBarUI;
        }
    }

    /// <summary>
    /// BossHPUI를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public BossHPUI BossHPUI
    {
        get
        {
            if (bossHPUI == null)
            {
                bossHPUI = FindAnyObjectByType<BossHPUI>();
            }
            return bossHPUI;
        }
    }

    /// <summary>
    /// BossEvocationUI를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public BossEvocationUI BossEvocationUI
    {
        get
        {
            if (bossEvocationUI == null)
            {
                bossEvocationUI = FindAnyObjectByType<BossEvocationUI>();
            }
            return bossEvocationUI;
        }
    }

    /// <summary>
    /// InventoryUI를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public InventoryUI InventoryUI
    {
        get
        {
            if (inventoryUI == null)
            {
                inventoryUI = FindAnyObjectByType<InventoryUI>();
            }
            return inventoryUI;
        }
    }

    /// <summary>
    /// DungeonResultUI를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public DungeonResultUI DungeonResultUI
    {
        get
        {
            if (dungeonResultUI == null)
            {
                dungeonResultUI = FindAnyObjectByType<DungeonResultUI>();
            }
            return dungeonResultUI;
        }
    }

    protected override void OnInitialize()
    {
        skillBarUI = FindAnyObjectByType<SkillBarUI>();             // SkillBarUI 찾기
        bossHPUI = FindAnyObjectByType<BossHPUI>();                 // BossHPUI 찾기
        inventoryUI = FindAnyObjectByType<InventoryUI>();           // inventoryUI 찾기
        dungeonResultUI = FindAnyObjectByType<DungeonResultUI>();   // DungeonResultUI 찾기
    }
}
