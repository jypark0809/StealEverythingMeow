using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PauseGamePopup : UI_Popup
{
    enum Buttons
    {
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

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        Time.timeScale = 1;
        Managers.UI.ClosePopupUI();
    }
}
