using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataTransformer : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Tools/DeleteGameData")]
    public static void DeleteGameData()
    {
        string path = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(path))
            File.Delete(path);
    }

    [MenuItem("Tools/ParseExcel")]
    public static void ParseExcel()
    {
        ParseLevelExpData("LevelExp");
        ParseStatSpeedData("StatSpeed");
        ParseStatSightData("StatSight");
        ParseStatMagnetData("StatMagnet");
        ParseDestroyableObjectData("DestroyableObject");
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
                Level = int.Parse(row[i++]),
                TotalExp = int.Parse(row[i++])
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

            loader.StatSpeeds.Add(new StatSpeedData()
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

    static void ParseStatSightData(string filename)
    {
        StatSightDataLoader loader = new StatSightDataLoader();

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

            loader.StatSights.Add(new StatSightData()
            {
                Stats_Lv = int.Parse(row[i++]),
                Stats_Sight = float.Parse(row[i++])
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

            loader.StatMagnets.Add(new StatMagnetData()
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
#endif
}
