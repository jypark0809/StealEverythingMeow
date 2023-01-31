using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Temp : UI_Popup
{
    enum GameObjects
    {
        Blocker,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.Blocker).BindEvent(OnCloseButtonClicked);
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        ClosePopupUI();
    }

}
