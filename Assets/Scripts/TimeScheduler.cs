using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class TimeScheduler : MonoBehaviour
{
    private static TimeScheduler instance;
    public static TimeScheduler Instance { get { Init(); return instance; } }
    private DateTime laseQuitTime = new DateTime(2023, 1, 1).ToLocalTime();
    // private Coroutine scedulerCoroutine = null;
    private const int MAX_COUNT = 5;

    // n초 뒤에 젤리 충전
    const int _rechargeInterval = 1800;
    public int RechargeTime { get { return _rechargeInterval; } }

    // 젤리 충전 후 남은 시간
    int _remainTime; 
    public int RemainTime { get { return _remainTime; } set { _remainTime = value; } } 

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    // 게임 초기화, 중간 이탈, 복귀 시 실행되는 함수
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            LoadLastQuitTime();
            RechargeJelly();
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

            DontDestroyOnLoad(go);
            instance = go.GetComponent<TimeScheduler>();
        }
    }

    void Start()
    {
        // 마지막 접속한 날짜와 지금 접속한 날짜가 다를 때
        if (laseQuitTime.Month != laseQuitTime.Month && laseQuitTime.Day != DateTime.Now.ToLocalTime().Day)
        {
            InitData();

            // 고양이 행복도 버튼 활성화
        }
            

        StartCoroutine(Timer());
    }

    void LoadLastQuitTime()
    {
        if (PlayerPrefs.HasKey("LastQuitTime"))
        {
            var getTime = string.Empty;
            getTime = PlayerPrefs.GetString("LastQuitTime");
            laseQuitTime = DateTime.FromBinary(Convert.ToInt64(getTime));
        }
    }

    void SaveLastQuitTime()
    {
        var currentTime = DateTime.Now.ToLocalTime().ToBinary().ToString();
        PlayerPrefs.SetString("LastQuitTime", currentTime);
    }

    public void RechargeJelly(Action onFinish = null)
    {
        if (Managers.Game.SaveData.Jelly >= MAX_COUNT)
            return;

        var timeDifferenceInSec = (int)((DateTime.Now.ToLocalTime() - laseQuitTime).TotalSeconds);
        var addCount = timeDifferenceInSec / RechargeTime;
        _remainTime = timeDifferenceInSec % RechargeTime;
        Managers.Game.SaveData.Jelly += addCount;
        if (Managers.Game.SaveData.Jelly >= MAX_COUNT)
        {
            Managers.Game.SaveData.Jelly = MAX_COUNT;
        }
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            DateTime now = DateTime.Now.ToLocalTime();
            if (now.Hour == 0 && now.Minute == 0)
            {
                InitData(); // 데이터 초기화
                
                // 고양이 행복도 버튼 활성화
            }

            yield return new WaitForSeconds(60f);
        }
    }

    void InitData()
    {
        // 데이터 초기화
    }
}
