using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{

    //미니게임 데이터
    public Dictionary<int, LevelExpData> LevelExps { get; private set; } = new Dictionary<int, LevelExpData>();
    public Dictionary<int, StatSpeedData> StatSpeeds { get; private set; } = new Dictionary<int, StatSpeedData>();
    public Dictionary<int, StatCooltimeData> StatCooltimes { get; private set; } = new Dictionary<int, StatCooltimeData>();
    public Dictionary<int, StatMagnetData> StatMagnets { get; private set; } = new Dictionary<int, StatMagnetData>();
    public Dictionary<int, DestroyableObjectData> DestroyableObjects { get; private set; } = new Dictionary<int, DestroyableObjectData>();


    //육성게임 데이터
    public Dictionary<int, FurnitureData> Furnitures { get; private set; } = new Dictionary<int, FurnitureData>();
    public Dictionary<int, SoomData> Sooms { get; private set; } = new Dictionary<int, SoomData>();
    public Dictionary<int, SpaceData> Spaces { get; private set; } = new Dictionary<int, SpaceData>();
    public Dictionary<int, CatBookData> CatBooks { get; private set; } = new Dictionary<int, CatBookData>();

    public void Init()
    {
        LevelExps = LoadJson<LevelExpDataLoader, int, LevelExpData>("LevelExpData").MakeDict();
        StatSpeeds = LoadJson<StatSpeedDataLoader, int, StatSpeedData>("StatSpeedData").MakeDict();
        StatCooltimes = LoadJson<StatCooltimeDataLoader, int, StatCooltimeData>("StatCooltimeData").MakeDict();
        StatMagnets = LoadJson<StatMagnetDataLoader, int, StatMagnetData>("StatMagnetData").MakeDict();
        DestroyableObjects = LoadJson<DestroyableObjectDataLoader, int, DestroyableObjectData>("DestroyableObjectData").MakeDict();

        Furnitures = LoadJson<FurnitureDataLoader, int, FurnitureData>("FurnitureData").MakeDict();
        Sooms = LoadJson<SoomDataLoader, int, SoomData>("SoomData").MakeDict();
        Spaces = LoadJson<SpaceDataLoader, int, SpaceData>("SpaceData").MakeDict();
        CatBooks = LoadJson<CatBookDataLoader, int, CatBookData>("CatBookData").MakeDict();

    }

    public bool Loaded()
    {
        if (LevelExps == null)
            return false;
        return true;
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/Json/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
