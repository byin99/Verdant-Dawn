using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    /// <summary>
    /// GhoulPool 선언
    /// </summary>
    GhoulPool ghoul;

    /// <summary>
    /// 초기화 함수
    /// </summary>
    protected override void OnInitialize()
    {
        // GhoulPool 초기화 및 생성
        ghoul = GetComponentInChildren<GhoulPool>();
        if (ghoul != null)
            ghoul.Initialize();
    }

    /// <summary>
    /// Ghoul 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns></returns>
    public EnemyController GetGhoul(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return ghoul.GetObject(position, eulerAngle);
    }
}
