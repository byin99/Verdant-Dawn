using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner_MonsterZone : MonoBehaviour
{

    /// <summary>
    /// 이 몬스터의 종류
    /// </summary>
    public EnemyType enemyKind;

    /// <summary>
    /// 몬스터 소환 개수
    /// </summary>
    public int spawnCount = 50;

    /// <summary>
    /// 몬스터가 죽고 리스폰 되는 시간
    /// </summary>
    public float respawnTime = 10.0f;

    /// <summary>
    /// 스폰 범위
    /// </summary>
    public float spawnRange = 150.0f;

    private void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            RandomSpawn();
        }
    }

    /// <summary>
    /// 랜덤 지역에 몬스터를 스폰시키는 함수
    /// </summary>
    void RandomSpawn()
    {
        float randomNum1 = Random.Range(-spawnRange, spawnRange);
        float randomNum2 = Random.Range(-spawnRange, spawnRange);

        EnemyStatus status;

        switch (enemyKind)
        {
            case EnemyType.Ghoul:
                status = Factory.Instance.GetGhoul(new Vector3(transform.position.x + randomNum1, 0, transform.position.z + randomNum2)).GetComponent<EnemyStatus>();
                status.onDie += ReSpawn;
                break;

            case EnemyType.Skeleton:
                status = Factory.Instance.GetSkeleton(new Vector3(transform.position.x + randomNum1, 0, transform.position.z + randomNum2)).GetComponent<EnemyStatus>();
                status.onDie += ReSpawn;
                break;

            case EnemyType.Mummy:
                status = Factory.Instance.GetMummy(new Vector3(transform.position.x + randomNum1, 0, transform.position.z + randomNum2)).GetComponent<EnemyStatus>();
                status.onDie += ReSpawn;
                break;

            case EnemyType.Undead:
                status = Factory.Instance.GetUndead(new Vector3(transform.position.x + randomNum1, 0, transform.position.z + randomNum2)).GetComponent<EnemyStatus>();
                status.onDie += ReSpawn;
                break;
        }
    }

    /// <summary>
    /// 몬스터가 죽으면 리스폰 시키는 함수
    /// </summary>
    void ReSpawn()
    {
        StartCoroutine(ReSpawnCoroutine());
    }

    /// <summary>
    /// 리스폰하는 코루틴
    /// </summary>
    IEnumerator ReSpawnCoroutine()
    {
        yield return new WaitForSeconds(respawnTime);
        RandomSpawn();
    }
}
