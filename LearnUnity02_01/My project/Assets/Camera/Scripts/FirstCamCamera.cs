using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstViewCamera : MonoBehaviour
{
    [SerializeField] Transform viewPort;
    [SerializeField] private bool inverseLook;

    // 카메라 회전 제어를 위한 변수
    [SerializeField] private float mouseSensitvity = 1.0f;
    [SerializeField] private int limitAngle = 60;

    private float verticalRot;              // 오일러 회전 수치를 저장해두기 위한 값
    private Vector2 mouseInput;

    void Start()
    {
        // 마우스 커서를 제한 하는 파트
        // 메뉴, 옵션 버튼을 클릭 시 마우스 버튼이 보이게 한다.
        Cursor.visible = false;
        // 마우스가 게임 밖으로 나가지 않게 lockmode 옵션을 바꿀수 있다.
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {
        float inversevalue = inverseLook ? -1f : 1f;
        float rotationX = Input.GetAxisRaw("Mouse X");
        float rotationY = Input.GetAxisRaw("Mouse Y");

        mouseInput = new Vector2(rotationX, rotationY) * mouseSensitvity;

        // 좌우 회전
        // Quaternion.Euler 함수 : 매개변수로 x, y, z 각축에 0 ~ 360도의 회전수치를 입력하면 그 수치만큼 회전한 쿼터니언 값으로 변환해준다.
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                                transform.rotation.eulerAngles.y + mouseInput.x,
                                                transform.rotation.eulerAngles.z);

        // 상하 회전
        verticalRot -= mouseInput.y;
        verticalRot = Mathf.Clamp(verticalRot, -limitAngle, limitAngle);

        viewPort.rotation = Quaternion.Euler(verticalRot,
                                                viewPort.rotation.eulerAngles.y,
                                                viewPort.rotation.eulerAngles.z);
    }
}
