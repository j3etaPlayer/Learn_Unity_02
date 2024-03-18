using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeFuction : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake ½ÇÇà µÊ");
    }

    void Start()
    {
        Debug.Log("Start ½ÇÇà µÊ");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable ½ÇÇà µÊ");
    }
}
