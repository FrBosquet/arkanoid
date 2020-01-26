using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SaveData
{
  public List<Score> scores;

  public SaveData(List<Score> data)
  {
    scores = data;
  }
}

public class DataManager : MonoBehaviour
{
  public static string path = Application.persistentDataPath + "/scores.save";

  public static List<Score> GetSaveData()
  {
    if (File.Exists(path))
    {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(path, FileMode.Open);
      SaveData save = (SaveData)bf.Deserialize(file);
      file.Close();

      return save.scores;
    }
    else
    {
      return new List<Score>();
    }
  }

  public static void SaveData(List<Score> scores)
  {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(path);
    bf.Serialize(file, new SaveData(scores));
    file.Close();
  }
}