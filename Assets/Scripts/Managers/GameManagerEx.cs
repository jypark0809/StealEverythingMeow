using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Define;

[Serializable]
public class GameData
{
    //재화
    public int Jelly;
    public int Gold = 80000;
    public int Dia = 90000;

    // 재료
    public int Wood = 1000;
    public int Cotton = 1000;
    public int Stone = 1000;

    // Furniture List
    public List<FurnitureData> FList = new List<FurnitureData>();

    public int[] MaxFurniture = new int[11] {0,0,0,0,0,0,0,0,0,0,0}; //추후 수정

    public bool[] Emotion = new bool[Define.MOTION_COUNT] {true, false, true , false, false, false, false, false, false, true, true, false, false, false, false, false };

    //공간
    public int SpaceLevel = 1;
    public int SoomLevel = 1;
    public bool IsRoomOpen = false;
    public bool IsSoomUp = false;


    public bool BGMOn = true;
    public bool EffectSoundOn = true;
    public bool firstExecution = true;


    //고양이 보유여부 [White, Black, Calico, Tabby, Grey]
    public bool[] CatHave = new bool[5] { true, true, false, false, false };
    public int[] CatHappinessLevel = new int[5] { 1, 1, 1, 1, 1 };
    public float[] CatCurHappinessExp = new float[5] { 0, 0, 0, 0, 0 };
    public string[] CatName = {"하양이","까망이","삼색이","치즈", "회색이" };

    //간식 [캣잎사탕, 츄르, 고등어구이, 육포, 참치캔, 연어,]
    public int[] Food = { 100, 2, 0, 4, 5 ,100};
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
