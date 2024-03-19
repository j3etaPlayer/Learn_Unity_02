using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCamera : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] Transform playerTf;

    private void Awake()
    {
        // �÷��̾ ī�޶� ���� ����� ũ�⸦ ���ϸ� => ī�޶��� ��ġ
        offset = transform.position - playerTf.position;

    }
    void Start()
    {
        
    }
    // playerController���� update���� �̵���Ű��, ���� ī�޶� lateUpdate�� �÷��̾ ���� �����̰� �Ѵ�.
    void LateUpdate()
    {
        // ī�޶��� ��ġ = �÷��̾ �̵��� ��ġ + ī�޶�� �÷��̾ �����Ǿ���� ����� �Ÿ�
        transform.position = playerTf.position + offset;
        // �÷��̾ �̵��Կ� ���� ��ȭ�� offset�� �ٽ� �����Ѵ�.
        offset = transform.position - playerTf.position;
    }
}
