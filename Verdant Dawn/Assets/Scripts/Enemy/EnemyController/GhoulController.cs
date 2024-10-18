using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulController : EnemyController
{
    protected override void Awake()
    {
        base.Awake();

        // SkinnedMeshRenderer 찾기 
        skinnedMeshRenderers = new SkinnedMeshRenderer[3];
        skinnedMeshRenderers[0] = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[1] = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[2] = transform.GetChild(3).GetComponent<SkinnedMeshRenderer>();
    }
}
