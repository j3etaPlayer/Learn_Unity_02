using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeFuction : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake ���� ��");
    }

    void Start()
    {
        Debug.Log("Start ���� ��");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable ���� ��");
    }
}
