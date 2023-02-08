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
    public int Gold = 0;
    public int Dia = 0;

    // 재료
    public int Wood = 30;
    public int Cotton = 0;
    public int Stone = 0;

    // Furniture List
    public List<FurnitureData> FList = new List<FurnitureData>();


    public bool[] Emotion = new bool[Define.MOTION_COUNT];
    public List<string> EmotionList = new List<string>();


    //공간
    public int SpaceLevel = 1;
    public int SoomLevel = 1;
    public bool IsRoomOpen = false;
    public bool IsSoomUp = false;
    public bool DoingRoomUpgrade;
    public bool DoingSoomUpgrdae;

    public bool BGMOn = true;
    public bool EffectSoundOn = true;
    public bool firstExecution = true;


    // 고양이 보유여부 [White, Black, Calico, Tabby, Grey]
    public int CatCount = 1;
    public bool[] CatHave = new bool[5] { true, false, false, false, false };
    public int[] CatHappinessLevel = new int[5] { 1, 1, 1, 1, 1 };
    public float[] CatCurHappinessExp = new float[5] { 0, 0, 0, 0, 0 };
    public string[] CatName = {"하양이","까망이","삼색이","치즈", "회색이"};
    public bool[] DaysRwd = new bool[5] { false, false, false, false, false };

    // 간식 [캣잎사탕, 츄르, 고등어구이, 육포, 참치캔, 연어]
    public int[] Food = { 0, 0, 0, 0, 0, 0 };
    public GameData()
    {
        Jelly = 5;

        //감정표현 기본세팅
        Emotion[(int)Define.CatEmotion.Blink] = true;
        Emotion[(int)Define.CatEmotion.Sleep3] = true;
        Emotion[(int)Define.CatEmotion.Sniff] = true;
        Emotion[(int)Define.CatEmotion.Ennui] = true;
    }
}

public class GameManagerEx
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    public void SpendJelly()
    {
        SaveData.Jelly--;
        SaveGame();

        UI_CatHouseScene scene = (Managers.UI.SceneUI as UI_CatHouseScene);
        scene._catHouseSceneTop.RefreshUI();

        // MAX_COUNT - 1
        if (Managers.Game.SaveData.Jelly == 4)
        {
            scene._catHouseSceneTop.SetActiveRechargeText();
        }
    }

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
