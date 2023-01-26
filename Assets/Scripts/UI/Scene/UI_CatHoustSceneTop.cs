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


        GetText((int)Texts.JellyText).text = Managers.Game.SaveData.Jelly +" /5";
        GetText((int)Texts.DiamondText).text = Managers.Game.SaveData.Dia.ToString();
        GetText((int)Texts.GoldText).text = Managers.Game.SaveData.Gold.ToString();
        GetText((int)Texts.Current_Room_Text_Step).text = Managers.Game.SaveData.RoomLevel.ToString();
    }
}
