using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerPortal : MonoBehaviour
{
    /// <summary>
    /// 로컬 포탈을 타면 실행되는 델리게이트
    /// </summary>
    public event Action<MapInfo> onLocalPortal;

    /// <summary>
    /// 던전 포탈을 타면 실행되는 델리게이트
    /// </summary>
    public event Action<Portal> onDungeonPortal;

    /// <summary>
    /// 던전 포탈을 취소하거나 던전에서 나오면 실행되는 델리게이트
    /// </summary>
    public Action offDungeonPortal;

    /// <summary>
    /// 포탈을 타고 있는지 여부
    /// </summary>
    [HideInInspector]
    public bool isPortal;

    /// <summary>
    /// 부활 맵 정보
    /// </summary>
    public MapInfo revivalMapInfo;

    /// <summary>
    /// 부활 Transform
    /// </summary>
    public Transform revivalTransform;

    /// <summary>
    /// 현재 지역의 맵 정보
    /// </summary>
    MapInfo currentMap;

    /// <summary>
    /// 포탈 레이어의 번호
    /// </summary>
    int portalLayer;

    /// <summary>
    /// 현재 지역의 맵 정보를 반환하는 프로퍼티
    /// </summary>
    public MapInfo CurrentMap => currentMap;

    // 컴포넌트들
    NavMeshAgent agent;

    // 레이어 마스크
    int playerLayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        portalLayer = LayerMask.NameToLayer("Portal");
    }

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == portalLayer)
        {
            Portal portal = other.GetComponent<Portal>();
            if (portal != null)
            {
                switch (portal.portalType)
                {
                    case PortalEnum.Local:
                        GetPortal(portal);
                        break;

                    case PortalEnum.Dungeon:
                        onDungeonPortal?.Invoke(portal);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 포탈을 타면 실행되는 함수
    /// </summary>
    /// <param name="portal">포탈</param>
    public void GetPortal(Portal portal)
    {
        currentMap = portal.connectMap;
        onLocalPortal?.Invoke(currentMap);
        StartCoroutine(PortalCoroutine(portal));
    }

    /// <summary>
    /// 부활 함수
    /// </summary>
    public void Revive()
    {
        onLocalPortal?.Invoke(revivalMapInfo);
        StartCoroutine(RevivalCoroutine());
    }

    /// <summary>
    /// 던전 씬을 언로드 하는 함수
    /// </summary>
    void UnloadDungeonScene()
    {
        if (currentMap is DungeonMapInfo)
        {
            DungeonMapInfo bossMap = currentMap as DungeonMapInfo;
            SceneManager.UnloadSceneAsync(bossMap.dungeonSceneName);
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

        Transform warpTransform = portal.GetPortalPosition();
        agent.Warp(warpTransform.position);
        agent.transform.rotation = warpTransform.rotation;

        isPortal = false;
    }

    /// <summary>
    /// 부활 코루틴
    /// </summary>
    IEnumerator RevivalCoroutine()
    {
        isPortal = true;
        yield return new WaitForSeconds(2.0f);

        agent.Warp(revivalTransform.position);
        agent.transform.rotation = revivalTransform.rotation;
        UnloadDungeonScene();
        gameObject.layer = playerLayer;

        isPortal = false;
    }
}