using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSaveManager;

    public SaveData saveData;
    public static bool isFirstTimePlaying = true;
    public bool isDataLoaded;
    public static bool isFirstSave = false;

    private void Awake()
    {

        gameSaveManager = this;
        LoadGame();
    }


    public void SaveGame()
    {

        string dataPath = Application.persistentDataPath;

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));
        FileStream fileStream = new FileStream(dataPath + "/flyingSailors.saveData", FileMode.Create);
        //print(fileStream);
        xmlSerializer.Serialize(fileStream, saveData);
        fileStream.Close();
        //isFirstSave = true;
    }

    public void LoadGame()
    {

        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/flyingSailors.saveData"))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));
            FileStream fileStream = new FileStream(dataPath + "/flyingSailors.saveData", FileMode.Open);
            saveData = xmlSerializer.Deserialize(fileStream) as SaveData;

            fileStream.Close();

            isDataLoaded = true;
            isFirstTimePlaying = false;
        }

    }

    public void DeleteGameData()
    {

        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/flyingSailors.saveData"))
        {
            File.Delete(dataPath + "/flyingSailors.saveData");
            isDataLoaded = false;
            isFirstTimePlaying = true;
        }
    }
}

[System.Serializable]
public class SaveData
{
    /*[Header("OptionsMenuData")]
    public float volume = 0.5f;
    public bool sfx = true;

*/
    [Header("InGameData")]
    public int trackSceneIndex;
    public int trackBoatCount;


   
    
}