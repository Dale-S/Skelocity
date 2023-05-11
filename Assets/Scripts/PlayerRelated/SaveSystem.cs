using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSystem : MonoBehaviour
{
    public static void SavePlayer(LevelScript LS, SkillScript SS)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(LS, SS);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(LevelScript LS, SkillScript SS)
    {
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Open);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data = new PlayerData(LS, SS);
            formatter.Serialize(stream, data);
            stream.Close();
            return data;
        }
    }
}
