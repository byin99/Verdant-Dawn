using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    public bool isPossibleAttack = true;

    Animator animator;
    NavMeshAgent agent;

    readonly int Attack_Hash = Animator.StringToHash("Attack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Attack()
    {
        if (isPossibleAttack)
        {
            animator.SetTrigger(Attack_Hash);
            agent.isStopped = true;
            isPossibleAttack = false;
        }
    }

    void EndAttackCoolTime()
    {
        isPossibleAttack = true;
        agent.ResetPath();
        agent.isStopped = false;
    }


}
