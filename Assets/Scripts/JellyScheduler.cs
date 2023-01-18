using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyScheduler : MonoBehaviour
{
    private static JellyScheduler instance;
    public static JellyScheduler Instance { get { Init(); return instance; } }

    private DateTime laseQuitTime = new DateTime(2023, 1, 1).ToLocalTime();
    private const int MAX_COUNT = 5;
    int rechargeInterval = 30;
    private Coroutine scedulerCoroutine = null;
    public int m_RechargeRemainTime;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();
        PrintSaveData();
    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Scheduler");
            if (go == null)
            {
                go = new GameObject { name = "@Scheduler" };
                go.AddComponent<JellyScheduler>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<JellyScheduler>();
        }
    }

    // 게임 초기화, 중간 이탈, 복귀 시 실행되는 함수
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            LoadLastQuitTime();
            SetRechargeScheduler();
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

    public void LoadLastQuitTime()
    {
        if (PlayerPrefs.HasKey("LastQuitTime"))
        {
            var getTime = string.Empty;
            getTime = PlayerPrefs.GetString("LastQuitTime");
            laseQuitTime = DateTime.FromBinary(Convert.ToInt64(getTime));
        }
    }

    public void SaveLastQuitTime()
    {
        var currentTime = DateTime.Now.ToLocalTime().ToBinary().ToString();
        PlayerPrefs.SetString("LastQuitTime", currentTime);
        // PlayerPrefs.Save();
    }

    public void SetRechargeScheduler(Action onFinish = null)
    {
        if (scedulerCoroutine != null)
            StopCoroutine(scedulerCoroutine);

        if (Managers.Game.SaveData.Jelly >= MAX_COUNT)
            return;

        var timeDifferenceInSec = (int)((DateTime.Now.ToLocalTime() - laseQuitTime).TotalSeconds);
        var addCount = timeDifferenceInSec / rechargeInterval;
        var remainTime = timeDifferenceInSec % rechargeInterval;
        Managers.Game.SaveData.Jelly += addCount;
        if (Managers.Game.SaveData.Jelly >= MAX_COUNT)
        {
            Managers.Game.SaveData.Jelly = MAX_COUNT;
        }
        else
        {
            scedulerCoroutine = StartCoroutine(DoRechargeTimer(remainTime, onFinish));
        }
    }

    private IEnumerator DoRechargeTimer(int remainTime, Action onFinish = null)
    {
        if (remainTime <= 0)
        {
            m_RechargeRemainTime = rechargeInterval;
        }
        else
        {
            m_RechargeRemainTime = remainTime;
        }

        while (m_RechargeRemainTime > 0)
        {
            m_RechargeRemainTime -= 1;
            yield return new WaitForSeconds(1f);
        }

        Managers.Game.SaveData.Jelly++;
        if (Managers.Game.SaveData.Jelly >= MAX_COUNT)
        {
            Managers.Game.SaveData.Jelly = MAX_COUNT;
            m_RechargeRemainTime = 0;
            scedulerCoroutine = null;
        }
        else
        {
            scedulerCoroutine = StartCoroutine(DoRechargeTimer(rechargeInterval, onFinish));
        }
    }

    void PrintSaveData()
    {
        Debug.Log(Managers.Game.SaveData.Jelly);
    }
}
