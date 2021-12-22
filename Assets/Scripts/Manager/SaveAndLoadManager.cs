using System.IO;
using UnityEngine;

public static class SaveAndLoadManager
{
    private static string fileName = "Save";
    private static string path = Path.Combine(Application.persistentDataPath, fileName);

    public static void Save(GameData gameData)
    {
        string data = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(path, data);
    }

    public static GameData Load()
    {
        if (ExistsPath())
        {
            string data = File.ReadAllText(path);
            GameData gameData = JsonUtility.FromJson<GameData>(data);

            if (gameData != null)
            {
                return gameData;
            }
        }

        return new GameData { isStart = false };
    }

    public static bool ExistsPath()
    {
        return File.Exists(path);
    }

    public static void DeleteFile()
    {
        File.Delete(path);
    }
}
