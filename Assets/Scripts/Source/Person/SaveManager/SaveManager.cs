using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    private static IFormatter _formatter;

    static SaveManager()
    {
        _formatter = new BinaryFormatter();
    }

    public static void Save(object data)
    {
        string fileName = data.GetType().ToString();
        string path = Path.Combine(Application.persistentDataPath, fileName);

        using(FileStream f = new FileStream(path, FileMode.Create))
        {
            _formatter.Serialize(f, data);
        }
    }

    public static object Load(Type type)
    {
        string fileName = type.ToString();
        string path = Path.Combine(Application.persistentDataPath, fileName);
        object result;

        using(FileStream f = new FileStream(path, FileMode.Open))
        {
            result = _formatter.Deserialize(f);
        }

        return result;
    }
}
