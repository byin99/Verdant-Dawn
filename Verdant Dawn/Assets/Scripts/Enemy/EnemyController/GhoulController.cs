using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulController : EnemyController
{
    /// <summary>
    /// 구울 공격 트랜스폼
    /// </summary>
    Transform attackTransform;

    protected override void Awake()
    {
        base.Awake();

        // 구울 공격 트랜스폼 찾기
        attackTransform = transform.GetChild(4);

        // SkinnedMeshRenderer 찾기 
        skinnedMeshRenderers = new SkinnedMeshRenderer[3];
        skinnedMeshRenderers[0] = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[1] = transform.GetChild(2).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[2] = transform.GetChild(3).GetComponent<SkinnedMeshRenderer>();
    }

    /// <summary>
    /// GhoulAttack 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect1()
    {
        Factory.Instance.GetGhoulAttack(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
