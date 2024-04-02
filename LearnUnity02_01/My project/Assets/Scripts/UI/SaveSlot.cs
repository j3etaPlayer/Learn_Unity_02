using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public enum SaveGameSlot
{
    _00,
    _01,
    _02,
    _03,
    NO_Slot
}

public class SaveSlot : MonoBehaviour
{
    [Header("���� ���� ����")]
    public SaveGameSlot saveGameSlot;
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playTime;
    private string userName;

    [Header("�÷��� �ð� ����")]
    private float timeValue;
    private int min;
    private int sec;

    [Header("������ �ڵ鷯")]
    private DataHandler dataHandler;
    private GameData gameData;

    private void OnEnable()
    {
        // �ε� ��ư �׷��� Ȱ��ȭ �Ǿ����� �����Ű�� �Լ�
        // ������ �ڵ鷯�� ����Ͽ� �ܺο� ����� ������ �����ϸ� �����͸� �����ϰ� �׷��� ������ �⺻���� ����Ѵ�.

        LoadSavedSlotData();
    }

    private void LoadSavedSlotData()
    {
        string mySlotName = SaveManager.Instance.fileName + saveGameSlot.ToString() + ".txt";
        dataHandler = new DataHandler(Application.persistentDataPath, mySlotName);

        if (dataHandler.CheckFileExists(Application.persistentDataPath, mySlotName))
        {
            gameData = dataHandler.DataLoad();
            LoadData();
        }
    }
    public void LoadGameData()
    {
        SaveManager.Instance.ChangeSaveFileNameBySelectSlot(saveGameSlot);

        LoadingUI.LoadScene("CameraSetting");
    }
    public void DeleteGameData()
    {
        dataHandler.DataDelete();

        playerName.text = "NO Data";
        playTime.text = "00:00";

        gameObject.SetActive(false);
    }

    private void LoadData()
    {
        playerName.text = gameData.playerName;
        if (userName == "")
        {
            playerName.text = "�̸��� ����";
        }
        timeValue = gameData.timeValue;
        min = (int)timeValue / 60;
        sec = (int)timeValue % 60;

        playTime.text = string.Format("{0:D2} : {1:D2}", min, sec);
    }

    private void Reset()
    {
        playerName = transform.Find("Player Name").GetComponent<TMP_Text>();
        playTime = transform.Find("Play Time Text").GetComponent<TMP_Text>();
    }
}
