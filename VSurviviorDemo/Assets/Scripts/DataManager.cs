using UnityEngine;
using System.IO;

public static class DataManager
{
    private static string dataPath => $"{Application.persistentDataPath}/playerData.json";

    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return new PlayerData(); // Return default data if file doesn't exist
    }
}
