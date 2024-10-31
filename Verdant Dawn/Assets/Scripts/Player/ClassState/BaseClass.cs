using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : IState<PlayerClass>
{
    /// <summary>
    /// 직업별 구르기 시간
    /// </summary>
    protected float rollTime;

    /// <summary>
    /// 장비하는 시간
    /// </summary>
    protected float equipTime;

    // 컴포넌트들
    protected Animator animator;
    protected PlayerMovement movement;

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
            movement = sender.gameObject.GetComponent<PlayerMovement>();
        }

        movement.rollAnimTime = rollTime;
    }

    public virtual void Exit(PlayerClass sender)
    {
    }

    public virtual void UpdateState(PlayerClass sender)
    {
    }
}
