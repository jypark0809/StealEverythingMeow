using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UI_Setting : UI_Popup
{
    enum Buttons
    {
        Insta,
        Blog,
        CloseButton
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
        GetButton((int)Buttons.Insta).gameObject.BindEvent(InstaOpen);
        GetButton((int)Buttons.Blog).gameObject.BindEvent(BlogOpen);
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }

    void InstaOpen(PointerEventData evt)
    {
        Application.OpenURL("https://www.instagram.com/stealeverything_meow/");
    }

    void BlogOpen(PointerEventData evt)
    {
        Application.OpenURL("https://blog.naver.com/stealeverything_meow");
    }
}
