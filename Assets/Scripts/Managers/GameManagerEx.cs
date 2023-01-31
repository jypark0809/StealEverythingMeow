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

    // Àç·á
    public int Wood = 1000;
    public int Cotton = 1000;
    public int Stone = 9999;

    public int RoomLevel = 1;
    public int[] MaxFurniture = new int[11] {0,0,0,0,0,0,0,0,0,0,0};

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
    // List<FurnitureData> _fList = new List<FurnitureData>();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }
    // public List<FurnitureData> FurntureList { get { return _fList; } }

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
        // _fPath = Application.persistentDataPath + "/FurnitureSaveData.json";
        if (LoadGame())
            return;

        IsLoaded = true;

        SaveGame();
    }

    #region Save&Load
    string _path;
    // string _fPath;

    public void SaveGame()
    {
        string gameDataStr = JsonUtility.ToJson(Managers.Game.SaveData);
        // string fDataStr = JsonUtility.ToJson(FurntureList);
        File.WriteAllText(_path, gameDataStr);
        // File.WriteAllText(_fPath, fDataStr);
    }

    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(fileStr);
        if (data != null)
            Managers.Game.SaveData = data;

        // string fDataStr = File.ReadAllText(_fPath);


        IsLoaded = true;
        return true;
    }
    #endregion
}
