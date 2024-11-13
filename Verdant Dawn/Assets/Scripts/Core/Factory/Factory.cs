using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    /// <summary>
    /// GhoulPool 선언
    /// </summary>
    GhoulPool ghoul;

    /// <summary>
    /// SkeletonPool 선언
    /// </summary>
    SkeletonPool skeleton;

    /// <summary>
    /// FistEffectPool 선언
    /// </summary>
    FistEffectPool fistEffect;

    /// <summary>
    /// GreatSwordEffectPool 선언
    /// </summary>
    GreatSwordEffectPool greatSwordEffect;

    /// <summary>
    /// RipleEffectPool 선언
    /// </summary>
    RipleEffectPool ripleEffect;

    /// <summary>
    /// StaffEffectPool 선언
    /// </summary>
    StaffEffectPool staffEffect;

    /// <summary>
    /// DaggerEffectPool 선언
    /// </summary>
    DaggerEffectPool daggerEffect;

    /// <summary>
    /// 초기화 함수
    /// </summary>
    protected override void OnInitialize()
    {
        // GhoulPool 초기화 및 생성
        ghoul = GetComponentInChildren<GhoulPool>();
        if (ghoul != null)
            ghoul.Initialize();

        // SkeletonPool 초기화 및 생성
        skeleton = GetComponentInChildren<SkeletonPool>();
        if (skeleton != null)
            skeleton.Initialize();

        // FistEffectPool 초기화 및 생성
        fistEffect = GetComponentInChildren<FistEffectPool>();
        if (fistEffect != null)
            fistEffect.Initialize();

        // GreatSwordEffectPool 초기화 및 생성
        greatSwordEffect = GetComponentInChildren<GreatSwordEffectPool>();
        if (greatSwordEffect != null)
            greatSwordEffect.Initialize();

        // RipleEffectPool 초기화 및 생성
        ripleEffect = GetComponentInChildren<RipleEffectPool>();
        if (ripleEffect != null)
            ripleEffect.Initialize();

        // StaffEffectPool 초기화 및 생성
        staffEffect = GetComponentInChildren<StaffEffectPool>();
        if (staffEffect != null)
            staffEffect.Initialize();

        // DaggerEffectPool 초기화 및 생성
        daggerEffect = GetComponentInChildren<DaggerEffectPool>();
        if (daggerEffect != null)
            daggerEffect.Initialize();
    }

    /// <summary>
    /// Ghoul 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 Ghoul</returns>
    public GhoulController GetGhoul(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return ghoul.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Skeleton 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 Skeleton</returns>
    public SkeletonController GetSkeleton(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return skeleton.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// FistEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 FistEffect</returns>
    public FistEffect GetFistEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return fistEffect.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// GreatSwordEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 GreatSwordEffect</returns>
    public GreatSwordEffect GetGreatSwordEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return greatSwordEffect.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// RipleEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 RipleEffect</returns>
    public RipleEffect GetRipleEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return ripleEffect.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// StaffEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 StaffEffect</returns>
    public StaffEffect GetStaffEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return staffEffect.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// DaggerEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 DaggerEffect</returns>
    public DaggerEffect GetDaggerEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return daggerEffect.GetObject(position, eulerAngle);
    }
}
