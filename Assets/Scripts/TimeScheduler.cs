using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class TimeScheduler : MonoBehaviour
{
    private static TimeScheduler instance;
    public static TimeScheduler Instance { get { Init(); return instance; } }
    private DateTime lastQuitTime;

    void Awake()
    {
        Init();
        lastQuitTime = new DateTime(2023, 1, 1).ToLocalTime();
    }

    #region Unity Event Function
    // 게임 초기화, 중간 이탈, 복귀 시 실행되는 함수
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            LoadLastQuitTime();
        }
        else
        {
            SaveLastQuitTime();
        }
    }

    //게임 종료 시 실행되는 함수
    public void OnApplicationQuit()
    {
        SaveLastQuitTime();
    }
    #endregion

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Scheduler");
            if (go == null)
            {
                go = new GameObject { name = "@Scheduler" };
                go.AddComponent<TimeScheduler>();
            }

            instance = go.GetComponent<TimeScheduler>();
        }
    }

    void Start()
    {
        // 마지막 접속한 날짜와 지금 접속한 날짜가 다를 때
        if (lastQuitTime.Year != lastQuitTime.Year || lastQuitTime.Month != lastQuitTime.Month || lastQuitTime.Day != DateTime.Now.ToLocalTime().Day)
        {
            InitData();
        }
        StartCoroutine(Timer());
    }

    public void LoadLastQuitTime()
    {
        // Debug.Log($"LoadLastQuitTime HasKey : {PlayerPrefs.HasKey("LastQuitTime")}");
        if (PlayerPrefs.HasKey("LastQuitTime"))
        {
            var getTime = string.Empty;
            getTime = PlayerPrefs.GetString("LastQuitTime");
            lastQuitTime = DateTime.FromBinary(Convert.ToInt64(getTime));
            // Debug.Log($"LoadLastQuitTime : {lastQuitTime}");
        }
    }

    public void SaveLastQuitTime()
    {
        var currentTime = DateTime.Now.ToLocalTime().ToBinary().ToString();
        PlayerPrefs.SetString("LastQuitTime", currentTime);
        // Debug.Log(PlayerPrefs.HasKey("LastQuitTime"));
    }

    public int GetUnconnectedTime()
    {
        return (int)((DateTime.Now.ToLocalTime() - lastQuitTime).TotalSeconds);
    }

    private IEnumerator Timer()
    {
        bool isNextDay = false;
        while (isNextDay == false)
        {
            DateTime now = DateTime.Now.ToLocalTime();
            if (now.Hour == 0 && now.Minute == 0 && now.Second == 0)
            {
                InitData(); // 데이터 초기화
                isNextDay = true;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void InitData()
    {
        // 데이터 초기화
        Managers.Game.SaveData.adsData.InitAdsCountData();

        for (int i = 0; i < Managers.Game.SaveData.DaysRwd.Length; i++)
        {
            Managers.Game.SaveData.DaysRwd[i] = false;
        }
        if(Managers.Game.SaveData.CatHave[0])
            Managers.Object.CatLobbyWhite.OnDayRwd();
        if (Managers.Game.SaveData.CatHave[1])
            Managers.Object.CatLobbyBlack.OnDayRwd();
        if (Managers.Game.SaveData.CatHave[2])
            Managers.Object.CatLobbyCalico.OnDayRwd();
        if (Managers.Game.SaveData.CatHave[3])
            Managers.Object.CatLobbyTabby.OnDayRwd();
        if (Managers.Game.SaveData.CatHave[4])
            Managers.Object.CatLobbyGray.OnDayRwd();    

        Managers.Game.SaveGame();
    }
}
