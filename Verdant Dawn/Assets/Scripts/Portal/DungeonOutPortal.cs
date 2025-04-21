using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonOutPortal : DungeonPortal
{
    /// <summary>
    /// 던전을 나갈 때 생성할 임시 바닥
    /// </summary>
    TemporaryGround temporaryGround;

    protected override void Awake()
    {
        base.Awake();
        temporaryGround = GameManager.Instance.TemporaryGround;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(UnloadSceneCoroutine());
        }
    }

    /// <summary>
    /// 던전 씬을 언로드하는 코루틴
    /// </summary>
    IEnumerator UnloadSceneCoroutine()
    {
        temporaryGround.MakeTemporaryGround();
        yield return new WaitForSeconds(3.0f);
        SceneManager.UnloadSceneAsync(dungeonSceneName);
        temporaryGround.RemoveTemporaryGround();
    }
}
