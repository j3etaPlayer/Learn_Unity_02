using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSetting
{
    [RequireComponent(typeof(CharacterController))]

    public class PlayerControler : MonoBehaviour
    {
        [Header("�÷��̾� �Է� ���� ����")]
        [SerializeField] private float runSpeed;        // �÷��̾� �޸��� �ӵ�
        [SerializeField] private float moveSpeed;       // �÷��̾� �⺻ �ӵ�
        [SerializeField] private float jumpForce;       // �÷��̾� ������

        private CharacterController playerCCon;         // rigidbody ��� character�� ���� �浹��ɰ� �̵��� ���� ������Ʈ
                                                        // RequireComponent�� ������ ������ �ȴ�.

        [Header("ī�޶� ���� ����")]
        [SerializeField] ThirdCamCamera thirdCam;               // 3��Ī ī�޶� ��Ʈ�ѷ��� �ִ� ���� ������Ʈ
        [SerializeField] private float smoothRotation = 5.0f;   // ī�޶��� �ڿ������� ȸ���� ���� ����ġ
        Quaternion targetRotation;                              // Ű���� �Է��� ���� �ʾ�����
                                                                // ī�޶� �������� ȸ���ϱ� ���� ȸ�������� �����ϴ� ����

        [Header("���� ���� ����")]
        [SerializeField] private float gravityModifier;
        [SerializeField] private Vector3 groundCheckPoint;    // ���� �Ǻ��ϱ� ���� üũ����Ʈ
        [SerializeField] private float groundCheckRadius;       // �� üũ�ϴ� ���� ũ�� ������
        [SerializeField] private LayerMask groundLayer;         // üũ�� ���̾ ������ �Ǻ��ϴ� ����
        private bool isGrounded;                                // true�� ������ ����, false�� ���� ����


        private float activeMoveSpeed;                          // ������ �÷��̾ �̵��� �ӷ��� ������ ����
        private Vector3 moveMent;                               // �÷��̾ �����̴� ����� �Ÿ��� ���� Vector��

        void Start()
        {
            playerCCon = GetComponent<CharacterController>();
        }

        void Update()
        {
            // 1. Input Ŭ������ �̿��Ͽ� Ű���� �Է��� ����
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Ű���� Input�� �Է� ���¤� Ȯ���ϱ� ���� ���� ����
            Vector3 moveInput = new Vector3(horizontal, 0, vertical);                       // Ű���� �Է°��� �����ϴ� ����
            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));  // Ű����� ���� �¿�Ű �Ѱ��� �Է��ϸ� 0���� ū ���� moveAmount�� �����Ѵ�.

            // ���� �༭ �̵��Ѵ�.
            // playerRigid.AddForce(moveDir * moveSpeed);
            // rigid.AddForce�� time.deltatime�� �Լ����� ���ԵǾ� �ִ�.

            // ��ġ�� ��ȭ��Ų��.
            // ������ġ + �ӵ� + ���ӵ� = �̵� �Ÿ�
            // transform.position += moveDir * moveSpeed * Time.deltaTime;

            Vector3 moveDirection = thirdCam.camLookRotation * moveInput;   // �÷��̾ �̵��� ������ �����ϴ� ���� moveDirection

            // 4. �÷��̾��� �̵��ӵ��� �ٸ������ִ� �ڵ�(�޸���)
            if (Input.GetKey(KeyCode.LeftShift))                             // key down : ������ �ѹ��� ����, key : key�� ������������ ���
                activeMoveSpeed = runSpeed;
            else
                activeMoveSpeed = moveSpeed;

            // 5. ������ �ϱ� ���� ���� (�߷��� �ʿ��ϴ�)
            float yValue = moveMent.y;                                      // ���������ִ� y�� ũ�⸦ ����
            moveMent = moveDirection * activeMoveSpeed;                     // ���� ��ǥ�� x, 0, z ���� ���� ����, �������� �ִ� ���� 0���� �ʱ�ȭ
            moveMent.y = yValue;                                            // �߷¿� ���� ��� �޵��� �Ҿ���� ������ �ٽ� �ҷ��´�.

            GroundCheck();

            if (isGrounded)
            {
                moveMent.y = 0;
                Debug.Log("���� �ֽ��ϴ�.");
            }
            else
                Debug.Log("�����Դϴ�");


            // ����Ű�� �Է��Ͽ� ���� ����
            if (Input.GetButtonDown("Jump")&&isGrounded)
            {
                moveMent.y = jumpForce;
            }

            moveMent.y += Physics.gravity.y * Time.deltaTime * gravityModifier;


            // 6. ���������� CharacterController�� ����Ͽ� ĳ���͸� �����δ�.
            if (moveAmount > 0)                                             // moveInput�� 0�϶� moveDirection�� 0�̵ȴ�.
            {
                targetRotation = Quaternion.LookRotation(moveDirection);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smoothRotation * Time.deltaTime);
            
            playerCCon.Move(moveMent * Time.deltaTime);
        }

        private void GroundCheck()  // player�� ������ �ƴ��� �Ǻ��ϴ� �Լ�
        {
            isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius, groundLayer);    // ���̾ ground�� �����浹�� �߻��ϸ� ũ�Ⱑ groundcheckradius�̰�
                                                                                                            // ��������checkpoint�� �����浹�� �߻��ϸ� true, �ƴϸ� false
        }
        private void OnDrawGizmos() // ���� �Ⱥ��̴� ��üũ �Լ��� ����ȭ �ϱ� ���� ����
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius);
        }
    }
}
