using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_RestTime : UI_Base
{
    private TextMeshProUGUI text1;
    private TextMeshProUGUI text2;
    private Image BarImage;

    DateTime st;

    enum Texts
    {
        TimeText,
        PlaceText
    }
    enum Images
    {
        ImagesBar,
    }


    int min;
    int sec;
    int DeltaTime;
    private float AllTime;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        text1 = GetText((int)Texts.TimeText);
        text2 = GetText((int)Texts.PlaceText);
        BarImage = GetImage((int)Images.ImagesBar);
        text2.text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel+1].Space_Name + "  °ø»çÁß...";

        st = DateTime.ParseExact(PlayerPrefs.GetString("OpenTime"), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
    }
    void Update()
    {
        SetTime();
        SetBarImage();
        if (DeltaTime < 0 || !Managers.Game.SaveData.DoingRoomUpgrade)
            Destroy(this.gameObject);
    }

    void SetTime()
    {
        DeltaTime = (int)(st - DateTime.Now).TotalSeconds;

        min = (int)(st - DateTime.Now).TotalSeconds / 60;
        sec = (int)(st - DateTime.Now).TotalSeconds % 60;
        string result = sec.ToString("D2");
        text1.text = $"{min}:{result}";

    }
    void  SetBarImage()
    {
        BarImage.fillAmount = 1- (DeltaTime / AllTime);
    }
    public void SetInfo(float _Time)
    {
        AllTime = _Time;
    }
}
