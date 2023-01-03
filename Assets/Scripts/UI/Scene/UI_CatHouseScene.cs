using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CatHouseScene : UI_Scene
{
    enum Buttons
    {
        SettingButton,
        StoreButton,
        CollectionButton,
        StatusButton,
        BagButton
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.StoreButton).gameObject.BindEvent(StoreOpen);
        GetButton((int)Buttons.BagButton).gameObject.BindEvent(Temp);
    }

    void StoreOpen(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Store>();
    }

    void Temp(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_UnlockRoomPopup>();
    }
}
