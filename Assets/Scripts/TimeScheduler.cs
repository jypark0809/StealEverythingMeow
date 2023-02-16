using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // ���� �ʱ�ȭ, �߰� ��Ż, ���� �� ����Ǵ� �Լ�
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

    //���� ���� �� ����Ǵ� �Լ�
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
        // ������ ������ ��¥�� ���� ������ ��¥�� �ٸ� ��
        if (lastQuitTime.Year != lastQuitTime.Year && lastQuitTime.Month != lastQuitTime.Month && lastQuitTime.Day != DateTime.Now.ToLocalTime().Day)
        {
            InitData();
        }
            
        StartCoroutine(Timer());
    }

    public void LoadLastQuitTime()
    {
        Debug.Log($"LoadLastQuitTime HasKey : {PlayerPrefs.HasKey("LastQuitTime")}");
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
        while (true)
        {
            DateTime now = DateTime.Now.ToLocalTime();
            if (now.Hour == 0 && now.Minute == 0 && now.Minute == 0)
            {
                InitData(); // ������ �ʱ�ȭ
            }

            yield return new WaitForSeconds(60f);
        }
    }

    void InitData()
    {
        // ������ �ʱ�ȭ
        Managers.Game.SaveData.adsData.InitAdsCountData();

        Managers.Game.SaveGame();
    }
}
