using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CatHoustSceneTop : UI_Base
{
    enum Texts
    {
        Current_Room_Text_Step,
        JellyText,
        DiamondText,
        GoldText,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.JellyText).text = "5/5";
        GetText((int)Texts.DiamondText).text = 999999.ToString();
        GetText((int)Texts.GoldText).text = 999999.ToString();
    }
}
