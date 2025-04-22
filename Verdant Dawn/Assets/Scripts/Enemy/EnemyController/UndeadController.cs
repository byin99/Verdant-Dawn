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

        // Material 찾기 
        materials = new Material[17];
        SkinnedMeshRenderer[] skinnedMeshRenderers = new SkinnedMeshRenderer[17];

        skinnedMeshRenderers[0] = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        materials[0] = skinnedMeshRenderers[0].material;

        skinnedMeshRenderers[1] = transform.GetChild(2).GetComponent<SkinnedMeshRenderer>();
        materials[1] = skinnedMeshRenderers[1].material;

        skinnedMeshRenderers[2] = transform.GetChild(3).GetComponent<SkinnedMeshRenderer>();
        materials[2] = skinnedMeshRenderers[2].material;

        skinnedMeshRenderers[3] = transform.GetChild(4).GetComponent<SkinnedMeshRenderer>();
        materials[3] = skinnedMeshRenderers[3].material;

        skinnedMeshRenderers[4] = transform.GetChild(5).GetComponent<SkinnedMeshRenderer>();
        materials[4] = skinnedMeshRenderers[4].material;

        skinnedMeshRenderers[5] = transform.GetChild(6).GetComponent<SkinnedMeshRenderer>();
        materials[5] = skinnedMeshRenderers[5].material;

        skinnedMeshRenderers[6] = transform.GetChild(7).GetComponent<SkinnedMeshRenderer>();
        materials[6] = skinnedMeshRenderers[6].material;

        skinnedMeshRenderers[7] = transform.GetChild(8).GetComponent<SkinnedMeshRenderer>();
        materials[7] = skinnedMeshRenderers[7].material;

        skinnedMeshRenderers[8] = transform.GetChild(9).GetComponent<SkinnedMeshRenderer>();
        materials[8] = skinnedMeshRenderers[8].material;

        skinnedMeshRenderers[9] = transform.GetChild(10).GetComponent<SkinnedMeshRenderer>();
        materials[9] = skinnedMeshRenderers[9].material;

        skinnedMeshRenderers[10] = transform.GetChild(11).GetComponent<SkinnedMeshRenderer>();
        materials[10] = skinnedMeshRenderers[10].material;

        skinnedMeshRenderers[11] = transform.GetChild(12).GetComponent<SkinnedMeshRenderer>();
        materials[11] = skinnedMeshRenderers[11].material;

        skinnedMeshRenderers[12] = transform.GetChild(13).GetComponent<SkinnedMeshRenderer>();
        materials[12] = skinnedMeshRenderers[12].material;

        skinnedMeshRenderers[13] = transform.GetChild(14).GetComponent<SkinnedMeshRenderer>();
        materials[13] = skinnedMeshRenderers[13].material;

        skinnedMeshRenderers[14] = transform.GetChild(15).GetComponent<SkinnedMeshRenderer>();
        materials[14] = skinnedMeshRenderers[14].material;

        skinnedMeshRenderers[15] = transform.GetChild(16).GetComponent<SkinnedMeshRenderer>();
        materials[15] = skinnedMeshRenderers[15].material;

        skinnedMeshRenderers[16] = transform.GetChild(17).GetComponent<SkinnedMeshRenderer>();
        materials[16] = skinnedMeshRenderers[16].material;
    }

    /// <summary>
    /// UndeadAttack 소환 함수(Animation Clip용)
    /// </summary>
    protected override void AttackEffect1()
    {
        AudioSource.PlayClipAtPoint(audioManager[AudioCode.EnemySlash], attackTransform.position);
        Factory.Instance.GetUndeadAttack(attackTransform.position, attackTransform.rotation.eulerAngles);
    }
}
