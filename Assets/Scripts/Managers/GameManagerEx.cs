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
    public int Gold = 999999;
    public int Dia = 999999;

    // 재료
    public int Wood = 1000;
    public int Cotton = 1000;
    public int Stone = 9999;

    public int RoomLevel = 1;
    public int[] MaxFurniture = new int[11] {0,0,0,0,0,0,0,0,0,0,0}; //추후 수정

    public int SoomLevel = 0;
    public bool[] Emotion = new bool[Define.MOTION_COUNT];

    public bool IsRoomOpen;
    public bool IsSoomUp = false;

    public bool BGMOn = true;
    public bool EffectSoundOn = true;

    public bool firstExecution = true;

    public GameData()
    {
        Jelly = 5;
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
