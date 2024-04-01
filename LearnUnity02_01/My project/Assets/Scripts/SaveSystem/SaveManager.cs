using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;  // 싱글톤 패턴을 위한 인스턴스 변수

    // Singleton Pattern : 인스턴스가 없을 경우 인스턴스를 생성하고, 이미 존재할 경우 반환

    GameData gameData;
    DataHandler dataHandler;

    List<ISaveManager> saveManagers;

    [Header("저장할 데이터 변수 정보")]
    public string fileName;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);      // 씬이 변경되어도 파괴되지 않도록 해주는 설정
    }

    public void NewGame()
    {
        gameData = new GameData();
    }
    private void Start()
    {
        dataHandler = new DataHandler(Application.persistentDataPath, fileName);
        saveManagers = FindAllSaveManagers();
        LoadGame();

        LoadGame();
    }

    public void SaveGame()
    {
        // 저장할 데이터를 이 함수에 호출한 뒤에 GameData에 저장한다.

        foreach(var saveManager in  saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        // 저장할 gameData를 외부 폴더에 저장시킨다.
        dataHandler.DataSave(gameData);

        Debug.Log("저장되었습니다.");
    }
    public void LoadGame()
    {
        // 외부에 저장된 게임 데이터를 불러온다.

        gameData = dataHandler.DataLoad();

        if (gameData == null)
            NewGame();

        // a만약 외부에 게임 데이터가 없다면 새로운 게임을 불러온다

        // 게임에 필요한 클레스에 각각 데이터를 전달한다.
        foreach (var saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

}
