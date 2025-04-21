using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Map : MonoBehaviour
{
    /// <summary>
    /// 이 스포너의 적 타입
    /// </summary>
    public EnemyType enemyType;

    /// <summary>
    /// 스포너가 존재하는 맵의 정보
    /// </summary>
    public MapInfo mapInfo;

    /// <summary>
    /// 스폰되는 간격
    /// </summary>
    public float spawnCoolTime = 10.0f;

    /// <summary>
    /// 스포너에서 최대로 유지 가능한 적의 수
    /// </summary>
    public int capacity = 20;

    /// <summary>
    /// 스폰되는 시간을 재기 위한 변수
    /// </summary>
    float timeElapsed = 0.0f;

    /// <summary>
    /// 현재 적의 수
    /// </summary>
    int enemyCount = 0;

    private void Start()
    {
        for(int i = 0; i < capacity; i++)
        {
            Spawn();
        }
    }

    private void Update()
    {
        if (enemyCount < capacity)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > spawnCoolTime)
            {
                Spawn();
                timeElapsed = 0.0f;
            }
        }
    }

    /// <summary>
    /// 적은 스폰하는 함수
    /// </summary>
    void Spawn()
    {
        float ranX = Random.Range(mapInfo.mapOrigin.x - mapInfo.mapSizeX * 0.5f, mapInfo.mapOrigin.x + mapInfo.mapSizeX * 0.5f);
        float ranZ = Random.Range(mapInfo.mapOrigin.z - mapInfo.mapSizeZ * 0.5f, mapInfo.mapOrigin.z + mapInfo.mapSizeZ * 0.5f);

        EnemyController enemy = null;

        switch (enemyType)
        {
            case EnemyType.Ghoul:
                enemy = Factory.Instance.GetGhoul(new Vector3(ranX, 0, ranZ));
                enemy.transform.SetParent(transform);
                break;
            case EnemyType.Skeleton:
                enemy = Factory.Instance.GetSkeleton(new Vector3(ranX, 0, ranZ));
                enemy.transform.SetParent(transform);
                break;
            case EnemyType.Mummy:
                enemy = Factory.Instance.GetMummy(new Vector3(ranX, 0, ranZ));
                enemy.transform.SetParent(transform);
                break;
            case EnemyType.Undead:
                enemy = Factory.Instance.GetUndead(new Vector3(ranX, 0, ranZ));
                enemy.transform.SetParent(transform);
                break;
        }
        enemyCount++;

        EnemyStatus status = enemy.GetComponent<EnemyStatus>();
        status.onDie += () =>
        {
            enemy.ReturnToPool();
            enemyCount--;
        };
    }
}
