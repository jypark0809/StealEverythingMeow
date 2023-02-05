using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_HappyLevelUp : UI_Popup
{
    public int CatIndex;

    enum Texts
    {
        RwdText
    }
    enum Buttons
    {
        OkButton,

    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        
        GetText((int)Texts.RwdText).text = " �ູ���� Lv " + Managers.Game.SaveData.CatHappinessLevel[CatIndex] + " �� ���Ҿ��.";
        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();

    }
}
