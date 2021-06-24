using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ScoreBoard
{
    public List<PlayerData> players;

    public static string filePath = "/Data/HighScore.data";

    public static ScoreBoard LoadScore(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogErrorFormat("ReadFromFile({0}) -- file not found, returning new object", filePath);
            return new ScoreBoard();
        }
        else
        {
            // If the file does exist then read the entire file to a string.
            string contents = File.ReadAllText(filePath);


            // If it happens that the file is somehow empty then tell us and return a new SaveData object.
            if (string.IsNullOrEmpty(contents))
            {
                Debug.LogErrorFormat("File: '{0}' is empty. Returning default SaveData");
                return new ScoreBoard();
            }

            // Otherwise we can just use JsonUtility to convert the string to a new SaveData object.
            return JsonUtility.FromJson<ScoreBoard>(contents);
        }
    }

    public void AddScore(PlayerData data) {
        if (players.Count < 10)
        {
            players.Add(data);
            players.Sort();
            return;
        }

        if (data.trashCollectected > players[players.Count - 1].trashCollectected) {
            players.Sort();
            players.RemoveAt(players.Count - 1);
            players.Add(data);
            players.Sort();
        }
    }

    public void SaveScore(string path)
    {
        string json = JsonUtility.ToJson(this, true);

        if (!File.Exists(path)) {
            File.Create(path);
        }
        File.WriteAllText(path, json);
    }
}
