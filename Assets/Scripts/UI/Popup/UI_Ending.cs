using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Ending : UI_Popup
{
    enum GameObjects
    {
        Touch,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        GetObject((int)GameObjects.Touch).BindEvent(OnOpenEVent);
    }
    void OnOpenEVent(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_EndingResult>();
    }
}
