using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;  // �̱��� ������ ���� �ν��Ͻ� ����

    // Singleton Pattern : �ν��Ͻ��� ���� ��� �ν��Ͻ��� �����ϰ�, �̹� ������ ��� ��ȯ

    GameData gameData;
    DataHandler dataHandler;

    List<ISaveManager> saveManagers;

    [Header("������ ������ ���� ����")]
    public string fileName;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);      // ���� ����Ǿ �ı����� �ʵ��� ���ִ� ����
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
        // ������ �����͸� �� �Լ��� ȣ���� �ڿ� GameData�� �����Ѵ�.

        foreach(var saveManager in  saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        // ������ gameData�� �ܺ� ������ �����Ų��.
        dataHandler.DataSave(gameData);

        Debug.Log("����Ǿ����ϴ�.");
    }
    public void LoadGame()
    {
        // �ܺο� ����� ���� �����͸� �ҷ��´�.

        gameData = dataHandler.DataLoad();

        if (gameData == null)
            NewGame();

        // a���� �ܺο� ���� �����Ͱ� ���ٸ� ���ο� ������ �ҷ��´�

        // ���ӿ� �ʿ��� Ŭ������ ���� �����͸� �����Ѵ�.
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
