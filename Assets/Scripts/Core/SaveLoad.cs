using System.IO;
using UnityEngine;

public static class SaveLoad
{
    public static void SaveData<T>(T data, string path, string filename)
    {
        string fullPath = Application.persistentDataPath + "/" + path + "/";
        bool isExist = Directory.Exists(fullPath);
        if (isExist == false)
        {
            Directory.CreateDirectory(fullPath);
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(fullPath + filename + ".json", json);
    }

    public static T LoadData<T>(string path, string filename)
    {
        string fullPath = Application.persistentDataPath + "/" + path + "/" + filename + ".json";
        if (File.Exists(fullPath))
        {
            string textJson = File.ReadAllText(fullPath);
            var obj = JsonUtility.FromJson<T>(textJson);
            return obj;
        }
        else
        {
            return default;
        }
    }

    public static void DeleteData(string path, string filename)
    {
        string fullPath = Application.persistentDataPath + "/" + path + "/" + filename + ".json";
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    public static void PathData(string path, string filename)
    {
        string fullPath = Application.persistentDataPath + "/" + path + "/" + filename + ".json";
        Debug.Log(fullPath);
    }
}