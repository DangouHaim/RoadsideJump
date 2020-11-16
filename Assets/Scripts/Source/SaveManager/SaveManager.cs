using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    private static string _folder = "Saves";
    private static string _path = "";
    private static IFormatter _formatter;

    static SaveManager()
    {
        _path = Path.Combine(Application.persistentDataPath, _folder);
        if(!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }

        _formatter = new BinaryFormatter();
    }

    public static void Save(object data)
    {
        string fileName = data.GetType().ToString();
        string path = Path.Combine(_path, fileName);

        using(FileStream f = new FileStream(path, FileMode.Create))
        {
            try
            {
                _formatter.Serialize(f, data);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + "; " + ex.StackTrace);
            }
        }
    }

    public static object Load(Type type)
    {
        string fileName = type.ToString();
        string path = Path.Combine(_path, fileName);
        object result;

        if(!File.Exists(path))
        {
            return null;
        }

        using(FileStream f = new FileStream(path, FileMode.OpenOrCreate))
        {
            if(f.Length == 0)
            {
                return null;
            }
            try
            {
                result = _formatter.Deserialize(f);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + "; " + ex.StackTrace);
                return null;
            }
        }

        return result;
    }
}
