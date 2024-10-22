using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PointerVFX : MonoBehaviour
{
    /// <summary>
    /// VFX 컴포넌트
    /// </summary>
    VisualEffect effect;

    /// <summary>
    /// 플레이어
    /// </summary>
    PlayerMovement player;

    // VFX ID들
    readonly int OnStartEventID = Shader.PropertyToID("OnStart");
    readonly int OnEndEventID = Shader.PropertyToID("OnEnd");

    private void Awake()
    {
        effect = GetComponent<VisualEffect>();
        player = GameManager.Instance.Movement;
    }

    private void Start()
    {
        player.onDirection += VFXStart; 
        player.onArrive += VFXEnd;
    }

    /// <summary>
    /// 플레이어가 목표로 하는 지점에 VFX설치
    /// </summary>
    /// <param name="spawnPosition">목표로 하는 지점</param>
    private void VFXStart(Vector3 spawnPosition)
    {
        effect.Reinit();                    // 전에 보이던 VFX 초기화
        transform.position = spawnPosition; // position 재설정
        effect.SendEvent(OnStartEventID);   // VFX 시작
    }

    private void VFXEnd()
    {
        effect.SendEvent(OnEndEventID);     // VFX 종료
    }
}
