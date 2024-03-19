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
            // ���� �༭ �̵��Ѵ�.
            // playerRigid.AddForce(moveDir * moveSpeed * Time.deltaTime);

            // ��ġ�� ��ȭ��Ų��.
            // ������ġ + �ӵ� + ���ӵ� = �̵� �Ÿ�
            // transform.position += moveDir * moveSpeed * Time.deltaTime;
            playerCCon.Move(moveDir * moveSpeed * Time.deltaTime);
        }
    }
}
