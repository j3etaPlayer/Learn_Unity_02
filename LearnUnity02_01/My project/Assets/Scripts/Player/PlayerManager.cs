using CameraSetting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Common Player Data")]
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [Header("플레이어 제약 조건")]
    public bool isPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool canCombo = false;

    [Header("Player Manager Script")]
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public PlayerMovementManager playerMovementManager;

    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
}
