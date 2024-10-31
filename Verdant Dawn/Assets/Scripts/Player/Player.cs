using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController), typeof(PlayerMovement), typeof(PlayerAttack))]
public class Player : MonoBehaviour
{
    // 컴포넌트들
    PlayerInputController inputcontroller;
    PlayerMovement movement;
    PlayerAttack attack;

    private void Awake()
    {
        inputcontroller = GetComponent<PlayerInputController>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();

        inputcontroller.onMove += movement.SetDestination;
        inputcontroller.onRoll += movement.Roll;
        inputcontroller.onAttack += attack.Attack;
    }

}
