using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IPortal
{
    /// <summary>
    /// 연결되어있는 맵의 정보
    /// </summary>
    public MapInfo connectMap;

    /// <summary>
    /// 포탈을 탈 때 이동할 Transform
    /// </summary>
    Transform portalPosition;

    private void Awake()
    {
        portalPosition = transform.GetChild(0);
    }

    /// <summary>
    /// 플레이어가 포탈에 들어왔을 때 실행하는 함수
    /// </summary>
    /// <returns>포탈 Transform</returns>
    public Transform StartMapEvent()
    {
        return portalPosition;
    }
}
