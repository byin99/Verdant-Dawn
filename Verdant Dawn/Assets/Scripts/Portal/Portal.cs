using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IPortal
{
    /// <summary>
    /// 이 포탈의 종류
    /// </summary>
    public PortalEnum portalType;

    /// <summary>
    /// 연결되어있는 맵의 정보
    /// </summary>
    public MapInfo connectMap;

    /// <summary>
    /// 포탈을 탈 때 이동할 Transform
    /// </summary>
    Transform portalPosition;

    protected virtual void Awake()
    {
        portalPosition = transform.GetChild(0);
    }

    /// <summary>
    /// 포탈 포지션을 가져오는 함수
    /// </summary>
    /// <returns>포탈 Transform</returns>
    public Transform GetPortalPosition()
    {
        return portalPosition;
    }
}
