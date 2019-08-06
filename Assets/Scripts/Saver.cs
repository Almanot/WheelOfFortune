using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Saver
{
    public static void SaveTheProgress()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        Progress progress = new Progress();

        formatter.Serialize(stream, progress);
        stream.Close();
    }

    public static Progress LoadTheProgress()
    {
        string path = Application.persistentDataPath + "/score.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Progress progress = formatter.Deserialize(stream) as Progress;
            stream.Close();
            return progress;
        }
        else
        {
            Debug.LogError("save not found");
            return null;
        }
    }
}
