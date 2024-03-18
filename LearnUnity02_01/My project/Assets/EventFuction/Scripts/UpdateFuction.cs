using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFuction : MonoBehaviour
{
    void Update()
    {
        Debug.Log("Update�� ȣ����");
    }
    private void LateUpdate()
    {
        Debug.Log("LateUpdate�� ȣ����");
    }
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate�� ȣ����");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(0, 0, 5), 10f);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit ȣ����");
    }
    private void OnDestroy()
    {
        Debug.Log("OnDestroy ȣ����");
    }
    private void OnDisable()
    {
        Debug.Log("OnDisable ȣ����");
    }
}
