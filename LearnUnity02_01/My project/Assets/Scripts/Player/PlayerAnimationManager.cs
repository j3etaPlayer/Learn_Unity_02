using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;

    [Header("�÷��̾� ���� ����")]
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }
    public void PlayerTargetActionAnimation(string targetAnimation, bool isPerformingAcion,
                                                                    bool applyRootMotion = true,
                                                                    bool canRotate = false, 
                                                                    bool canMove = false)   // �ִϸ��̼� Ŭ���� �̸��� ȣ���Ͽ� �� playerManager���� ���ϸ��̼��� ���� ȣ���Ҽ� �ְ� �ϴ� ���Լ�
    {
        animator.CrossFade(targetAnimation, 0.2f);
        player.isPerformingAction = isPerformingAcion;
        player.applyRootMotion = applyRootMotion;
        player.canRotate = canRotate;
        player.canMove = canMove;
    }
}
