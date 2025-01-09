using Newtonsoft.Json;
using System.IO;
using UnityEngine;


public class SavingSystem
{
    private static string filePath = Application.persistentDataPath + "/gameData.json";

    public static void Save(string key, float value)
    {
        GameData gameData = new GameData(key, value);

        string jsonData = JsonConvert.SerializeObject(gameData);
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Saved game data: " + jsonData);
    }

    public static GameData Load()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            GameData loadedData = JsonConvert.DeserializeObject<GameData>(jsonData);

            Debug.Log("Loaded game data: EnemyHp " + loadedData.data["EnemyHp"]);

            return loadedData;
        }
        else
        {
            Debug.LogWarning("No save file found");
            return new GameData();
        }
    }
}
