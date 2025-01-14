using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : EnemyController
{
    /// <summary>
    /// 스켈레톤 공격 트랜스폼
    /// </summary>
    Transform attackTransform;

    protected override void Awake()
    {
        base.Awake();

        // 스켈레톤 공격 트랜스폼 찾기
        attackTransform = transform.GetChild(3);

        // SkinnedMeshRenderer 찾기 
        skinnedMeshRenderers = new SkinnedMeshRenderer[2];
        skinnedMeshRenderers[0] = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[1] = transform.GetChild(2).GetComponent<SkinnedMeshRenderer>();
    }

    /// <summary>
    /// SkeletonAttackEffect1 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect1()
    {
        Factory.Instance.GetSkeletonAttack1Effect(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// SkeletonAttackEffect2 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect2()
    {
        Factory.Instance.GetSkeletonAttack2Effect(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
