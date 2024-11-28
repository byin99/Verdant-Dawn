using System.Collections;
using UnityEngine;

/// <summary>
/// 클래스가 가지는 인터페이스
/// </summary>
public interface IClass
{
    /// <summary>
    /// 클래스별로 가지는 애니메이션 시간을 바꿔주는 함수
    /// </summary>
    public void ChangeAnimTime();

    /// <summary>
    /// 공격 이펙트 소환함수
    /// </summary>
    /// <param name="attackTransform">공격 트랜스폼</param>
    public void AttackEffect(Transform attackTransform);

    /// <summary>
    /// W스킬 이펙트(준비)
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void W_Skill_Prepare(Transform attackTransform);

    /// <summary>
    /// W스킬 이펙트(성공)
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void W_Skill_Success(Transform attackTransform);

    /// <summary>
    /// W스킬 이펙트(실패)
    /// </summary>
    /// <param name="attackTransform">Effect 소환 트랜스폼</param>
    public void W_Skill_Fail(Transform attackTransform);

    /// <summary>
    /// 무기를 장착하는 코루틴(무기가 애니메이션에 맞게 보여지기 위함)
    /// </summary>
    /// <param name="sender">장착할 플레이어</param>
    public IEnumerator EquipWeapon(PlayerClass sender);
}