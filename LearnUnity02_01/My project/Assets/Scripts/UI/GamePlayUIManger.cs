using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIManger : MonoBehaviour
{
    [SerializeField] private GameObject UIgameSetting;

    private GameObject SettingPrefab;

    private bool isOpen = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;
            CallSettingUi(isOpen);
        }
    }
    private void CallSettingUi(bool isOpen)
    {
        UIgameSetting.SetActive(isOpen);
    }

}
