using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstViewCamera : MonoBehaviour
{
    [SerializeField] Transform viewPort;
    [SerializeField] private bool inverseLook;

    // ī�޶� ȸ�� ��� ���� ����
    [SerializeField] private float mouseSensitvity = 1.0f;
    [SerializeField] private int limitAngle = 60;

    private float verticalRot;              // ���Ϸ� ȸ�� ��ġ�� �����صα� ���� ��
    private Vector2 mouseInput;

    void Start()
    {
        // ���콺 Ŀ���� ���� �ϴ� ��Ʈ
        // �޴�, �ɼ� ��ư�� Ŭ�� �� ���콺 ��ư�� ���̰� �Ѵ�.
        Cursor.visible = false;
        // ���콺�� ���� ������ ������ �ʰ� lockmode �ɼ��� �ٲܼ� �ִ�.
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {
        float inversevalue = inverseLook ? -1f : 1f;
        float rotationX = Input.GetAxisRaw("Mouse X");
        float rotationY = Input.GetAxisRaw("Mouse Y");

        mouseInput = new Vector2(rotationX, rotationY) * mouseSensitvity;

        // �¿� ȸ��
        // Quaternion.Euler �Լ� : �Ű������� x, y, z ���࿡ 0 ~ 360���� ȸ����ġ�� �Է��ϸ� �� ��ġ��ŭ ȸ���� ���ʹϾ� ������ ��ȯ���ش�.
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                                transform.rotation.eulerAngles.y + mouseInput.x,
                                                transform.rotation.eulerAngles.z);

        // ���� ȸ��
        verticalRot -= mouseInput.y;
        verticalRot = Mathf.Clamp(verticalRot, -limitAngle, limitAngle);

        viewPort.rotation = Quaternion.Euler(verticalRot,
                                                viewPort.rotation.eulerAngles.y,
                                                viewPort.rotation.eulerAngles.z);
    }
}
