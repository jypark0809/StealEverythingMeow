using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Define;

[Serializable]
public class GameData
{
    public int Jelly;
    public int Gold;
    public int Dia;

    // 재료
    public int Wood = 9999;
    public int Cotton = 9999;
    public int Stone = 9999;

    public int RoomLevel ;
    public int[] MaxFurniture = new int[11]; //추후 수정

    public int SoomLevel = 1;
    public List<string> Emotion = new List<string>(); //배열 갱신
    public List<float> EmotionTime = new List<float>(); //추후배열로 다시봐보기

    public bool BGMOn = true;
    public bool EffectSoundOn = true;


    public GameData()
    {
        Jelly = 5;

        //초기 감정 배열선언
        Emotion.Add("Sleep3");
        Emotion.Add("Sleep2");
        Emotion.Add("Blink");
        Emotion.Add("Ennui");

        EmotionTime.Add(10f);
        EmotionTime.Add(10f);
        EmotionTime.Add(5f);
        EmotionTime.Add(10f);
    }
}

public class GameManagerEx
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    #region Option
    public bool BGMOn
    {
        get { return _gameData.BGMOn; }
        set { _gameData.BGMOn = value; }
    }

    public bool EffectSoundOn
    {
        get { return _gameData.EffectSoundOn; }
        set { _gameData.EffectSoundOn = value; }
    }
    #endregion

    public bool IsLoaded = false;

    public void Init()
    {
        _path = Application.persistentDataPath + "/SaveData.json";
        if (LoadGame())
            return;

        IsLoaded = true;

        SaveGame();
    }

    #region Save&Load
    string _path;

    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
        File.WriteAllText(_path, jsonStr);
    }

    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(fileStr);
        if (data != null)
            Managers.Game.SaveData = data;

        IsLoaded = true;
        return true;
    }
    #endregion
}
