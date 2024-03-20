using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSetting
{
    [RequireComponent(typeof(CharacterController))]

    public class PlayerControler : MonoBehaviour
    {
        private CharacterController playerCCon;
        [SerializeField] private float moveSpeed = 1.0f;
        [SerializeField] ThirdCamCamera thirdCam;
        
        [SerializeField] private float smoothRotation = 5.0f;

        Quaternion targetRotation; // 키보드 입력을 하지 않았을때 카메라 방향으로 회전하기 위한 회전각도를 저장하는 변수

        private void Awake()
        {
            playerCCon = GetComponent<CharacterController>();
        }
        void Start()
        {

        }
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 moveDir = new Vector3(horizontal, 0, vertical);
            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));  // 키보드로 상하 좌우키 한개만 입력하면 0보다 큰 값을 moveAmount에 저장한다.
            // 힘을 줘서 이동한다.
            // playerRigid.AddForce(moveDir * moveSpeed);
            // rigid.AddForce는 time.deltatime이 함수내에 포함되어 있다.

            // 위치를 변화시킨다.
            // 현재위치 + 속도 + 가속도 = 이동 거리
            // transform.position += moveDir * moveSpeed * Time.deltaTime;

            Vector3 moveMent = thirdCam.camLookRotation * moveDir;
            if (moveAmount > 0)
            {
                targetRotation = Quaternion.LookRotation(moveMent);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smoothRotation * Time.deltaTime);
            
            playerCCon.Move(moveMent * moveSpeed * Time.deltaTime);
            // playerCCon.SimpleMove(moveDir * moveSpeed);
        }
    }
}
