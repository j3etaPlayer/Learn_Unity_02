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
    [Header("저장 슬롯 정보")]
    public SaveGameSlot saveGameSlot;
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playTime;
    private string userName;

    [Header("플레이 시간 정보")]
    private float timeValue;
    private int min;
    private int sec;

    [Header("데이터 핸들러")]
    private DataHandler dataHandler;
    private GameData gameData;

    private void OnEnable()
    {
        // 로드 버튼 그룹이 활성화 되었을때 실행시키는 함수
        // 데이터 핸들러를 사용하여 외부에 저장된 파일이 존재하면 데이터를 갱신하고 그렇지 않으면 기본값을 출력한다.

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
            playerName.text = "이름이 없음";
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
