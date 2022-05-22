// Roman Baranov 22.05.2022

using System.IO;
using UnityEngine;

public static class SaveGameManager
{
    #region VARIABLES
    public static SaveData CurrentSaveData = new SaveData();

    public const string DIRECTORY = "/SaveData/";
    public const string FILE_NAME = "SaveGame.cf";

    #endregion

    #region PUBLIC Methods
    public static bool SaveGame()
    {
        string dir = Application.persistentDataPath + DIRECTORY;
        Debug.Log($"Save dir = {dir}");
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(CurrentSaveData, true);
        File.WriteAllText(dir + FILE_NAME, json);

        return true;
    }

    public static void LoadGame()
    {
        string dir = Application.persistentDataPath + DIRECTORY + FILE_NAME;
        SaveData tmpSaveData = new SaveData();

        if (!File.Exists(dir))
        {
            Debug.LogError("Save file does not exist");
            return;
        }

        string json = File.ReadAllText(dir);
        tmpSaveData = JsonUtility.FromJson<SaveData>(json);
        CurrentSaveData = tmpSaveData;
    }
    #endregion
}
