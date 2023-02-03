using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelExpData
{
    public int Game_Lv;
    public int Game_Lv_Exp;
}

[Serializable]
public class LevelExpDataLoader : ILoader<int, LevelExpData>
{
    public List<LevelExpData> levelExps = new List<LevelExpData>();

    public Dictionary<int, LevelExpData> MakeDict()
    {
        Dictionary<int, LevelExpData> dic = new Dictionary<int, LevelExpData>();

        foreach (LevelExpData levelExp in levelExps)
            dic.Add(levelExp.Game_Lv, levelExp);

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
    public List<StatSpeedData> statSpeeds = new List<StatSpeedData>();

    public Dictionary<int, StatSpeedData> MakeDict()
    {
        Dictionary<int, StatSpeedData> dic = new Dictionary<int, StatSpeedData>();

        foreach (StatSpeedData speedData in statSpeeds)
            dic.Add(speedData.Stats_Lv, speedData);

        return dic;
    }
}

[Serializable]
public class StatCooltimeData
{
    public int Stats_Lv;
    public float Stats_Cooltime;
}

[Serializable]
public class StatCooltimeDataLoader : ILoader<int, StatCooltimeData>
{
    public List<StatCooltimeData> statCooltimes = new List<StatCooltimeData>();

    public Dictionary<int, StatCooltimeData> MakeDict()
    {
        Dictionary<int, StatCooltimeData> dic = new Dictionary<int, StatCooltimeData>();

        foreach (StatCooltimeData coolTimeData in statCooltimes)
            dic.Add(coolTimeData.Stats_Lv, coolTimeData);

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
    public List<StatMagnetData> statMagnets = new List<StatMagnetData>();

    public Dictionary<int, StatMagnetData> MakeDict()
    {
        Dictionary<int, StatMagnetData> dic = new Dictionary<int, StatMagnetData>();

        foreach (StatMagnetData magnetData in statMagnets)
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


[Serializable]
public class FurnitureData : IEquatable<FurnitureData>
{
    public int F_Id;
    public string F_Int_Name;
    public string F_Name;
    public string F_Desc;
    public int F_Space_Num;
    public int F_Happiness;
    public int F_Gold;
    public string F_Path;

    public bool Equals(FurnitureData other)
    {
        if (this.F_Id == other.F_Id)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

[Serializable]
public class FurnitureDataLoader : ILoader<int, FurnitureData>
{
    public List<FurnitureData> Furnitures = new List<FurnitureData>();

    public Dictionary<int, FurnitureData> MakeDict()
    {
        Dictionary<int, FurnitureData> dic = new Dictionary<int, FurnitureData>();

        foreach (FurnitureData furnitureData in Furnitures)
            dic.Add(furnitureData.F_Id, furnitureData);

        return dic;
    }
}

[Serializable]
public class SoomData
{
    public int Soom_Id;
    public string Soom_Int_Name;
    public string Soom_Name;
    public string Soom_Desc;
    public int Soom_Lv;
    public int Happiness;
    public int Space_Num;
    public int Space_F_Count;
    public int Cap_Capacity;
    public int Gold;
    public int Diamond;
    public int Wood;
    public int Stone;
    public int Cotton;
    public string Soom_Img;
}

[Serializable]
public class SoomDataLoader : ILoader<int, SoomData>
{
    public List<SoomData> Sooms = new List<SoomData>();

    public Dictionary<int, SoomData> MakeDict()
    {
        Dictionary<int, SoomData> dic = new Dictionary<int, SoomData>();

        foreach (SoomData SoomData in Sooms)
            dic.Add(SoomData.Soom_Id, SoomData);

        return dic;
    }
}

[Serializable]
public class SpaceData
{
    public int Space_Id;
    public string Space_Int_Name;
    public string Space_Name;
    public int Space_Lv;
    public int Space_Furniture_Count;
    public int Space_Gold_Plus;
    public int Gold;
    public int Wood;
    public int Stone;
    public int Cotton;
    public int Happiness;
    public int Space_Time;
    public int Soom_Lv;

}

[Serializable]
public class SpaceDataLoader : ILoader<int, SpaceData>
{
    public List<SpaceData> Spaces = new List<SpaceData>();

    public Dictionary<int, SpaceData> MakeDict()
    {
        Dictionary<int, SpaceData> dic = new Dictionary<int, SpaceData>();

        foreach (SpaceData objectData in Spaces)
            dic.Add(objectData.Space_Id, objectData);

        return dic;
    }
}

[Serializable]
public class CatBookData
{
    public int Cat_Id;
    public string Cat_Int_Name;
    public string Cat_Name;
    public string Cat_Desc;
    public int Cat_Favor_Food;
    public string Cat_Skill_Name;
    public int Cat_Skill_Count;
    public int Cat_Soom_Lv;
    public string Cat_Skill_Desc;
    public int Cat_Present_Type;
    public int Cat_Present_Count;
    public int Cat_Present_Time;
    public int Happiness;
    public int Soom_Lv;
    public int Gold;
    public int Diamond;
    public string Cat_Path;
}
[Serializable]
public class CatBookDataLoader : ILoader<int, CatBookData>
{
    public List<CatBookData> CatBooks = new List<CatBookData>();

    public Dictionary<int, CatBookData> MakeDict()
    {
        Dictionary<int, CatBookData> dic = new Dictionary<int, CatBookData>();

        foreach (CatBookData CatBookData in CatBooks)
            dic.Add(CatBookData.Cat_Id, CatBookData);

        return dic;
    }
}

[Serializable]
public class ExpressBookData
{
    public int Express_Id;
    public string Express_Int_Name;
    public string Express_Name;
    public string Express_Rwd;
    public int Diamond;
    public int Express_Time;
    public string Express_Path;
}
[Serializable]
public class ExpressBookDataLoader : ILoader<int, ExpressBookData>
{
    public List<ExpressBookData> ExpressBooks = new List<ExpressBookData>();

    public Dictionary<int, ExpressBookData> MakeDict()
    {
        Dictionary<int, ExpressBookData> dic = new Dictionary<int, ExpressBookData>();

        foreach (ExpressBookData ExpressBookData in ExpressBooks)
            dic.Add(ExpressBookData.Express_Id, ExpressBookData);

        return dic;
    }
}

[Serializable]
public class ShopItemData
{
    public int Shop_Id;
    public string Shop_Int_Name;
    public string Shop_Name;
    public string Shop_Desc;
    public int Shop_Type;
    public int Shop_Limit_Count;
    public int PaymentType;
    public int Value;
    public float Scale;
    public string ImgPath;
}
[Serializable]
public class ShopItemDataLoader : ILoader<int, ShopItemData>
{
    public List<ShopItemData> ShopItems = new List<ShopItemData>();

    public Dictionary<int, ShopItemData> MakeDict()
    {
        Dictionary<int, ShopItemData> dic = new Dictionary<int, ShopItemData>();

        foreach (ShopItemData itemData in ShopItems)
            dic.Add(itemData.Shop_Id, itemData);

        return dic;
    }
}

[Serializable]
public class HappinessData
{
    public int H_Id;
    public int H_Lv;
    public int H_Max;
    public int H_Cat_Type;
    public int H_Rwd_Wood;
    public int H_Rwd_Stone;
    public int H_Rwd_Cotton;
    public int H_Rwd_Gold;
    public int H_Rwd_Power;
}
[Serializable]
public class HappinessDataLoader : ILoader<int, HappinessData>
{
    public List<HappinessData> Happinesses = new List<HappinessData>();

    public Dictionary<int, HappinessData> MakeDict()
    {
        Dictionary<int, HappinessData> dic = new Dictionary<int, HappinessData>();

        foreach (HappinessData HappinessData in Happinesses)
            dic.Add(HappinessData.H_Id, HappinessData);

        return dic;
    }
}