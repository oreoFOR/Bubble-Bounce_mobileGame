using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SerializationManager
{
    public static void Save(string saveName, BubbleLayout layout)
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        string path = Application.persistentDataPath + "/saves/" + saveName;
        FileStream file = File.Create(path);
        SaveData data = new SaveData(layout);
        formatter.Serialize(file, data);
        file.Close();
    }
    public static SaveData Load(string _path)
    {
        string path = Application.persistentDataPath + "/saves/" + _path;
        if (!File.Exists(path))
        {
            Debug.Log("null");
            return null;
        }
        else
        {
            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            try
            {
                SaveData save = formatter.Deserialize(file) as SaveData;
                file.Close();
                return save;
            }
            catch
            {
                Debug.LogErrorFormat("Failed to load coz wrong path passed in");
                file.Close();
                return null;
            }
        }
    }

    private static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();

        Vector2SerializationSurrogate v2Surrogate = new Vector2SerializationSurrogate();

        selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), v2Surrogate);
        formatter.SurrogateSelector = selector;

        return formatter;
    }
}

