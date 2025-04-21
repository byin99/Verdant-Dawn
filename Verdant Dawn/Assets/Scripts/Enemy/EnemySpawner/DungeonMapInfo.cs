using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapInfo : MapInfo
{
    /// <summary>
    /// 보스 이미지
    /// </summary>
    public Material bossImage;

    /// <summary>
    /// 보스 이름
    /// </summary>
    public string bossName;

    /// <summary>
    /// 보스 던전씬 이름
    /// </summary>
    public string dungeonSceneName;

    /// <summary>
    /// 던전에서 나가는 포탈
    /// </summary>
    [HideInInspector]
    public GameObject outPortal;

    private void Awake()
    {
        outPortal = transform.GetChild(2).gameObject;
    }

    private void Start()
    {
        outPortal.SetActive(false);
    }
}
