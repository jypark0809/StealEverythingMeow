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
    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopupUI();
        }
#endif
    }
    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        
        GetText((int)Texts.RwdText).text = "행복도가 Lv " + Managers.Game.SaveData.CatHappinessLevel[CatIndex] + " 로 올랐어요.";
        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();

    }
}

