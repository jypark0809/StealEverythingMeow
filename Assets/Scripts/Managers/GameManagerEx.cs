using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

[Serializable]
public class GameData
{
    public int Jelly;
    public int Gold;
    public int Dia;
    public int Level;
    public long LastQuitTime;

    public bool BGMOn = true;
    public bool EffectSoundOn = true;

    public int[] Motion = new int[MOTION_COUNT];
}

public class GameManagerEx
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    public bool IsLoaded = false;

    public int Jelly
    {
        get { return _gameData.Jelly; }
        set { _gameData.Jelly = value; }
    }

    public int Gold
    {
        get { return _gameData.Gold; }
        set { _gameData.Gold = value; }
    }

    public int Dia
    {
        get { return _gameData.Dia; }
        set { _gameData.Dia = value; }
    }

    public long LastQuitTime
    {
        get { return _gameData.LastQuitTime; }
        set { _gameData.LastQuitTime = value; }
    }

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

    public void Init()
    {
        _path = Application.persistentDataPath + "/SaveData.json";
        if (!LoadGame())
        {
            InitSaveData();
            return;
        }

        IsLoaded = true;

        SaveGame();
    }

    public bool SpendJelly()
    {
        Jelly--;
        if (Managers.UI.SceneUI is UI_CatHouseScene)
        {
            (Managers.UI.SceneUI as UI_CatHouseScene).RefreshJelly();
        }
        return true;
    }

    public bool CheckGold(int gold)
    {
        if (Gold >= gold)
            return true;
        else
            return false;
    }

    public bool SpendGold(int gold)
    {
        if (CheckGold(gold))
        {
            Gold -= gold;

            //if (Managers.UI.SceneUI is UI_SelectStageScene)
            //{
            //    (Managers.UI.SceneUI as UI_SelectStageScene).TopUI.Refresh();
            //}
            //return true;
        }

        return false;
    }

    public void GetGold(int gold)
    {
        //Gold += coin;
        //if (Managers.UI.SceneUI is UI_SelectStageScene)
        //{
        //    (Managers.UI.SceneUI as UI_SelectStageScene).TopUI.Refresh();
        //}
    }

    public bool CheckDia(int dia)
    {
        if (Dia >= dia)
            return true;
        else
            return false;
    }

    public bool SpendDia(int dia)
    {
        if (CheckDia(dia))
        {
            //Dia -= dia;
            //if (Managers.UI.SceneUI is UI_SelectStageScene)
            //{
            //    (Managers.UI.SceneUI as UI_SelectStageScene).TopUI.Refresh();
            //}
            //return true;
        }

        return false;
    }

    public void GetDia(int dia)
    {
        //Dia += dia;
        //if (Managers.UI.SceneUI is UI_SelectStageScene)
        //{
        //    (Managers.UI.SceneUI as UI_SelectStageScene).TopUI.Refresh();
        //}
    }

    #region RechargeJelly


    #endregion

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

    void InitSaveData()
    {
        GameData newData = new GameData();
        newData.LastQuitTime = new DateTime(2023, 1, 1).ToBinary();
        Managers.Game.SaveData = newData;
    }
    #endregion
}
