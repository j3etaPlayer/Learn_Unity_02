using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdCamCamera : MonoBehaviour
{
    [Header("카메라 제어 변수")]
    [SerializeField] private Transform target;          // 카메라가 찍을 대상
    [SerializeField] private float camDistance;         // 대상과 카메라와의 거리
    [SerializeField] private float rotSpeed;            // 카메라가 회전하는 속도
    [SerializeField] private int limitAngle;            // 최대 회전 각도

    [SerializeField] private bool inverseX;             // 마우스 위아래 반전체크
    [SerializeField] private bool inverseY;             // 마우스 좌우 반전체크

    float rotationX;
    float rotationY;

    public Quaternion camLookRotation => Quaternion.Euler(0, rotationY, 0);

    void Update()
    {
        float invertXValue = inverseX ? -1 : 1;
        float invertYValue = inverseY ? -1 : 1;

        // 마우스의 입력 값을 받아온다.
        // 마우스를 위아래로 움직일때마자 로테이션 x에 값이 저장된다.
        rotationX -= Input.GetAxis("Mouse Y") * invertYValue * rotSpeed;        // 상하 회전에 대한 마우스 입력 값
        rotationX = Mathf.Clamp(rotationX, -limitAngle, 90);

        rotationY += Input.GetAxis("Mouse X") * invertXValue * rotSpeed;        // 좌우 회전에 대한 마우스 입력 값

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);         // 상하, 좌우 회전에 대한 오일러 수치를 변경한 회전값을 targetRotation에 저장
        transform.rotation = targetRotation;

        // 카메라가 플레이어를 쫒아서 이동하는 로직
        Vector3 focusPosition = target.position;
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, camDistance); // 뒤에 있는 Vector에서 focusPosition을 바라보는 방향 벡터를 반환한다.
    }
}
