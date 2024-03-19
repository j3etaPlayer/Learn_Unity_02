using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCamera : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] Transform playerTf;

    private void Awake()
    {
        // 플레이어가 카메라를 보는 방향과 크기를 더하면 => 카메라의 위치
        offset = transform.position - playerTf.position;

    }
    void Start()
    {
        
    }
    // playerController에서 update에서 이동시키고, 이후 카메라가 lateUpdate로 플레이어를 따라 움직이게 한다.
    void LateUpdate()
    {
        // 카메라의 위치 = 플레이어가 이동한 위치 + 카메라와 플레이어가 고정되어야할 방향과 거리
        transform.position = playerTf.position + offset;
        // 플레이어가 이동함에 따라 변화한 offset을 다시 갱신한다.
        offset = transform.position - playerTf.position;
    }
}
