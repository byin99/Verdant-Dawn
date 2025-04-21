using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnemySpawner_Dungeon : MonoBehaviour
{
    /// <summary>
    /// 특정 상황에서 플레이어의 이동을 막기 위한 NavMesh컴포넌트
    /// </summary>
    public NavMeshSurface surface;
    
    /// <summary>
    /// 적 타입
    /// </summary>
    public EnemyType enemyType = EnemyType.Ghoul;

    /// <summary>
    /// 적이 스폰되는 사이즈
    /// </summary>
    public float spawnRadius = 5.0f;

    /// <summary>
    /// 적 마리수
    /// </summary>
    public int enemyCount = 5;

    /// <summary>
    /// 적이 죽은 마리수
    /// </summary>
    int deathCount = 0;

    /// <summary>
    /// 적을 다 죽였는지 알려주는 프로퍼티(읽기 전용)
    /// </summary>
    public bool IsClear => enemyCount == deathCount;

    /// <summary>
    /// 스폰 콜라이더(충돌 처리가 일어나면 적 스폰, 1회만)
    /// </summary>
    Collider spawnCollider;

    /// <summary>
    /// 던전 바리케이드 오브젝트
    /// </summary>
    GameObject dungeonWallObject;

    // 컴포넌트들
    Player player;

    private void Awake()
    {
        spawnCollider = GetComponent<Collider>();
        player = GameManager.Instance.Player;
        dungeonWallObject = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        player.onRevive += ClearAllMonsters;
    }

    private void OnDisable()
    {
        player.onRevive -= ClearAllMonsters;
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
                EnableMovementBlockade();
            }
        }
    }

    /// <summary>
    /// 적을 스폰하는 함수
    /// </summary>
    void Spawn()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            EnemyController enemy = null;

            switch (enemyType)
            {
                case EnemyType.Vampire:
                    enemy = Factory.Instance.GetVampire(GetSpawnRandomPosition());
                    enemy.transform.SetParent(transform);
                    EnemyStatus status = enemy.GetComponent<EnemyStatus>();
                    status.onDie += () =>
                    {
                        deathCount++;
                        enemy.ReturnToPool();
                        if (IsClear)
                        {
                            DisableMovementBlockade();
                        }
                    };
                    break;
            }
        }
    }

    /// <summary>
    /// 랜덤으로 스폰 위치를 정하는 함수
    /// </summary>
    /// <returns>랜덤 위치</returns>
    Vector3 GetSpawnRandomPosition()
    {
        Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;
        return new Vector3(spawnPosition.x, 0, spawnPosition.y) + transform.position;
    }

    /// <summary>
    /// 모든 적을 클리어하는 함수
    /// </summary>
    void ClearAllMonsters()
    {
        EnemyController[] enemies = GetComponentsInChildren<EnemyController>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].ReturnToPool();
        }

        
    }

    /// <summary>
    /// 바리케이드를 치는 함수
    /// </summary>
    void EnableMovementBlockade()
    {
        dungeonWallObject.SetActive(true);
        surface.BuildNavMesh();
    }
        
    /// <summary>
    /// 바리케이드를 없애는 함수
    /// </summary>
    void DisableMovementBlockade()
    {
        dungeonWallObject.SetActive(false);
        surface.BuildNavMesh();
    }
}
