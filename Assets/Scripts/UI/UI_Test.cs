using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Test : UI_Base
{
    bool isMaxCount = true;
    float remainTime = 0;
    const int rechargeInterval = 30;
    private const int MAX_COUNT = 5;

    enum Texts
    {
        JellyText,
        TimeText,
    }

    enum Buttons
    {
        Button,
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (!isMaxCount)
        {
            UpdateTime();
        }
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        if(Managers.Game.SaveData.Jelly < 5)
        {
            GetText((int)Texts.JellyText).gameObject.SetActive(true);
        }
        else
        {
            GetText((int)Texts.JellyText).gameObject.SetActive(true);
        }

        remainTime = rechargeInterval - JellyScheduler.Instance.m_RechargeRemainTime;

        GetText((int)Texts.JellyText).text = $"{Managers.Game.SaveData.Jelly}/{MAX_COUNT}";
        GetButton((int)Buttons.Button).gameObject.BindEvent(SubJelly);
    }

    void SubJelly(PointerEventData evt)
    {
        Managers.Game.SaveData.Jelly--;
        Managers.Game.SaveGame();
    }

    void UpdateTime()
    {
        remainTime -= Time.deltaTime;
        int min = (int)remainTime / 60;
        int sec = (int)remainTime % 60;
        string result = sec.ToString("D2");
        GetText((int)Texts.TimeText).text = $"{min}:{result}";
    }
}
