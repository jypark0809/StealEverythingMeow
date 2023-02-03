using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UpgradePopUp : UI_Popup
{
    enum GameObjects
    {
        UI_UnlockRoomPopup,
        UI_UpgradeSom
    }
    enum Buttons
    {
        CloseButton,
        Space,
        Soom,
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Space).gameObject.BindEvent(OpenSpace);
        GetButton((int)Buttons.Soom).gameObject.BindEvent(OpenSoom);

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OpenSoom(PointerEventData evt)
    {
        GetObject((int)GameObjects.UI_UnlockRoomPopup).SetActive(false);
        GetObject((int)GameObjects.UI_UpgradeSom).SetActive(true);
    }
    void OpenSpace(PointerEventData evt)
    {
        GetObject((int)GameObjects.UI_UnlockRoomPopup).SetActive(true);
        GetObject((int)GameObjects.UI_UpgradeSom).SetActive(false);
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
