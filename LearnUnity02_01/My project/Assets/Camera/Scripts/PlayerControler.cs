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

        Quaternion targetRotation; // Ű���� �Է��� ���� �ʾ����� ī�޶� �������� ȸ���ϱ� ���� ȸ�������� �����ϴ� ����

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
            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));  // Ű����� ���� �¿�Ű �Ѱ��� �Է��ϸ� 0���� ū ���� moveAmount�� �����Ѵ�.
            // ���� �༭ �̵��Ѵ�.
            // playerRigid.AddForce(moveDir * moveSpeed);
            // rigid.AddForce�� time.deltatime�� �Լ����� ���ԵǾ� �ִ�.

            // ��ġ�� ��ȭ��Ų��.
            // ������ġ + �ӵ� + ���ӵ� = �̵� �Ÿ�
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
