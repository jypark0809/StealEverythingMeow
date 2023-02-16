using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_FullHappy : UI_Popup
{
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
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnCloseButton);
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
