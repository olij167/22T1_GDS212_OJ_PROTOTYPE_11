using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(DucklingStats ducklingStats, TimeSO time)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveData.duck";

        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(ducklingStats, time);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData ()
    {
        string path = Application.persistentDataPath + "/SaveData.duck";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("No Save File Found in " + path);
            return null;
        }
    }
}
