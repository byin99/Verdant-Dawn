using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : IState<PlayerClass>
{
    /// <summary>
    /// 장비하는 시간
    /// </summary>
    protected float equipTime;

    /// <summary>
    /// 차징에 필요한 시간
    /// </summary>
    protected float chargingTime = 2.0f;

    /// <summary>
    /// 차징하고 있는 시간
    /// </summary>
    protected float chargingTimeElapsed = 0.0f;

    // 컴포넌트들
    protected Animator animator;
    protected Player player;
    protected PlayerMovement movement;
    protected PlayerAttack attack;
    protected PlayerInputController input;
    protected SkillBarUI skillBarUI;

    /// <summary>
    /// 장비하는 코루틴 저장용(무기를 장비하는 도중에 다른 무기로 바꿀때 이전 코루틴 제거용)
    /// </summary>
    protected IEnumerator equipWeapon;

    /// <summary>
    /// 차징 스킬 코루틴 저장용(차징 스킬을 중간에 멈췄을 때, 사용하는 코루틴 제거용)
    /// </summary>
    protected IEnumerator chargingSkill;

    /// <summary>
    /// 차징 스킬 이펙트(삭제용)
    /// </summary>
    protected W_SkillEffect w_SkillEffect;

    // 해쉬 값
    protected readonly int Class_Hash = Animator.StringToHash("Class");
    protected readonly int WSkill_Hash = Animator.StringToHash("WSkill");

    public virtual void Enter(PlayerClass sender)
    {
        if (animator == null)
        {
            animator = sender.GetComponent<Animator>();
        }

        if (player == null)
        {
            player = GameManager.Instance.Player;
        }

        if (movement == null)
        {
            movement = GameManager.Instance.PlayerMovement;
        }

        if (attack == null)
        {
            attack = GameManager.Instance.PlayerAttack;
        }

        if (input == null)
        {
            input = GameManager.Instance.PlayerInputController;
        }

        if (skillBarUI == null)
        {
            skillBarUI = UIManager.Instance.SkillBarUI;
        }
    }

    public virtual void Exit(PlayerClass sender)
    {
    }

    public virtual void UpdateState(PlayerClass sender)
    {
        
    }
}
