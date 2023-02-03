using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_HeartSet : UI_Base
{
    private Image FrontImage;
    private float CurExp;
    private float NextExp;

    enum Images
    {
        FrontHeart,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));

        FrontImage = GetImage((int)Images.FrontHeart);
        FrontImage.fillAmount = CurExp / NextExp;
    }
    public void SetInfo(float _cur, float _next)
    {
        CurExp = _cur;
        NextExp = _next;
    }

}