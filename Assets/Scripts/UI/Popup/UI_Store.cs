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
        BuyFurnitureButton,
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
        GetButton((int)Buttons.BuyFurnitureButton).gameObject.BindEvent(BuyFurnitureButton);
    }

    void BuySomsomButton(PointerEventData evt)
    {
        GameObject go = Managers.Resource.Instantiate("Somsom", Managers.Object.CatHouse.transform);
        go.transform.position = Util.FindChild(Managers.Object.CatHouse.gameObject, "Som_Spwan_Point").transform.position;
        Managers.UI.ClosePopupUI();
    }
    void BuyFurnitureButton(PointerEventData evt)
    {
        GameObject go = Managers.Resource.Instantiate("Somsom", Managers.Object.CatHouse.transform);
        go.transform.position = Util.FindChild(Managers.Object.CatHouse.gameObject, "Living_1", true).transform.position;
        //가구 배치or 해금
        Managers.UI.ClosePopupUI();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
