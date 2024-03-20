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
    
    // 오일러 회전 수치를 저장해두기 위한 값
    private float verticalRot; 
    
    private Vector2 mouseInput;

    // 1인칭 카메라를 플레이어의 자식으로 귀속하지않고
    // 플레이어를 따라오게 하기 위해
    // 카메라를 변수로 가져온다.

    // 1인칭 카메라 게임오브젝트를 사용하기 위한 변수
    [SerializeField] private Camera firstCam;

    private void Awake()
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
        float rotationY = Input.GetAxisRaw("Mouse Y") * inversevalue;

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
    private void LateUpdate()
    {
        // 1인칭 카메라의 회전역할을 하는 viewPort position 일치시킨다.
        firstCam.transform.position = viewPort.position;
        // 1인칭 카메라의 회전과 이동역할을 하는 viewPort rotation 일치시킨다.
        firstCam.transform.rotation = viewPort.rotation;

        // 선형보간법으로 회전을 부드럽게 적용시켜보기
    }
}
