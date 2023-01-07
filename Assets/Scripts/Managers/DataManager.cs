using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, LevelExpData> LevelExps { get; private set; } = new Dictionary<int, LevelExpData>();

    public void Init()
    {
        LevelExps = LoadJson<LevelExpDataLoader, int, LevelExpData>("LevelExpData").MakeDict();
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
