using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataHandler
{
    private string directoryPath = string.Empty;
    private string fileName = string.Empty;

    public DataHandler(string directoryPath, string fileName)
    {
        this.directoryPath = directoryPath;
        this.fileName = fileName;
    }

    public void DataSave(GameData data)
    {
        string dataPath = Path.Combine(directoryPath, fileName);
        try
        {
            Debug.Log(dataPath);
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));

            string serializData = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(dataPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(serializData);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("에러발생" + dataPath + "\n" + e);
        }
    }
    public GameData DataLoad()
    {
        string datapath = Path.Combine(directoryPath, fileName);
        GameData loaddata = null;

        if(File.Exists(datapath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(datapath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loaddata = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("에러 발생" + datapath + "\n" + e);
            }

        }

        return loaddata;
    }
}
