using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveScore(Scoremanager _scoreManager)
    {
        BinaryFormatter _formatter = new BinaryFormatter();
        string _path = Application.persistentDataPath + "/score.fun";

        using (FileStream _stream = new FileStream(_path, FileMode.Create))
        {
            GameData _data = new GameData(_scoreManager);

            _formatter.Serialize(_stream, _data);
        }
    }

    public static GameData loadScore()
    {
        string _path = Application.persistentDataPath + "/score.fun";
        if (File.Exists(_path))
        {
            BinaryFormatter _formatter = new BinaryFormatter();
            using (FileStream _stream = new FileStream(_path, FileMode.Open))
            {
                GameData _data = _formatter.Deserialize(_stream) as GameData;
                return _data;
            }
        }
        else
        {
            Debug.LogError("Save File not found in " + _path);
            return null;
        }
    }
}
