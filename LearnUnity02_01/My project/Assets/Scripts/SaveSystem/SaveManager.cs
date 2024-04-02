using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;  // �̱��� ������ ���� �ν��Ͻ� ����

    // Singleton Pattern : �ν��Ͻ��� ���� ��� �ν��Ͻ��� �����ϰ�, �̹� ������ ��� ��ȯ

    GameData gameData;
    DataHandler dataHandler;

    List<ISaveManager> saveManagers;

    [Header("������ ������ ���� ����")]
    public string fileName;
    public SaveGameSlot currentSlot;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;   
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataHandler = new DataHandler(Application.persistentDataPath, fileName);
        saveManagers = FindAllSaveManagers();
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
    public void ChangeSaveFileNameBySelectSlot(SaveGameSlot slot)
    {
        currentSlot = slot;

        fileName += slot.ToString() + ".txt";
    }
}
