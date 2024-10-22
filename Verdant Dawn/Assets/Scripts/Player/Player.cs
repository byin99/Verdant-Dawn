using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputController), typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    // 컴포넌트들
    PlayerInputController inputcontroller;
    PlayerMovement movement;

    private void Awake()
    {
        inputcontroller = GetComponent<PlayerInputController>();
        movement = GetComponent<PlayerMovement>();

        inputcontroller.onMove += movement.SetDestination;
        inputcontroller.onRoll += movement.Roll;
    } 
}
