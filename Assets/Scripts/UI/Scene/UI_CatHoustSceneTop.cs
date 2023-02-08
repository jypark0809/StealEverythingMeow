using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CatHoustSceneTop : UI_Base
{
    const int MAX_COUNT = 5;
    const int RECHARGE_INTERVAL = 1800;
    int _remainTime; // 젤리 충전 후 남은 시간(저번 종료시 남은 시간 고려하기 전)
    int _lastRemainTime; // 충전 남은 시간(저번 종료시 남은 시간까지 계산한 후)
    GameObject remainTimeText;

    enum Texts
    {
        LevelText,
        // JellyText,
        DiamondText,
        GoldText,
        // RechargeText
    }

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        // remainTimeText = GetText((int)Texts.RechargeText).gameObject;
    }

    public override void Init()
    {
        GetText((int)Texts.LevelText).text = Managers.Game.SaveData.SpaceLevel.ToString();
        GetText((int)Texts.DiamondText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Dia);
        GetText((int)Texts.GoldText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Gold);
    }

    float timer = 1;
    private void Update()
    {
        if (Managers.Game.SaveData.Jelly >= MAX_COUNT)
            return;

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            _lastRemainTime--;
            setTimeFormatString(_lastRemainTime);
            if (_lastRemainTime <= 0)
            {
                Debug.Log($"Jelly++ at Update()");
                Managers.Game.SaveData.Jelly++;
                RefreshUI();
                if (Managers.Game.SaveData.Jelly == MAX_COUNT)
                {
                    remainTimeText.SetActive(false);
                }
                _lastRemainTime = RECHARGE_INTERVAL;
            }
            timer = 1;
        }
    }

    //void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        LoadLastRemainTime();
    //        RechargeJelly();
    //        InitRechargeTimeText();
    //    }
    //    else
    //    {
    //        SaveLastRemainTime();
    //    }
    //}

    //void OnApplicationQuit()
    //{
    //    SaveLastRemainTime();
    //}

    void InitRechargeTimeText()
    {
        if (Managers.Game.SaveData.Jelly >= MAX_COUNT)
        {
            remainTimeText.SetActive(false);
            _lastRemainTime = RECHARGE_INTERVAL;
        }

        // GetText((int)Texts.JellyText).text = $"{Managers.Game.SaveData.Jelly}/5";
        setTimeFormatString(_lastRemainTime);
    }

    public void SetActiveRechargeText()
    {
        remainTimeText.SetActive(true);
        _lastRemainTime = RECHARGE_INTERVAL;
        setTimeFormatString(_lastRemainTime);
    }

    void LoadLastRemainTime()
    {
        if (PlayerPrefs.HasKey("LastRemainTime"))
        {
            _lastRemainTime = PlayerPrefs.GetInt("LastRemainTime");
            Debug.Log($"LoadLastRemainTime : {_lastRemainTime}");
        }
            
        else
            _lastRemainTime = RECHARGE_INTERVAL;
    }

    void SaveLastRemainTime()
    {
        PlayerPrefs.SetInt("LastRemainTime", _lastRemainTime);
    }

    public void RechargeJelly()
    {
        if (Managers.Game.SaveData.Jelly >= MAX_COUNT)
        {
            _lastRemainTime = RECHARGE_INTERVAL;
            return;
        }

        int timeDifferenceInSec = TimeScheduler.Instance.GetUnconnectedTime();
        Debug.Log($"timeDifferenceInSec : {timeDifferenceInSec}sec");

        var addCount = timeDifferenceInSec / RECHARGE_INTERVAL;
        _remainTime = timeDifferenceInSec % RECHARGE_INTERVAL;
        Managers.Game.SaveData.Jelly += addCount;

        if (_lastRemainTime - _remainTime < 0)
        {
            Debug.Log($"Jelly++ at RechargeJelly()");
            Managers.Game.SaveData.Jelly++;
            _lastRemainTime = RECHARGE_INTERVAL + _lastRemainTime - _remainTime;
        }
        else
        {
            _lastRemainTime -= _remainTime;
        }

        if (Managers.Game.SaveData.Jelly >= MAX_COUNT)
        {
            Managers.Game.SaveData.Jelly = MAX_COUNT;
            _lastRemainTime = RECHARGE_INTERVAL;
        }
    }

    public void RefreshUI()
    {
        // GetText((int)Texts.JellyText).text = $"{Managers.Game.SaveData.Jelly}/5";
        GetText((int)Texts.LevelText).text = Managers.Game.SaveData.SpaceLevel.ToString();
        GetText((int)Texts.DiamondText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Dia);
        GetText((int)Texts.GoldText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Gold);
    }

    void setTimeFormatString(int totalSec)
    {
        int min = totalSec / 60;
        int sec = totalSec % 60;
        string result = sec.ToString("D2");
        // GetText((int)Texts.RechargeText).text = $"{min}:{result}";
    }
}
