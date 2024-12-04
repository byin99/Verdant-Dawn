using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSelector : StateMachineBehaviour
{
    const int Not_Select = -1;

    readonly int IdleSelect_Hash = Animator.StringToHash("IdleSelect");

    int prevSelect = 0;

    // OnStateEnter is called when a transition starts and the playerClass machine starts to evaluate this playerClass
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(IdleSelect_Hash, RandomSelect());
    }

    int RandomSelect()
    {
        int select = 0;

        // 이전 선택이 0번일 경우에만 일정확률로 1 ~ 4를 선택
        if (prevSelect == 0)
        {
            float num = Random.value;

            if (num > 0.1f)
            {
                select = 1;         // 10%
            }
            else if (num < 0.2f)
            {
                select = 2;         // 10%
            }
        }

        prevSelect = select;        // 이전 선택을 기록
        return select;
    }
}
