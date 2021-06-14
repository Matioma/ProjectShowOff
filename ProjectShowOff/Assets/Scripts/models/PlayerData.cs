
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



[System.Serializable]
public class PlayerData
{
    public string Name;
    public int trashCollectected;


    public void AddTrash(int amount) {
        trashCollectected += amount;
    }


    public static PlayerData Load(string filePath) {
        if (!File.Exists(filePath))
        {
            Debug.LogErrorFormat("ReadFromFile({0}) -- file not found, returning new object", filePath);
            return new PlayerData();
        }else
        {
            // If the file does exist then read the entire file to a string.
            string contents = File.ReadAllText(filePath);

            // If debug is on then tell us the file we read and its contents.
            //if (DEBUG_ON)
            //    Debug.LogFormat("ReadFromFile({0})\ncontents:\n{1}", filePath, contents);

            // If it happens that the file is somehow empty then tell us and return a new SaveData object.
            if (string.IsNullOrEmpty(contents))
            {
                Debug.LogErrorFormat("File: '{0}' is empty. Returning default SaveData");
                return new PlayerData();
            }

            // Otherwise we can just use JsonUtility to convert the string to a new SaveData object.
            return JsonUtility.FromJson<PlayerData>(contents);
        }
    }

    public void SavePlayerData(string path) {
        string json = JsonUtility.ToJson(this, true);

        File.WriteAllText(path, json);

    }

}
