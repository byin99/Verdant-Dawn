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
    /// MummyPool 선언
    /// </summary>
    MummyPool mummy;

    /// <summary>
    /// UndeadPool 선언
    /// </summary>
    UndeadPool undead;

    /// <summary>
    /// VampirePool 선언
    /// </summary>
    VampirePool vampire;

    /// <summary>
    /// DemonLordPool 선언
    /// </summary>
    DemonLordPool demonLord;

    /// <summary>
    /// GhoulAttackPool 선언
    /// </summary>
    GhoulAttackPool ghoulAttack;

    /// <summary>
    /// SkeletonAttackPool 선언
    /// </summary>
    SkeletonAttackPool skeletonAttack;

    /// <summary>
    /// MummyAttack1Pool 선언
    /// </summary>
    MummyAttack1Pool mummyAttack1;

    /// <summary>
    /// MummyAttack2Pool 선언
    /// </summary>
    MummyAttack2Pool mummyAttack2;

    /// <summary>
    /// UndeadAttackPool 선언
    /// </summary>
    UndeadAttackPool undeadAttack;

    /// <summary>
    /// VampireAttack1Pool 선언
    /// </summary>
    VampireAttack1Pool vampireAttack1;

    /// <summary>
    /// VampireAttack2Pool 선언
    /// </summary>
    VampireAttack2Pool vampireAttack2;

    /// <summary>
    /// BossRoarPool 선언
    /// </summary>
    BossRoarPool bossRoar;

    /// <summary>
    /// BossAttack1Pool 선언
    /// </summary>
    BossAttack1Pool bossAttack1;

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
    /// F_W_SkillEffectPool_Prepare 선언
    /// </summary>
    F_W_SkillEffectPool_Prepare f_W_Skill_Prepare;

    /// <summary>
    /// F_W_SkillEffectPool_Success 선언
    /// </summary>
    F_W_SkillEffectPool_Success f_W_Skill_Success;

    /// <summary>
    /// F_W_SkillEffectPool_Fail 선언
    /// </summary>
    F_W_SkillEffectPool_Fail f_W_Skill_Fail;

    /// <summary>
    /// B_W_SkillEffectPool_Prepare 선언
    /// </summary>
    B_W_SkillEffectPool_Prepare b_W_Skill_Prepare;

    /// <summary>
    /// B_W_SkillEffectPool_Success 선언
    /// </summary>
    B_W_SkillEffectPool_Success b_W_Skill_Success;

    /// <summary>
    /// B_W_SkillEffectPool_Fail 선언
    /// </summary>
    B_W_SkillEffectPool_Fail b_W_Skill_Fail;

    /// <summary>
    /// H_W_SkillEffectPool_Prepare 선언
    /// </summary>
    H_W_SkillEffectPool_Prepare h_W_Skill_Prepare;

    /// <summary>
    /// H_W_SkillEffectPool_Success 선언
    /// </summary>
    H_W_SkillEffectPool_Success h_W_Skill_Success;

    /// <summary>
    /// H_W_SkillEffectPool_Fail 선언
    /// </summary>
    H_W_SkillEffectPool_Fail h_W_Skill_Fail;

    /// <summary>
    /// M_W_SkillEffectPool_Prepare 선언
    /// </summary>
    M_W_SkillEffectPool_Prepare m_W_Skill_Prepare;

    /// <summary>
    /// M_W_SkillEffectPool_Success 선언
    /// </summary>
    M_W_SkillEffectPool_Success m_W_Skill_Success;

    /// <summary>
    /// M_W_SkillEffectPool_Fail 선언
    /// </summary>
    M_W_SkillEffectPool_Fail m_W_Skill_Fail;

    /// <summary>
    /// A_W_SkillEffectPool_Prepare 선언
    /// </summary>
    A_W_SkillEffectPool_Prepare a_W_Skill_Prepare;

    /// <summary>
    /// A_W_SkillEffectPool_Success 선언
    /// </summary>
    A_W_SkillEffectPool_Success a_W_Skill_Success;

    /// <summary>
    /// A_W_SkillEffectPool_Fail 선언
    /// </summary>
    A_W_SkillEffectPool_Fail a_W_Skill_Fail;

    /// <summary>
    /// F_E_SkillEffectPool1 선언
    /// </summary>
    F_E_SkillEffectPool1 f_E_SkillEffectPool1;

    /// <summary>
    /// F_E_SkillEffectPool2 선언
    /// </summary>
    F_E_SkillEffectPool2 f_E_SkillEffectPool2;

    /// <summary>
    /// F_E_SkillEffectPool3 선언
    /// </summary>
    F_E_SkillEffectPool3 f_E_SkillEffectPool3;

    /// <summary>
    /// B_E_SkillEffectPool1 선언
    /// </summary>
    B_E_SkillEffectPool1 b_E_SkillEffectPool1;

    /// <summary>
    /// B_E_SkillEffectPool2 선언
    /// </summary>
    B_E_SkillEffectPool2 b_E_SkillEffectPool2;

    /// <summary>
    /// H_E_SkillEffectPool1 선언
    /// </summary>
    H_E_SkillEffectPool1 h_E_SkillEffectPool1;

    /// <summary>
    /// H_E_SkillEffectPool2 선언
    /// </summary>
    H_E_SkillEffectPool2 h_E_SkillEffectPool2;

    /// <summary>
    /// M_E_SkillEffectPool1 선언
    /// </summary>
    M_E_SkillEffectPool1 m_E_SkillEffectPool1;

    /// <summary>
    /// M_E_SkillEffectPool2 선언
    /// </summary>
    M_E_SkillEffectPool2 m_E_SkillEffectPool2;

    /// <summary>
    /// M_E_SkillEffectPool3 선언
    /// </summary>
    M_E_SkillEffectPool3 m_E_SkillEffectPool3;

    /// <summary>
    /// A_E_SkillEffectPool1 선언
    /// </summary>
    A_E_SkillEffectPool1 a_E_SkillEffectPool1;

    /// <summary>
    /// A_E_SkillEffectPool2 선언
    /// </summary>
    A_E_SkillEffectPool2 a_E_SkillEffectPool2;

    /// <summary>
    /// A_E_SkillEffectPool3 선언
    /// </summary>
    A_E_SkillEffectPool3 a_E_SkillEffectPool3;

    /// <summary>
    /// F_R_SkillEffectPool1 선언
    /// </summary>
    F_R_SkillEffectPool1 f_R_SkillEffectPool1;

    /// <summary>
    /// F_R_SkillEffectPool2 선언
    /// </summary>
    F_R_SkillEffectPool2 f_R_SkillEffectPool2;

    /// <summary>
    /// B_R_SkillEffectPool1 선언
    /// </summary>
    B_R_SkillEffectPool1 b_R_SkillEffectPool1;

    /// <summary>
    /// B_R_SkillEffectPool2 선언
    /// </summary>
    B_R_SkillEffectPool2 b_R_SkillEffectPool2;

    /// <summary>
    /// H_R_SkillEffectPool1 선언
    /// </summary>
    H_R_SkillEffectPool1 h_R_SkillEffectPool1;

    /// <summary>
    /// M_R_SkillEffectPool1 선언
    /// </summary>
    M_R_SkillEffectPool1 m_R_SkillEffectPool1;

    /// <summary>
    /// M_R_SkillEffectPool2 선언
    /// </summary>
    M_R_SkillEffectPool2 m_R_SkillEffectPool2;


    /// <summary>
    /// A_R_SkillEffectPool1 선언
    /// </summary>
    A_R_SkillEffectPool1 a_R_SkillEffectPool1;

    /// <summary>
    /// A_R_SkillEffectPool2 선언
    /// </summary>
    A_R_SkillEffectPool2 a_R_SkillEffectPool2;

    /// <summary>
    /// LevelUpEffectPool
    /// </summary>
    LevelUpEffectPool levelUpEffectPool;

    /// <summary>
    /// DamageText_E_Pool
    /// </summary>
    DamageText_E_Pool damageText_E_Pool;

    /// <summary>
    /// DamageText_P_Pool
    /// </summary>
    DamageText_P_Pool damageText_P_Pool;

    /// <summary>
    /// HitEnemyEffectPool
    /// </summary>
    HitEnemyEffectPool hitEnemyEffectPool;

    /// <summary>
    /// HitPlayerEffectPool
    /// </summary>
    HitPlayerEffectPool hitPlayerEffectPool;

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

        // MummyPool 초기화 및 생성
        mummy = GetComponentInChildren<MummyPool>();
        if (mummy != null)
            mummy.Initialize();

        // UndeadPool 초기화 및 생성
        undead = GetComponentInChildren<UndeadPool>();
        if (undead != null)
            undead.Initialize();

        // VampirePool 초기화 및 생성
        vampire = GetComponentInChildren<VampirePool>();
        if (vampire != null)
            vampire.Initialize();

        // DemonLordPool 초기화 및 생성
        demonLord = GetComponentInChildren<DemonLordPool>();
        if (demonLord != null)
            demonLord.Initialize();

        // GhoulAttackPool 초기화 및 생성
        ghoulAttack = GetComponentInChildren<GhoulAttackPool>();
        if (ghoulAttack != null)
            ghoulAttack.Initialize();

        // SkeletonAttackPool 초기화 및 생성
        skeletonAttack = GetComponentInChildren<SkeletonAttackPool>();
        if (skeletonAttack != null)
            skeletonAttack.Initialize();

        // MummyAttack1Pool 초기화 및 생성
        mummyAttack1 = GetComponentInChildren<MummyAttack1Pool>();
        if (mummyAttack1 != null)
            mummyAttack1.Initialize();

        // MummyAttack2Pool 초기화 및 생성
        mummyAttack2 = GetComponentInChildren<MummyAttack2Pool>();
        if (mummyAttack2 != null)
            mummyAttack2.Initialize();

        // UndeadAttackPool 초기화 및 생성
        undeadAttack = GetComponentInChildren<UndeadAttackPool>();
        if (undeadAttack != null)
            undeadAttack.Initialize();

        // VampireAttack1Pool 초기화 및 생성
        vampireAttack1 = GetComponentInChildren<VampireAttack1Pool>();
        if (vampireAttack1 != null)
            vampireAttack1.Initialize();

        // VampireAttack2Pool 초기화 및 생성
        vampireAttack2 = GetComponentInChildren<VampireAttack2Pool>();
        if (vampireAttack2 != null)
            vampireAttack2.Initialize();

        // BossRoarPool 초기화 및 생성
        bossRoar = GetComponentInChildren<BossRoarPool>();
        if (bossRoar != null)
            bossRoar.Initialize();

        // BossAttack1Pool 초기화 및 생성
        bossAttack1 = GetComponentInChildren<BossAttack1Pool>();
        if (bossAttack1 != null)
            bossAttack1.Initialize();

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

        // F_W_SkillEffectPool(Prepare) 초기화 및 생성
        f_W_Skill_Prepare = GetComponentInChildren<F_W_SkillEffectPool_Prepare>();
        if (f_W_Skill_Prepare != null)
            f_W_Skill_Prepare.Initialize();

        // F_W_SkillEffectPool(Success) 초기화 및 생성
        f_W_Skill_Success = GetComponentInChildren<F_W_SkillEffectPool_Success>();
        if (f_W_Skill_Success != null)
            f_W_Skill_Success.Initialize();

        // F_W_SkillEffectPool(Fail) 초기화 및 생성
        f_W_Skill_Fail = GetComponentInChildren<F_W_SkillEffectPool_Fail>();
        if (f_W_Skill_Fail != null)
            f_W_Skill_Fail.Initialize();

        // B_W_SkillEffectPool(Prepare) 초기화 및 생성
        b_W_Skill_Prepare = GetComponentInChildren<B_W_SkillEffectPool_Prepare>();
        if (b_W_Skill_Prepare != null)
            b_W_Skill_Prepare.Initialize();

        // B_W_SkillEffectPool(Success) 초기화 및 생성
        b_W_Skill_Success = GetComponentInChildren<B_W_SkillEffectPool_Success>();
        if (b_W_Skill_Success != null)
            b_W_Skill_Success.Initialize();

        // B_W_SkillEffectPool(Fail) 초기화 및 생성
        b_W_Skill_Fail = GetComponentInChildren<B_W_SkillEffectPool_Fail>();
        if (b_W_Skill_Fail != null)
            b_W_Skill_Fail.Initialize();

        // H_W_SkillEffectPool(Prepare) 초기화 및 생성
        h_W_Skill_Prepare = GetComponentInChildren<H_W_SkillEffectPool_Prepare>();
        if (h_W_Skill_Prepare != null)
            h_W_Skill_Prepare.Initialize();

        // H_W_SkillEffectPool(Success) 초기화 및 생성
        h_W_Skill_Success = GetComponentInChildren<H_W_SkillEffectPool_Success>();
        if (h_W_Skill_Success != null)
            h_W_Skill_Success.Initialize();

        // H_W_SkillEffectPool(Fail) 초기화 및 생성
        h_W_Skill_Fail = GetComponentInChildren<H_W_SkillEffectPool_Fail>();
        if (h_W_Skill_Fail != null)
            h_W_Skill_Fail.Initialize();

        // M_W_SkillEffectPool(Prepare) 초기화 및 생성
        m_W_Skill_Prepare = GetComponentInChildren<M_W_SkillEffectPool_Prepare>();
        if (m_W_Skill_Prepare != null)
            m_W_Skill_Prepare.Initialize();
    
        // M_W_SkillEffectPool(Success) 초기화 및 생성
        m_W_Skill_Success = GetComponentInChildren<M_W_SkillEffectPool_Success>();
        if (m_W_Skill_Success != null)
            m_W_Skill_Success.Initialize();

        // M_W_SkillEffectPool(Fail) 초기화 및 생성
        m_W_Skill_Fail = GetComponentInChildren<M_W_SkillEffectPool_Fail>();
        if (m_W_Skill_Fail != null)
            m_W_Skill_Fail.Initialize();

        // A_W_SkillEffectPool(Prepare) 초기화 및 생성
        a_W_Skill_Prepare = GetComponentInChildren<A_W_SkillEffectPool_Prepare>();
        if (a_W_Skill_Prepare != null)
            a_W_Skill_Prepare.Initialize();

        // A_W_SkillEffectPool(Success) 초기화 및 생성
        a_W_Skill_Success = GetComponentInChildren<A_W_SkillEffectPool_Success>();
        if (a_W_Skill_Success != null)
            a_W_Skill_Success.Initialize();

        // A_W_SkillEffectPool(Fail) 초기화 및 생성
        a_W_Skill_Fail = GetComponentInChildren<A_W_SkillEffectPool_Fail>();
        if (a_W_Skill_Fail != null)
            a_W_Skill_Fail.Initialize();

        // F_E_SkillEffectPool1 초기화 및 생성
        f_E_SkillEffectPool1 = GetComponentInChildren<F_E_SkillEffectPool1>();
        if (f_E_SkillEffectPool1 != null)
            f_E_SkillEffectPool1 .Initialize();

        // F_E_SkillEffectPool2 초기화 및 생성
        f_E_SkillEffectPool2 = GetComponentInChildren<F_E_SkillEffectPool2>();
        if (f_E_SkillEffectPool2 != null)
            f_E_SkillEffectPool2.Initialize();

        // F_E_SkillEffectPool3 초기화 및 생성
        f_E_SkillEffectPool3 = GetComponentInChildren<F_E_SkillEffectPool3>();
        if (f_E_SkillEffectPool3 != null)
            f_E_SkillEffectPool3.Initialize();

        // B_E_SkillEffectPool1 초기화 및 생성
        b_E_SkillEffectPool1 = GetComponentInChildren<B_E_SkillEffectPool1>();
        if (b_E_SkillEffectPool1 != null)
            b_E_SkillEffectPool1.Initialize();

        // B_E_SkillEffectPool2 초기화 및 생성
        b_E_SkillEffectPool2 = GetComponentInChildren<B_E_SkillEffectPool2>();
        if (b_E_SkillEffectPool2 != null)
            b_E_SkillEffectPool2.Initialize();

        // H_E_SkillEffectPool1 초기화 및 생성
        h_E_SkillEffectPool1 = GetComponentInChildren<H_E_SkillEffectPool1>();
        if (h_E_SkillEffectPool1 != null)
            h_E_SkillEffectPool1.Initialize();

        // H_E_SkillEffectPool2 초기화 및 생성
        h_E_SkillEffectPool2 = GetComponentInChildren<H_E_SkillEffectPool2>();
        if (h_E_SkillEffectPool2 != null)
            h_E_SkillEffectPool2.Initialize();

        // M_E_SkillEffectPool1 초기화 및 생성
        m_E_SkillEffectPool1 = GetComponentInChildren<M_E_SkillEffectPool1>();
        if (m_E_SkillEffectPool1 != null)
            m_E_SkillEffectPool1.Initialize();

        // M_E_SkillEffectPool2 초기화 및 생성
        m_E_SkillEffectPool2 = GetComponentInChildren<M_E_SkillEffectPool2>();
        if (m_E_SkillEffectPool2 != null)
            m_E_SkillEffectPool2.Initialize();

        // M_E_SkillEffectPoole3 초기화 및 생성
        m_E_SkillEffectPool3 = GetComponentInChildren<M_E_SkillEffectPool3>();
        if (m_E_SkillEffectPool3 != null)
            m_E_SkillEffectPool3.Initialize();

        // A_E_SkillEffectPool1 초기화 및 생성
        a_E_SkillEffectPool1 = GetComponentInChildren<A_E_SkillEffectPool1>();
        if (a_E_SkillEffectPool1 != null)
            a_E_SkillEffectPool1.Initialize();

        // A_E_SkillEffectPool2 초기화 및 생성
        a_E_SkillEffectPool2 = GetComponentInChildren<A_E_SkillEffectPool2>();
        if (a_E_SkillEffectPool2 != null)
            a_E_SkillEffectPool2.Initialize();

        // A_E_SkillEffectPool3 초기화 및 생성
        a_E_SkillEffectPool3 = GetComponentInChildren<A_E_SkillEffectPool3>();
        if (a_E_SkillEffectPool3 != null)
            a_E_SkillEffectPool3.Initialize();

        // F_R_SkillEffectPool1 초기화 및 생성
        f_R_SkillEffectPool1 = GetComponentInChildren<F_R_SkillEffectPool1>();
        if (f_R_SkillEffectPool1 != null)
            f_R_SkillEffectPool1.Initialize();

        // F_R_SkillEffectPool2 초기화 및 생성
        f_R_SkillEffectPool2 = GetComponentInChildren<F_R_SkillEffectPool2>();
        if (f_R_SkillEffectPool2 != null)
            f_R_SkillEffectPool2.Initialize();

        // B_R_SkillEffectPool1 초기화 및 생성
        b_R_SkillEffectPool1 = GetComponentInChildren<B_R_SkillEffectPool1>();
        if (b_R_SkillEffectPool1 != null)
            b_R_SkillEffectPool1.Initialize();

        // B_R_SkillEffectPool2 초기화 및 생성
        b_R_SkillEffectPool2 = GetComponentInChildren<B_R_SkillEffectPool2>();
        if (b_R_SkillEffectPool2 != null)
            b_R_SkillEffectPool2.Initialize();

        // H_R_SkillEffectPool1 초기화 및 생성
        h_R_SkillEffectPool1 = GetComponentInChildren<H_R_SkillEffectPool1>();
        if (h_R_SkillEffectPool1 != null)
            h_R_SkillEffectPool1.Initialize();

        // M_R_SkillEffectPool1 초기화 및 생성
        m_R_SkillEffectPool1 = GetComponentInChildren<M_R_SkillEffectPool1>();
        if (m_R_SkillEffectPool1 != null)
            m_R_SkillEffectPool1.Initialize();

        // M_R_SkillEffectPool2 초기화 및 생성
        m_R_SkillEffectPool2 = GetComponentInChildren<M_R_SkillEffectPool2>();
        if (m_R_SkillEffectPool2 != null)
            m_R_SkillEffectPool2.Initialize();

        // A_R_SkillEffectPool1 초기화 및 생성
        a_R_SkillEffectPool1 = GetComponentInChildren<A_R_SkillEffectPool1>();
        if (a_R_SkillEffectPool1 != null)
            a_R_SkillEffectPool1.Initialize();

        // A_R_SkillEffectPool2 초기화 및 생성
        a_R_SkillEffectPool2 = GetComponentInChildren<A_R_SkillEffectPool2>();
        if (a_R_SkillEffectPool2 != null)
            a_R_SkillEffectPool2.Initialize();

        // LevelUpEffectPool 초기화 및 생성
        levelUpEffectPool = GetComponentInChildren<LevelUpEffectPool>();
        if (levelUpEffectPool != null)
            levelUpEffectPool.Initialize();

        // DamageText_E_Pool 초기화 및 생성
        damageText_E_Pool = GetComponentInChildren<DamageText_E_Pool>();
        if (damageText_E_Pool != null)
            damageText_E_Pool.Initialize();

        // DamageText_P_Pool 초기화 및 생성
        damageText_P_Pool = GetComponentInChildren<DamageText_P_Pool>();
        if (damageText_P_Pool != null)
            damageText_P_Pool.Initialize();

        // HitEnemyEffectPool 초기화 및 생성
        hitEnemyEffectPool = GetComponentInChildren<HitEnemyEffectPool>();
        if (hitEnemyEffectPool != null)
            hitEnemyEffectPool.Initialize();

        // HitPlayerEffectPool 초기화 및 생성
        hitPlayerEffectPool = GetComponentInChildren<HitPlayerEffectPool>();
        if (hitPlayerEffectPool != null)
            hitPlayerEffectPool.Initialize();
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
    /// Mummy 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 Mummy</returns>
    public MummyController GetMummy(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return mummy.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Undead 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 Undead</returns>
    public UndeadController GetUndead(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return undead.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Vampire 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 Vampire</returns>
    public VampireController GetVampire(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return vampire.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// DemonLord 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 DemonLord</returns>
    public BossController GetDemonLord(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return demonLord.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// GhoulAttack 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 GhoulAttack</returns>
    public EnemyAttackEffect GetGhoulAttack(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return ghoulAttack.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// SkeletonAttack 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 SkeletonAttack</returns>
    public EnemyAttackEffect GetSkeletonAttack(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return skeletonAttack.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// MummyAttack1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 MummyAttack1</returns>
    public EnemyAttackEffect GetMummyAttack1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return mummyAttack1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// MummyAttack2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 MummyAttack2</returns>
    public EnemyAttackEffect GetMummyAttack2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return mummyAttack2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// UndeadAttack 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 UndeadAttack</returns>
    public EnemyAttackEffect GetUndeadAttack(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return undeadAttack.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// VampireAttack1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 VampireAttack1</returns>
    public EnemyAttackEffect GetVampireAttack1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return vampireAttack1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// VampireAttack2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 VampireAttack2</returns>
    public EnemyAttackEffect GetVampireAttack2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return vampireAttack2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// BossRoar 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 BossRoar</returns>
    public BossEffect GetBossRoar(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return bossRoar.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// BossAttack1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 BossAttack1</returns>
    public BossAttackEffect GetBossAttack1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return bossAttack1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// FistEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 FistEffect</returns>
    public WeaponEffect GetFistEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return fistEffect.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// GreatSwordEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 GreatSwordEffect</returns>
    public WeaponEffect GetGreatSwordEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return greatSwordEffect.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// RipleEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 RipleEffect</returns>
    public WeaponEffect GetRipleEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return ripleEffect.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// StaffEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 StaffEffect</returns>
    public WeaponEffect GetStaffEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return staffEffect.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// DaggerEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 DaggerEffect</returns>
    public WeaponEffect GetDaggerEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return daggerEffect.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Fighter W스킬 이펙트 소환 함수(준비 단계)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetFighterWSkill_Prepare(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return f_W_Skill_Prepare.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Fighter W스킬 이펙트 소환 함수(성공 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetFighterWSkill_Success(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return f_W_Skill_Success.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Fighter W스킬 이펙트 소환 함수(실패 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetFighterWSkill_Fail(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return f_W_Skill_Fail.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Berserker W스킬 이펙트 소환 함수(준비 단계)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetBerserkerWSkill_Prepare(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return b_W_Skill_Prepare.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Berserker W스킬 이펙트 소환 함수(성공 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetBerserkerWSkill_Success(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return b_W_Skill_Success.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Berserker W스킬 이펙트 소환 함수(실패 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetBerserkerWSkill_Fail(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return b_W_Skill_Fail.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Hunter W스킬 이펙트 소환 함수(준비 단계)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetHunterWSkill_Prepare(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return h_W_Skill_Prepare.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Hunter W스킬 이펙트 소환 함수(성공 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetHunterWSkill_Success(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return h_W_Skill_Success.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Hunter W스킬 이펙트 소환 함수(실패 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetHunterWSkill_Fail(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return h_W_Skill_Fail.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Magician W스킬 이펙트 소환 함수(준비 단계)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetMagicianWSkill_Prepare(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return m_W_Skill_Prepare.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Magician W스킬 이펙트 소환 함수(성공 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetMagicianWSkill_Success(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return m_W_Skill_Success.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Magician W스킬 이펙트 소환 함수(실패 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetMagicianWSkill_Fail(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return m_W_Skill_Fail.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Assassin W스킬 이펙트 소환 함수(준비 단계)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetAssassinWSkill_Prepare(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return a_W_Skill_Prepare.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Assassin W스킬 이펙트 소환 함수(성공 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetAssassinWSkill_Success(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return a_W_Skill_Success.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Assassin W스킬 이펙트 소환 함수(실패 시)
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public W_SkillEffect GetAssassinWSkill_Fail(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return a_W_Skill_Fail.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Fighter E스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetFighterESkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return f_E_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Fighter E스킬 이펙트2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetFighterESkill2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return f_E_SkillEffectPool2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Fighter E스킬 이펙트3 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetFighterESkill3(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return f_E_SkillEffectPool3.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Berserker E스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetBerserkerESkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return b_E_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Berserker E스킬 이펙트2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetBerserkerESkill2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return b_E_SkillEffectPool2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Hunter E스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetHunterESkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return h_E_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Hunter E스킬 이펙트2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetHunterESkill2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return h_E_SkillEffectPool2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Magician E스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetMagicianESkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return m_E_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Magician E스킬 이펙트2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetMagicianESkill2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return m_E_SkillEffectPool2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Magician E스킬 이펙트3 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetMagicianESkill3(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return m_E_SkillEffectPool3.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Assassin E스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetAssassinESkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return a_E_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Assassin E스킬 이펙트2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetAssassinESkill2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return a_E_SkillEffectPool2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Assassin E스킬 이펙트3 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public E_SkillEffect GetAssassinESkill3(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return a_E_SkillEffectPool3.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Fighter R스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public R_SkillEffect GetFighterRSkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return f_R_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Fighter R스킬 이펙트2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public R_SkillEffect GetFighterRSkill2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return f_R_SkillEffectPool2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Berserker R스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public R_SkillEffect GetBerserkerRSkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return b_R_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Berserker R스킬 이펙트2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public R_SkillEffect GetBerserkerRSkill2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return b_R_SkillEffectPool2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Hunter R스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public R_SkillEffect GetHunterRSkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return h_R_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Magician R스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public R_SkillEffect GetMagicianRSkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return m_R_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Magician R스킬 이펙트2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public R_SkillEffect GetMagicianRSkill2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return m_R_SkillEffectPool2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Assassin R스킬 이펙트1 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public R_SkillEffect GetAssassinRSkill1(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return a_R_SkillEffectPool1.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// Assassin R스킬 이펙트2 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public R_SkillEffect GetAssassinRSkill2(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return a_R_SkillEffectPool2.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// LevelUpEffect 소환 함수
    /// </summary>
    /// <param name="position">소환 위치</param>
    /// <param name="eulerAngle">소환 각도</param>
    /// <returns>소환된 이펙트</returns>
    public LevelUpEffect GetLevelUpEffect(Vector3? position = null, Vector3? eulerAngle = null)
    {
        return levelUpEffectPool.GetObject(position, eulerAngle);
    }

    /// <summary>
    /// 데미지 텍스트 소환 함수(Enemy용)
    /// </summary>
    /// <param name="damage">데미지 양</param>
    /// <param name="position">소환 위치</param>
    /// <returns>소환된 데미지 텍스트</returns>
    public DamageText MakeDamageText_E(int damage, Vector3? position = null)
    {
        DamageText damageText = damageText_E_Pool.GetObject(position);
        damageText.SetDamage(damage);
        return damageText;
    }

    /// <summary>
    /// 데미지 텍스트 소환 함수(Player용)
    /// </summary>
    /// <param name="damage">데미지 양</param>
    /// <param name="position">소환 위치</param>
    /// <returns>소환된 데미지 텍스트</returns>
    public DamageText MakeDamageText_P(int damage, Vector3? position = null)
    {
        DamageText damageText = damageText_P_Pool.GetObject(position);
        damageText.SetDamage(damage);
        return damageText;
    }
}
