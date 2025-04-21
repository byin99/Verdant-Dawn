using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemySpawner_Boss : MonoBehaviour
{
    /// <summary>
    /// 클리어 시 바꿀 스카이박스
    /// </summary>
    public Material clearSkyboxMaterial;

    /// <summary>
    /// 스폰하는 보스 타입
    /// </summary>
    public BossType bossType;

    /// <summary>
    /// 보스 맵 정보
    /// </summary>
    public DungeonMapInfo bossMapInfo;

    /// <summary>
    /// 보스가 패턴을 사용할 때의 Transform
    /// </summary>
    Transform bossPatternTransform;

    /// <summary>
    /// 보스 Appearance용 카메라
    /// </summary>
    CinemachineVirtualCamera virtualCamera;

    // 컴포넌트들
    Collider spawnCollider;
    Player player;
    PlayerQuest playerQuest;
    NPC npc;
    BossHPUI bossHPUI;
    DungeonResultUI dungeonResultUI;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        playerQuest = GameManager.Instance.PlayerQuest;
        npc = GameManager.Instance.NPC;
        spawnCollider = GetComponent<Collider>();
        bossHPUI = UIManager.Instance.BossHPUI;
        dungeonResultUI = UIManager.Instance.DungeonResultUI;
        virtualCamera = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        bossPatternTransform = transform.GetChild(1);
    }

    private void OnEnable()
    {
        player.onRevive += ClearBossMonster;
    }

    private void OnDisable()
    {
        player.onRevive -= ClearBossMonster;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                spawnCollider.enabled = false;
                Spawn();
            }
        }
    }

    /// <summary>
    /// 보스를 스폰하는 함수
    /// </summary>
    void Spawn()
    {
        BossController boss;
        switch (bossType)
        {
            case BossType.DemonLord:
                boss = Factory.Instance.GetDemonLord(transform.position, transform.rotation.eulerAngles);
                virtualCamera.Follow = boss.transform;
                virtualCamera.LookAt = boss.transform;
                virtualCamera.Priority = 20;
                boss.virtualCamera = virtualCamera;
                boss.SetBossPatternTransform(bossPatternTransform);
                boss.transform.SetParent(transform);

                boss.onDie += () =>
                {
                    boss.ReturnToPool();
                    if (playerQuest.CurrentQuest is QuestData_Dungeon)
                    {
                        QuestData_Dungeon dungeon = playerQuest.CurrentQuest as QuestData_Dungeon;
                        if (dungeon.dungeonType == DungeonType.DevilDungeon)
                        {
                            npc.QuestCanCompleted();
                        }
                    }
                    dungeonResultUI.ClearDungeon();
                    RenderSettings.skybox = clearSkyboxMaterial;
                    bossMapInfo.outPortal.SetActive(true);
                };

                BossStatus status = boss.GetComponent<BossStatus>();
                bossHPUI.bossStatus = status;
                bossHPUI.ShowBossHPBarUI();
                break;
        }
    }

    /// <summary>
    /// 플레이어가 죽었을 때 보스 몬스터를 없애는 함수
    /// </summary>
    void ClearBossMonster()
    {
        BossController boss = transform.GetComponentInChildren<BossController>();
        boss.ReturnToPool();
    }
}
