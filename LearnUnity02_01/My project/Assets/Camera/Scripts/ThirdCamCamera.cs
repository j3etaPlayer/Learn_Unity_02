using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdCamCamera : MonoBehaviour
{
    [Header("ī�޶� ���� ����")]
    [SerializeField] private Transform target;          // ī�޶� ���� ���
    [SerializeField] private float camDistance;         // ���� ī�޶���� �Ÿ�
    [SerializeField] private float rotSpeed;            // ī�޶� ȸ���ϴ� �ӵ�
    [SerializeField] private int limitAngle;            // �ִ� ȸ�� ����

    [SerializeField] private bool inverseX;             // ���콺 ���Ʒ� ����üũ
    [SerializeField] private bool inverseY;             // ���콺 �¿� ����üũ

    float rotationX;
    float rotationY;

    public Quaternion camLookRotation => Quaternion.Euler(0, rotationY, 0);

    void Update()
    {
        float invertXValue = inverseX ? -1 : 1;
        float invertYValue = inverseY ? -1 : 1;

        // ���콺�� �Է� ���� �޾ƿ´�.
        // ���콺�� ���Ʒ��� �����϶����� �����̼� x�� ���� ����ȴ�.
        rotationX -= Input.GetAxis("Mouse Y") * invertYValue * rotSpeed;        // ���� ȸ���� ���� ���콺 �Է� ��
        rotationX = Mathf.Clamp(rotationX, -limitAngle, 90);

        rotationY += Input.GetAxis("Mouse X") * invertXValue * rotSpeed;        // �¿� ȸ���� ���� ���콺 �Է� ��

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);         // ����, �¿� ȸ���� ���� ���Ϸ� ��ġ�� ������ ȸ������ targetRotation�� ����
        transform.rotation = targetRotation;

        // ī�޶� �÷��̾ �i�Ƽ� �̵��ϴ� ����
        Vector3 focusPosition = target.position;
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, camDistance); // �ڿ� �ִ� Vector���� focusPosition�� �ٶ󺸴� ���� ���͸� ��ȯ�Ѵ�.
    }
}
