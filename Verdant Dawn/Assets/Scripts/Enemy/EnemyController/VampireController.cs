using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireController : EnemyController
{
    /// <summary>
    /// 뱀파이어 공격 트랜스폼
    /// </summary>
    Transform attackTransform;

    protected override void Awake()
    {
        base.Awake();

        // 뱀파이어 공격 트랜스폼 찾기
        attackTransform = transform.GetChild(2);

        // Material 찾기 
        materials = new Material[1];
        SkinnedMeshRenderer[] skinnedMeshRenderers = new SkinnedMeshRenderer[1];

        skinnedMeshRenderers[0] = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        materials[0] = skinnedMeshRenderers[0].material;

    }

    /// <summary>
    /// VampireAttack1 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect1()
    {
        Factory.Instance.GetVampireAttack1(attackTransform.position, attackTransform.rotation.eulerAngles);
    }

    /// <summary>
    /// VampireAttack2 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect2()
    {
        Factory.Instance.GetVampireAttack2(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
