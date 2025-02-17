using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : EnemyController
{
    /// <summary>
    /// 미라 공격 트랜스폼
    /// </summary>
    Transform attackTransform;

    protected override void Awake()
    {
        base.Awake();

        // 미라 공격 트랜스폼 찾기
        attackTransform = transform.GetChild(2);

        // SkinnedMeshRenderer 찾기 
        skinnedMeshRenderers = new SkinnedMeshRenderer[1];
        skinnedMeshRenderers[0] = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
    }

    /// <summary>
    /// MummyAttack1 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect1()
    {
        Factory.Instance.GetMummyAttack1(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// MummyAttack2 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect2()
    {
        Factory.Instance.GetMummyAttack2(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
