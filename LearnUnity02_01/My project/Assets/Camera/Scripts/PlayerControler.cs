using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSetting
{
    [RequireComponent(typeof(CharacterController))]

    public class PlayerControler : MonoBehaviour
    {
        [Header("플레이어 입력 제어 변수")]
        [SerializeField] private float runSpeed;        // 플레이어 달리기 속도
        [SerializeField] private float moveSpeed;       // 플레이어 기본 속도
        [SerializeField] private float jumpForce;       // 플레이어 점프력

        private CharacterController playerCCon;         // rigidbody 대신 character에 물리 충돌기능과 이동을 위한 컴포너트
                                                        // RequireComponent로 무조건 실행이 된다.

        [Header("카메라 제어 변수")]
        [SerializeField] ThirdCamCamera thirdCam;               // 3인칭 카메라 컨트롤러가 있는 게임 오브젝트
        [SerializeField] private float smoothRotation = 5.0f;   // 카메라의 자연스러운 회전을 위한 가중치
        Quaternion targetRotation;                              // 키보드 입력을 하지 않았을때
                                                                // 카메라 방향으로 회전하기 위한 회전각도를 저장하는 변수

        [Header("점프 제어 변수")]
        [SerializeField] private float gravityModifier;
        [SerializeField] private Vector3 groundCheckPoint;    // 땅을 판별하기 위한 체크포인트
        [SerializeField] private float groundCheckRadius;       // 땅 체크하는 구의 크기 반지름
        [SerializeField] private LayerMask groundLayer;         // 체크할 레이어가 땅인지 판별하는 변수
        private bool isGrounded;                                // true면 점프가 가능, false면 점프 제한


        private float activeMoveSpeed;                          // 실제로 플레이어가 이동할 속력을 저장할 변수
        private Vector3 moveMent;                               // 플레이어가 움직이는 방향과 거리가 최종 Vector값

        void Start()
        {
            playerCCon = GetComponent<CharacterController>();
        }

        void Update()
        {
            // 1. Input 클래스를 이용하여 키보드 입력을 제어
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // 키보드 Input과 입력 갑승ㄹ 확인하기 위한 변수 선언
            Vector3 moveInput = new Vector3(horizontal, 0, vertical);                       // 키보드 입력값을 저장하는 벡터
            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));  // 키보드로 상하 좌우키 한개만 입력하면 0보다 큰 값을 moveAmount에 저장한다.

            // 힘을 줘서 이동한다.
            // playerRigid.AddForce(moveDir * moveSpeed);
            // rigid.AddForce는 time.deltatime이 함수내에 포함되어 있다.

            // 위치를 변화시킨다.
            // 현재위치 + 속도 + 가속도 = 이동 거리
            // transform.position += moveDir * moveSpeed * Time.deltaTime;

            Vector3 moveDirection = thirdCam.camLookRotation * moveInput;   // 플레이어가 이동할 방향을 저장하는 변수 moveDirection

            // 4. 플레이어의 이동속도를 다르게해주는 코드(달리기)
            if (Input.GetKey(KeyCode.LeftShift))                             // key down : 누를때 한번만 적용, key : key를 떼기전까지는 계속
                activeMoveSpeed = runSpeed;
            else
                activeMoveSpeed = moveSpeed;

            // 5. 점프를 하기 위한 계산식 (중력이 필요하다)
            float yValue = moveMent.y;                                      // 떨어지고있는 y의 크기를 저장
            moveMent = moveDirection * activeMoveSpeed;                     // 벡터 좌표의 x, 0, z 벡터 값을 저장, 떨어지고 있는 값을 0으로 초기화
            moveMent.y = yValue;                                            // 중력에 힘을 계속 받도록 잃어버린 변수를 다시 불러온다.

            GroundCheck();

            if (isGrounded)
            {
                moveMent.y = 0;
                Debug.Log("땅에 있습니다.");
            }
            else
                Debug.Log("공중입니다");


            // 점프키를 입려하여 점프 구현
            if (Input.GetButtonDown("Jump")&&isGrounded)
            {
                moveMent.y = jumpForce;
            }

            moveMent.y += Physics.gravity.y * Time.deltaTime * gravityModifier;


            // 6. 최종적으로 CharacterController를 사용하여 캐릭터를 움직인다.
            if (moveAmount > 0)                                             // moveInput이 0일때 moveDirection이 0이된다.
            {
                targetRotation = Quaternion.LookRotation(moveDirection);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smoothRotation * Time.deltaTime);
            
            playerCCon.Move(moveMent * Time.deltaTime);
        }

        private void GroundCheck()  // player가 땅인지 아닌지 판별하는 함수
        {
            isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius, groundLayer);    // 레이어가 ground인 물리충돌이 발생하면 크기가 groundcheckradius이고
                                                                                                            // 시작점이checkpoint인 물리충돌이 발생하면 true, 아니면 false
        }
        private void OnDrawGizmos() // 눈에 안보이는 땅체크 함수를 가시화 하기 위해 선언
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius);
        }
    }
}
