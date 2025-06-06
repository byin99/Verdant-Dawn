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

        // Material 찾기 
        materials = new Material[1];
        SkinnedMeshRenderer[] skinnedMeshRenderers = new SkinnedMeshRenderer[1];

        skinnedMeshRenderers[0] = transform.GetChild(2).GetComponent<SkinnedMeshRenderer>();
        materials[0] = skinnedMeshRenderers[0].material;
    }

    /// <summary>
    /// SkeletonAttack 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect1()
    {
        AudioSource.PlayClipAtPoint(audioManager[AudioCode.EnemySlash], attackTransform.position);
        Factory.Instance.GetSkeletonAttack(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
