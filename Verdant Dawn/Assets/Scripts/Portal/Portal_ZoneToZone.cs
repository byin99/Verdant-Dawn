using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Portal_ZoneToZone : MonoBehaviour, IPortal
{
    /// <summary>
    /// 이 포탈이 속한 존의 EnemyType
    /// </summary>
    public EnemyType enemyType;

    /// <summary>
    /// 이 포탈과 연결된 포탈
    /// </summary>
    public Portal_ZoneToZone portal;

    /// <summary>
    /// 포탈의 Transform
    /// </summary>
    Transform portalTransform;

    private void Awake()
    {
        portalTransform = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            agent.ResetPath();
            agent.enabled = false;
            UIManager.Instance.PortalUI.MoveToZone(portal.enemyType);
            StartCoroutine(MoveToZone(player));
        }
    }

    /// <summary>
    /// 포탈을 탈 때 호출되는 함수
    /// </summary>
    /// <param name="player">포탈을 타는 Player</param>
    public void TakePortal(Player player)
    {
        player.transform.position = portal.portalTransform.position;
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    /// <summary>
    /// 존끼리 이동할 때 호출되는 코루틴
    /// </summary>
    /// <param name="player">이동하는 Player</param>
    IEnumerator MoveToZone(Player player)
    {
        yield return new WaitForSeconds(1.5f);
        TakePortal(player);
    }
}
