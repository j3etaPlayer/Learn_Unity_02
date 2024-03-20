using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCamera : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] Transform playerTf;
    [SerializeField] private float smoothValue = 5.0f;

    private void Awake()
    {
        // 플레이어가 카메라를 보는 방향과 크기를 더하면 => 카메라의 위치
        offset = transform.position - playerTf.position;
    }
    // playerController에서 update에서 이동시키고, 이후 카메라가 lateUpdate로 플레이어를 따라 움직이게 한다.
    void LateUpdate()
    {
        #region 카메라가 플레이어와 같은 속도로 이동
        // 카메라의 위치 = 플레이어가 이동한 위치 + 카메라와 플레이어가 고정되어야할 방향과 거리
        // transform.position = playerTf.position + offset;
        // 플레이어가 이동함에 따라 변화한 offset을 다시 갱신한다.
        // offset = transform.position - playerTf.position;
        #endregion

        // 선형 보간을 사용한 카메라 이동
        // 두 점의 값이 주어졌을 때 0 ~ 1 Percent로 그 사이의 값을 추정하는 방법

        #region 선형보간으로 카메라가 이동
        // Update될때 마다 카메라가 최종적으로 도착해야할 위치
        Vector3 targetCamPos = playerTf.position + offset;
        // Vector 방향과 크기를 가진 데이터를 가지고 있는데 방향은 유지한 채로 크기만 조금씩 변화
        // 1번매개변수 : 시작위치, 2번매개변수 : 최종 도착할 위치, 3번매개변수 : a와 b의 거리 비율을 percent로 나타낸 값
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothValue * Time.deltaTime); 
        #endregion
    }
}
