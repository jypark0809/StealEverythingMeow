using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CatHoustSceneTop : UI_Base
{
    enum Texts
    {
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

        GetText((int)Texts.JellyText).text = Managers.Game.SaveData.Jelly +"/5";
        GetText((int)Texts.DiamondText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Dia);
        GetText((int)Texts.GoldText).text = String.Format("{0:#,0}", Managers.Game.SaveData.Gold);
    }
}
