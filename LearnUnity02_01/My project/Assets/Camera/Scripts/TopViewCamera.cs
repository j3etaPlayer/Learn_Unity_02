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
        // �÷��̾ ī�޶� ���� ����� ũ�⸦ ���ϸ� => ī�޶��� ��ġ
        offset = transform.position - playerTf.position;
    }
    // playerController���� update���� �̵���Ű��, ���� ī�޶� lateUpdate�� �÷��̾ ���� �����̰� �Ѵ�.
    void LateUpdate()
    {
        #region ī�޶� �÷��̾�� ���� �ӵ��� �̵�
        // ī�޶��� ��ġ = �÷��̾ �̵��� ��ġ + ī�޶�� �÷��̾ �����Ǿ���� ����� �Ÿ�
        // transform.position = playerTf.position + offset;
        // �÷��̾ �̵��Կ� ���� ��ȭ�� offset�� �ٽ� �����Ѵ�.
        // offset = transform.position - playerTf.position;
        #endregion

        // ���� ������ ����� ī�޶� �̵�
        // �� ���� ���� �־����� �� 0 ~ 1 Percent�� �� ������ ���� �����ϴ� ���

        #region ������������ ī�޶� �̵�
        // Update�ɶ� ���� ī�޶� ���������� �����ؾ��� ��ġ
        Vector3 targetCamPos = playerTf.position + offset;
        // Vector ����� ũ�⸦ ���� �����͸� ������ �ִµ� ������ ������ ä�� ũ�⸸ ���ݾ� ��ȭ
        // 1���Ű����� : ������ġ, 2���Ű����� : ���� ������ ��ġ, 3���Ű����� : a�� b�� �Ÿ� ������ percent�� ��Ÿ�� ��
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothValue * Time.deltaTime); 
        #endregion
    }
}
