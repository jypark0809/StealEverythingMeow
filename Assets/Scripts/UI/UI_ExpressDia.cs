using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ExpressDia : UI_Popup
{
    int Index;
    enum Texts
    {
        ConditionText,
        DiaPrice

    }
    enum Buttons
    {
        OkButton,
        CloseButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));



        GetText((int)Texts.ConditionText).text = ($"'{Managers.Data.ExpressBooks[1501+Index].Express_Name}'¸¦ È¹µæÇÏ·Á¸é\n /<color = red >{Index}</color>¸¦ ´Þ¼ºÇØ¾ß ÇØ¿ä!");


        if (Managers.Game.SaveData.Dia<100)
        {
            GetText((int)Texts.DiaPrice).text = "<color=red>" +"100"+ "</color>";
        }
        else
        {
            GetButton((int)Buttons.OkButton).gameObject.BindEvent(GetEmotion);
        }
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }


    public void SetInfo(int _index)
    {
        Index = _index;
    }

    void GetEmotion(PointerEventData evt)
    {
        Debug.Log("È¹µæ");
        Managers.Game.SaveData.Emotion[Index] = true;
        Managers.UI.ClosePopupUI();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
