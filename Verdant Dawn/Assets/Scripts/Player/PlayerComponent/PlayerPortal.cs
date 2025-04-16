using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPortal : MonoBehaviour
{
    /// <summary>
    /// 포탈을 타면 실행되는 델리게이트
    /// </summary>
    public Action<MapInfo> onPortal;

    /// <summary>
    /// 포탈을 타고 있는지 여부
    /// </summary>
    public bool isPortal;

    /// <summary>
    /// 현재 지역의 맵 정보
    /// </summary>
    MapInfo currentMap;

    /// <summary>
    /// 포탈 레이어의 번호
    /// </summary>
    int portalLayer;

    // 컴포넌트들
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        portalLayer = LayerMask.NameToLayer("Portal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == portalLayer)
        {
            Portal portal = other.GetComponent<Portal>();
            if (portal != null)
            {
                currentMap = portal.connectMap;
                onPortal?.Invoke(currentMap);
                StartCoroutine(PortalCoroutine(portal));
            }
        }
    }

    /// <summary>
    /// 포탈 코루틴
    /// </summary>
    /// <param name="portal">타는 포탈</param>
    IEnumerator PortalCoroutine(Portal portal)
    {
        isPortal = true;

        yield return new WaitForSeconds(2.0f);

        Transform warpTransform = portal.StartMapEvent();
        agent.Warp(warpTransform.position);
        agent.transform.rotation = warpTransform.rotation;
        isPortal = false;
    }
}