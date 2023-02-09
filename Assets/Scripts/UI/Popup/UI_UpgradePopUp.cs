using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UpgradePopUp : UI_Popup
{
    enum Buttons
    {
        Space,
        Soom,
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

        GetButton((int)Buttons.Space).gameObject.BindEvent(OpenSpace);
        GetButton((int)Buttons.Soom).gameObject.BindEvent(OpenSoom);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OpenSoom(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_UnlockSoomPopup>();
    }
    void OpenSpace(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_UnlockRoomPopup>();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
