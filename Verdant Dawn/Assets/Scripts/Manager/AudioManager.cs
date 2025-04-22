using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// AudioClip들
    /// </summary>
    public AudioClip[] audioClips;

    /// <summary>
    /// AudioClip를 가져오기 위한 인덱서
    /// </summary>
    /// <param name="code">오디오 코드</param>
    /// <returns>오디오 코드가 맞는 AudioClip</returns>
    public AudioClip this[AudioCode code] => audioClips[(int)code];

    /// <summary>
    /// AudioClip를 가져오기 위한 인덱서
    /// </summary>
    /// <param name="index">인덱스</param>
    /// <returns>인덱스와 맞는 AudioClip</returns>
    public AudioClip this[uint index] => audioClips[index];

    /// <summary>
    /// 2D 사운드로 재생하기
    /// </summary>
    /// <param name="code">재생할 AudioCode</param>
    /// <param name="volume">소리 볼륨</param>
    public void PlaySound2D(AudioCode code, float volume = 0.1f)
    {
        GameObject obj = new GameObject("2DSound");
        AudioSource audioSource = obj.AddComponent<AudioSource>();
        audioSource.clip = audioClips[(int)code];
        audioSource.volume = volume;
        audioSource.spatialBlend = 0f;
        audioSource.Play();
        Object.Destroy(obj, audioSource.clip.length);
    }
}

/// <summary>
/// 오디오 코드
/// </summary>
public enum AudioCode : byte
{
    Click = 0,
    Interaction,
    ItemDrop,
    ItemPick,
    ItemGet,
    ItemUse,
    FootStep,
    Roll,
    LevelUp,
    Portal,
    GhoulDie,
    SkeletonDie,
    MummyDie,
    UndeadDie,
    VampireDie,
    EvilWatcherDie,
    DemonLoadDie,
    PlayerDie,
    BaseAttack_F,
    BaseAttack_B,
    BaseAttack_H,
    BaseAttack_M,
    BaseAttack_A,
    WSkill_F_1,
    WSkill_F_2,
    WSkill_B_1,
    WSkill_B_2,
    WSkill_H_1,
    WSkill_H_2,
    WSkill_M_1,
    WSkill_M_2,
    WSkill_A_1,
    WSkill_A_2,
    ESkill_F_1,
    ESkill_F_2,
    ESkill_F_3,
    ESkill_B_1,
    ESkill_B_2,
    ESkill_H_1,
    ESkill_H_2,
    ESkill_M_1,
    ESkill_M_2,
    ESkill_A_1,
    ESkill_A_2,
    ESkill_A_3,
    RSkill_F_1,
    RSkill_F_2,
    RSkill_B_1,
    RSkill_H_1,
    RSkill_M_1,
    RSkill_M_2,
    RSkill_A_1,
    RSkill_A_2,
    EnemyScratch,
    EnemySlash,
    EnemyBite,
    BossRoar,
    BossSlash,
    BossWhip,
    BossEvocation,
    BossDown,
    BossStep,
    EnemyToPlayer,
    PlayerToEnemy,
    DungeonClear,
}
