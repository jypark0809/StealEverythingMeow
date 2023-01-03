using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Store : UI_Popup
{
    enum Buttons
    {
        BuySomsomButton,
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

        GetButton((int)Buttons.BuySomsomButton).gameObject.BindEvent(BuySomsomButton);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void BuySomsomButton(PointerEventData evt)
    {
        Managers.Resource.Instantiate("Somsom", Managers.Object.CatHouse.transform);
        Managers.UI.ClosePopupUI();
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
