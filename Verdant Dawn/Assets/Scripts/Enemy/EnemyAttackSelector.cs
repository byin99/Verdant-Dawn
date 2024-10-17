using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSelector : StateMachineBehaviour
{
    // 애니메이터 해쉬값
    readonly int AttackSelect_Hash = Animator.StringToHash("AttackSelect");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(AttackSelect_Hash, Random.Range(0, 2));   // 랜덤으로 Attack애니메이션 나오게 하기      
    }
}
