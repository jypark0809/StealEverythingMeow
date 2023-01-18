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

[Serializable]
public class StatSightData
{
    public int Stats_Lv;
    public float Stats_Sight;
}

[Serializable]
public class StatSightDataLoader : ILoader<int, StatSightData>
{
    public List<StatSightData> StatSights = new List<StatSightData>();

    public Dictionary<int, StatSightData> MakeDict()
    {
        Dictionary<int, StatSightData> dic = new Dictionary<int, StatSightData>();

        foreach (StatSightData sightData in StatSights)
            dic.Add(sightData.Stats_Lv, sightData);

        return dic;
    }
}

[Serializable]
public class StatMagnetData
{
    public int Stats_Lv;
    public float Stats_Magnet;
}

[Serializable]
public class StatMagnetDataLoader : ILoader<int, StatMagnetData>
{
    public List<StatMagnetData> StatMagnets = new List<StatMagnetData>();

    public Dictionary<int, StatMagnetData> MakeDict()
    {
        Dictionary<int, StatMagnetData> dic = new Dictionary<int, StatMagnetData>();

        foreach (StatMagnetData magnetData in StatMagnets)
            dic.Add(magnetData.Stats_Lv, magnetData);

        return dic;
    }
}

[Serializable]
public class DestroyableObjectData
{
    public int Object_Id;
    public string Object_Int_Name;
    public int Player_Lv;
    public int Touch_Count;
    public int Object_Gold;
    public int Object_Diamond;
    public string Image_Path;
}

[Serializable]
public class DestroyableObjectDataLoader : ILoader<int, DestroyableObjectData>
{
    public List<DestroyableObjectData> destroyableObjects = new List<DestroyableObjectData>();

    public Dictionary<int, DestroyableObjectData> MakeDict()
    {
        Dictionary<int, DestroyableObjectData> dic = new Dictionary<int, DestroyableObjectData>();

        foreach (DestroyableObjectData objectData in destroyableObjects)
            dic.Add(objectData.Object_Id, objectData);

        return dic;
    }
}