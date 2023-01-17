using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelExpData
{
    public int Level;
    public int TotalExp;
}

[Serializable]
public class LevelExpDataLoader : ILoader<int, LevelExpData>
{
    public List<LevelExpData> levelExps = new List<LevelExpData>();

    public Dictionary<int, LevelExpData> MakeDict()
    {
        Dictionary<int, LevelExpData> dic = new Dictionary<int, LevelExpData>();

        foreach (LevelExpData levelExp in levelExps)
            dic.Add(levelExp.Level, levelExp);

        return dic;
    }
}

[Serializable]
public class StatSpeedData
{
    public int Stats_Lv;
    public float Stats_Speed;
}

[Serializable]
public class StatSpeedDataLoader : ILoader<int, StatSpeedData>
{
    public List<StatSpeedData> StatSpeeds = new List<StatSpeedData>();

    public Dictionary<int, StatSpeedData> MakeDict()
    {
        Dictionary<int, StatSpeedData> dic = new Dictionary<int, StatSpeedData>();

        foreach (StatSpeedData speedData in StatSpeeds)
            dic.Add(speedData.Stats_Lv, speedData);

        return dic;
    }
}
