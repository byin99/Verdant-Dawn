using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : IState<PlayerClass>
{
    /// <summary>
    /// 장비하는 시간
    /// </summary>
    protected float equipTime;

    // 컴포넌트들
    protected Animator animator;
    protected PlayerMovement movement;
    protected PlayerAttack attack;

    // 해쉬 값
    protected readonly int Class_Hash = Animator.StringToHash("Class");

    public virtual void Enter(PlayerClass sender)
    {
        if (animator == null)
        {
            animator = sender.GetComponent<Animator>();
        }

        if (movement == null)
        {
            movement = GameManager.Instance.PlayerMovement;
        }

        if (attack == null)
        {
            attack = GameManager.Instance.PlayerAttack;
        }
    }

    public virtual void Exit(PlayerClass sender)
    {
    }

    public virtual void UpdateState(PlayerClass sender)
    {
        
    }
}
