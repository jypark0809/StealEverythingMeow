using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CatHoustSceneTop : UI_Base
{
    private const int MAX_COUNT = 5;
    int _rechargeInterval; // 충전 시간 간격
    int _remainTime; // 젤리 충전 후 남은 시간(저번 종료시 남은 시간 고려하기 전)
    int _lastRemainTime; // 충전 남은 시간(저번 종료시 남은 시간까지 계산한 후)
    GameObject remainTimeText;
    Coroutine timerCoroutine = null;

    enum Texts
    {
        LevelText,
        JellyText,
        DiamondText,
        GoldText,
        RechargeText
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        _rechargeInterval = TimeScheduler.Instance.RechargeTime;
        _remainTime = TimeScheduler.Instance.RemainTime;
        LoadLastRemainTime();

        Bind<TextMeshProUGUI>(typeof(Texts));

        remainTimeText = GetText((int)Texts.RechargeText).gameObject;
        GetText((int)Texts.JellyText).text = $"{Managers.Game.SaveData.Jelly}/5";
        GetText((int)Texts.LevelText).text = Managers.Game.SaveData.SpaceLevel.ToString();
        GetText((int)Texts.DiamondText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Dia);
        GetText((int)Texts.GoldText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Gold);

        if (Managers.Game.SaveData.Jelly < MAX_COUNT)
            InitRechargeTimeText();
        else
            remainTimeText.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        SaveLastRemainTime();
    }

    void InitRechargeTimeText()
    {
        if (_lastRemainTime - _remainTime < 0)
        {
            Managers.Game.SaveData.Jelly++;
            if (Managers.Game.SaveData.Jelly == MAX_COUNT)
            {
                remainTimeText.SetActive(false);
                _lastRemainTime = _rechargeInterval;
            }
            else
            {
                _lastRemainTime = _rechargeInterval + _lastRemainTime - _remainTime;
                if (timerCoroutine == null)
                    timerCoroutine = StartCoroutine(SetTimeScheduler());
            }
        }
        else
        {
            _lastRemainTime -= _remainTime;
            if (timerCoroutine == null)
                timerCoroutine = StartCoroutine(SetTimeScheduler());
        }

        GetText((int)Texts.JellyText).text = $"{Managers.Game.SaveData.Jelly}/5";
    }

    public void SetActiveRechargeText()
    {
        remainTimeText.SetActive(true);
        _lastRemainTime = _rechargeInterval;
        GetText((int)Texts.RechargeText).text = _lastRemainTime.ToString();
        if (timerCoroutine == null)
            timerCoroutine = StartCoroutine(SetTimeScheduler());
    }

    void LoadLastRemainTime()
    {
        if (PlayerPrefs.HasKey("LastRemainTime"))
            _lastRemainTime = PlayerPrefs.GetInt("LastRemainTime");
        else
            _lastRemainTime = _rechargeInterval;
    }

    void SaveLastRemainTime()
    {
        PlayerPrefs.SetInt("LastRemainTime", _lastRemainTime);
    }

    public void RefreshUI()
    {
        GetText((int)Texts.JellyText).text = $"{Managers.Game.SaveData.Jelly}/5";
        GetText((int)Texts.LevelText).text = Managers.Game.SaveData.SpaceLevel.ToString();
        GetText((int)Texts.DiamondText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Dia);
        GetText((int)Texts.GoldText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Gold);
    }

    IEnumerator SetTimeScheduler()
    {
        while (true)
        {
            GetText((int)Texts.RechargeText).text = _lastRemainTime.ToString();
            if (_lastRemainTime <= 0)
            {
                Managers.Game.SaveData.Jelly++;
                RefreshUI();
                if (Managers.Game.SaveData.Jelly == MAX_COUNT)
                {
                    remainTimeText.SetActive(false);
                    // StopCoroutine
                }
                _lastRemainTime = _rechargeInterval;
            }
            yield return new WaitForSeconds(1f);
            _lastRemainTime--;
        }
    }
}
