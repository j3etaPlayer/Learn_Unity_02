using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;

    [Header("플레이어 제어 변수")]
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }
    public void PlayerTargetActionAnimation(string targetAnimation, bool isPerformingAcion,
                                                                    bool applyRootMotion = true,
                                                                    bool canRotate = false, 
                                                                    bool canMove = false)   // 애니메이션 클립의 이름을 호출하여 각 playerManager에서 에니메이션을 쉽게 호출할수 있게 하느 ㄴ함수
    {
        animator.CrossFade(targetAnimation, 0.2f);
        player.isPerformingAction = isPerformingAcion;
        player.applyRootMotion = applyRootMotion;
        player.canRotate = canRotate;
        player.canMove = canMove;
    }
}
