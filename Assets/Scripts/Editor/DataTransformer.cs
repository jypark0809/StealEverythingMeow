using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DataTransformer : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Tools/DeleteGameData")]
    public static void DeleteGameData()
    {
        string path = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(path))
            File.Delete(path);

        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Tools/ParseExcel")]
    public static void ParseExcel()
    {
        ParseCatBookData("CatBook");
        ParseDestroyableObjectData("DestroyableObject");
        ParseExpressBookData("ExpressBook");
        ParseFurnitureData("Furniture");
        ParseHappinessData("Happiness");
        ParseLevelExpData("LevelExp");
        ParseShopItemData("ShopItem");
        ParseRewardData("Reward");
        ParseSoomData("Soom");
        ParseSpaceData("Space");
        ParseStatCooltimeData("StatCooltime");
        ParseStatMagnetData("StatMagnet");
        ParseStatSpeedData("StatSpeed");
    }

    static void ParseLevelExpData(string filename)
    {
        LevelExpDataLoader loader = new LevelExpDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.levelExps.Add(new LevelExpData()
            {
                Game_Lv = int.Parse(row[i++]),
                Game_Lv_Exp = int.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseStatSpeedData(string filename)
    {
        StatSpeedDataLoader loader = new StatSpeedDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.statSpeeds.Add(new StatSpeedData()
            {
                Stats_Lv = int.Parse(row[i++]),
                Stats_Speed = float.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseStatCooltimeData(string filename)
    {
        StatCooltimeDataLoader loader = new StatCooltimeDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.statCooltimes.Add(new StatCooltimeData()
            {
                Stats_Lv = int.Parse(row[i++]),
                Stats_Cooltime = float.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseStatMagnetData(string filename)
    {
        StatMagnetDataLoader loader = new StatMagnetDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.statMagnets.Add(new StatMagnetData()
            {
                Stats_Lv = int.Parse(row[i++]),
                Stats_Magnet = float.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseDestroyableObjectData(string filename)
    {
        DestroyableObjectDataLoader loader = new DestroyableObjectDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.destroyableObjects.Add(new DestroyableObjectData()
            {
                Object_Id = int.Parse(row[i++]),
                Object_Int_Name = row[i++],
                Player_Lv = int.Parse(row[i++]),
                Touch_Count = int.Parse(row[i++]),
                Object_Gold = int.Parse(row[i++]),
                Object_Diamond = int.Parse(row[i++]),
                Image_Path = row[i++]
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseFurnitureData(string filename)
    {
        FurnitureDataLoader loader = new FurnitureDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.Furnitures.Add(new FurnitureData()
            {
                F_Id = int.Parse(row[i++]),
                F_Int_Name = row[i++],
                F_Name = row[i++],
                F_Desc = row[i++],
                F_Space_Num = int.Parse(row[i++]),
                F_Happiness = int.Parse(row[i++]),
                F_Gold = int.Parse(row[i++]),
                F_Path = row[i++],
                F_Size = float.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseSoomData(string filename)
    {
        SoomDataLoader loader = new SoomDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.Sooms.Add(new SoomData()
            {
                Soom_Id = int.Parse(row[i++]),
                Soom_Int_Name = row[i++],
                Soom_Name = row[i++],
                Soom_Desc = row[i++],
                Soom_Lv = int.Parse(row[i++]),
                Happiness = int.Parse(row[i++]),
                Space_Num = int.Parse(row[i++]),
                Space_F_Count = int.Parse(row[i++]),
                Cap_Capacity = int.Parse(row[i++]),
                Gold = int.Parse(row[i++]),
                Diamond = int.Parse(row[i++]),
                Wood = int.Parse(row[i++]),
                Stone = int.Parse(row[i++]),
                Cotton = int.Parse(row[i++]),
                //Soom_Path,
            });

        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseCatBookData(string filename)
    {
        CatBookDataLoader loader = new CatBookDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.CatBooks.Add(new CatBookData()
            {
                Cat_Id = int.Parse(row[i++]),
                Cat_Int_Name = row[i++],
                Cat_Name = row[i++],
                Cat_Desc = row[i++].Replace("\\n", "\n"),
                Cat_Favor_Food = int.Parse(row[i++]),
                Cat_Skill_Name = row[i++],
                Cat_Skill_Count = int.Parse(row[i++]),
                Cat_Soom_Lv = int.Parse(row[i++]),
                Cat_Skill_Desc = row[i++],
                Cat_Present_Type = int.Parse(row[i++]),
                Cat_Present_Count = int.Parse(row[i++]),
                Cat_Present_Time = int.Parse(row[i++]),
                Happiness = int.Parse(row[i++]),
                Soom_Lv = int.Parse(row[i++]),
                Gold = int.Parse(row[i++]),
                Diamond = int.Parse(row[i++]),
                Cat_Img = row[i++],
                End_Story1 = row[i++].Replace("/", ","),
                End_Story1_Img = row[i++],
                End_Story2 = row[i++].Replace("/", ","),
                End_Story2_Img = row[i++],
                End_Story3 = row[i++].Replace("/", ","),
                End_Story3_Img = row[i++],
                End_Story4 = row[i++].Replace("/", ","),
                End_Story4_Img = row[i++],
                End_Story5 = row[i++].Replace("/", ","),
                End_Story5_Img = row[i++],
                End_Reward1_Type = int.Parse(row[i++]),
                End_Reward1 = int.Parse(row[i++]),
                End_Reward2_Type = int.Parse(row[i++]),
                End_Reward2 = int.Parse(row[i++]),
                End_Reward3_Type = int.Parse(row[i++]),
                End_Reward3 = int.Parse(row[i++]),
            });

        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseExpressBookData(string filename)
    {
        ExpressBookDataLoader loader = new ExpressBookDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.ExpressBooks.Add(new ExpressBookData()
            {
                Express_Id = int.Parse(row[i++]),
                Express_Int_Name = row[i++],
                Express_Name = row[i++],
                Express_Rwd = row[i++],
                Diamond = int.Parse(row[i++]),
                Express_Time = int.Parse(row[i++]),
                Express_Path = row[i++],
            });

        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseShopItemData(string filename)
    {
        ShopItemDataLoader loader = new ShopItemDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.ShopItems.Add(new ShopItemData()
            {
                Shop_Id = int.Parse(row[i++]),
                Shop_Int_Name = row[i++],
                Shop_Name = row[i++],
                Shop_Desc = row[i++].Replace("\\n", "\n"),
                Shop_Type = int.Parse(row[i++]),
                Shop_Limit_Num = int.Parse(row[i++]),
                Pay_Type = int.Parse(row[i++]),
                Pay_Value = int.Parse(row[i++]),
                Reward = int.Parse(row[i++]),
                Scale = float.Parse(row[i++]),
                ImgPath = row[i++]
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseRewardData(string filename)
    {
        RewardDataLoader loader = new RewardDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.Rewards.Add(new RewardData()
            {
                Id = int.Parse(row[i++]),
                Gold = int.Parse(row[i++]),
                Diamond = int.Parse(row[i++]),
                Wood = int.Parse(row[i++]),
                Stone = int.Parse(row[i++]),
                Cotton = int.Parse(row[i++]),
                Jelly = int.Parse(row[i++])
            });

        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseHappinessData(string filename)
    {
        HappinessDataLoader loader = new HappinessDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.Happinesses.Add(new HappinessData()
            {
                H_Id = int.Parse(row[i++]),
                H_Lv = int.Parse(row[i++]),
                H_Max = int.Parse(row[i++]),
                H_Cat_Type = int.Parse(row[i++]),
                H_Rwd_Wood = int.Parse(row[i++]),
                H_Rwd_Stone = int.Parse(row[i++]),
                H_Rwd_Cotton = int.Parse(row[i++]),
                H_Rwd_Gold = int.Parse(row[i++]),
                H_Rwd_Power = int.Parse(row[i++]),
            });

        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseSpaceData(string filename)
    {
        SpaceDataLoader loader = new SpaceDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.Spaces.Add(new SpaceData()
            {
                Space_Id = int.Parse(row[i++]),
                Space_Int_Name = row[i++],
                Space_Name = row[i++],
                Space_Desc = row[i++].Replace("\\n", "\n"),
                Space_Lv = int.Parse(row[i++]),
                Space_Furniture_Count = int.Parse(row[i++]),
                Space_Gold_Plus = int.Parse(row[i++]),
                Gold = int.Parse(row[i++]),
                Wood = int.Parse(row[i++]),
                Stone = int.Parse(row[i++]),
                Cotton = int.Parse(row[i++]),
                Happiness = int.Parse(row[i++]),
                Space_Time = int.Parse(row[i++]),
                Soom_Lv = int.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }
#endif
}
