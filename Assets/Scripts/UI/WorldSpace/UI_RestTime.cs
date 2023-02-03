using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_RestTime : UI_Base
{
    private float RestTime;
    private float AllTime;
    private TextMeshProUGUI text1;
    private TextMeshProUGUI text2;
    private Image BarImage;

    enum Texts
    {
        TimeText,
        PlaceText
    }
    enum Images
    {
        ImagesBar,
    }

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

        BarImage.fillAmount = 0f;
        text1.text = Mathf.Floor(RestTime).ToString();
        text2.text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel].Space_Name + "  °ø»çÁß..."; 
    }
    void Update()
    {
        RestTime -= Time.deltaTime;
        text1.text = Mathf.Floor(RestTime).ToString();
        SetBarImage();
        if (RestTime <= 0)
            Destroy(this.gameObject);
    }

    void  SetBarImage()
    {
        BarImage.fillAmount = 1- (RestTime / AllTime);
    }

    public void SetInfo(float _Time)
    {
        AllTime = _Time;
        RestTime = _Time;
    }
}
