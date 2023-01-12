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
        QuestButton,
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

        GetButton((int)Buttons.SettingButton).gameObject.BindEvent(SettingOpen);
        GetButton((int)Buttons.StoreButton).gameObject.BindEvent(StoreOpen);
        GetButton((int)Buttons.CollectionButton).gameObject.BindEvent(ColletionOpen);
        GetButton((int)Buttons.QuestButton).gameObject.BindEvent(QuestOpen);
        GetButton((int)Buttons.BagButton).gameObject.BindEvent(BagOpen);
    }


    void SettingOpen(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
    void StoreOpen(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Store>();
    }
    void ColletionOpen(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Colletion>();
    }
    void QuestOpen(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Quest>();
    }
    void BagOpen(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Bag>();
    }

}
