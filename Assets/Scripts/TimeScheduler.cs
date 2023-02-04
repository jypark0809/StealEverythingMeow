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

    // n�� �ڿ� ���� ����
    const int _rechargeInterval = 1800;
    public int RechargeTime { get { return _rechargeInterval; } }

    // ���� ���� �� ���� �ð�
    int _remainTime; 
    public int RemainTime { get { return _remainTime; } set { _remainTime = value; } } 

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    // ���� �ʱ�ȭ, �߰� ��Ż, ���� �� ����Ǵ� �Լ�
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

    //���� ���� �� ����Ǵ� �Լ�
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
        // ������ ������ ��¥�� ���� ������ ��¥�� �ٸ� ��
        if (laseQuitTime.Month != laseQuitTime.Month && laseQuitTime.Day != DateTime.Now.ToLocalTime().Day)
        {
            InitData();

            // ����� �ູ�� ��ư Ȱ��ȭ
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
                InitData(); // ������ �ʱ�ȭ
                
                // ����� �ູ�� ��ư Ȱ��ȭ
            }

            yield return new WaitForSeconds(60f);
        }
    }

    void InitData()
    {
        // ������ �ʱ�ȭ
    }
}
