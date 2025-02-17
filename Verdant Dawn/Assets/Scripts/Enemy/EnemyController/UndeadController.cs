using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadController : EnemyController
{

    /// <summary>
    /// 언데드 공격 트랜스폼
    /// </summary>
    Transform attackTransform;

    protected override void Awake()
    {
        base.Awake();

        // 언데드 공격 트랜스폼 찾기
        attackTransform = transform.GetChild(18);

        // SkinnedMeshRenderer 찾기 
        skinnedMeshRenderers = new SkinnedMeshRenderer[17];
        skinnedMeshRenderers[0] = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[1] = transform.GetChild(2).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[2] = transform.GetChild(3).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[3] = transform.GetChild(4).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[4] = transform.GetChild(5).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[5] = transform.GetChild(6).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[6] = transform.GetChild(7).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[7] = transform.GetChild(8).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[8] = transform.GetChild(9).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[9] = transform.GetChild(10).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[10] = transform.GetChild(11).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[11] = transform.GetChild(12).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[12] = transform.GetChild(13).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[13] = transform.GetChild(14).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[14] = transform.GetChild(15).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[15] = transform.GetChild(16).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[16] = transform.GetChild(17).GetComponent<SkinnedMeshRenderer>();
    }

    /// <summary>
    /// UndeadAttack 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect1()
    {
        Factory.Instance.GetUndeadAttack(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
