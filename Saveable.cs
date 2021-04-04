using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saveable
{
    protected string fileName;

    public Saveable(string fileName) 
    {
        this.fileName = fileName + ".json";
    }

    public static string GetPath(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }

    public virtual void Save()
    {
        string jsonString = JsonUtility.ToJson(this, true);
        File.WriteAllText(GetPath(fileName), jsonString);
    }

    public virtual void Load()
    {
        string path = GetPath(fileName);

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonString, this);
        }
        else
        {
            Debug.LogWarning("file is not exsists");
            File.Create(path);
        }

    }
}
