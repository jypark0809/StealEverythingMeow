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

        if(Managers.Game.SaveData.currentFurniture ==5)
            GetButton((int)Buttons.BuyFurnitureButton).interactable = false;
    }

    void BuySomsomButton(PointerEventData evt)
    {
        Managers.Resource.Destroy(Util.FindChild(Managers.Object.CatHouse.gameObject, "newspaper", true));
        GameObject go = Managers.Resource.Instantiate("Somsom", Managers.Object.CatHouse.transform);
        go.transform.position = Util.FindChild(Managers.Object.CatHouse.gameObject, "Som_Spwan_Point").transform.position;
        Managers.UI.ClosePopupUI();

    }
    void BuyFurnitureButton(PointerEventData evt)
    {
        //가구 배치or 해금
        switch (Managers.Game.SaveData.currentFurniture)
        {
            case 0:
                GameObject go0 = Managers.Resource.Instantiate("1", Managers.Object.CatHouse.transform);
                go0.transform.position = (Util.FindChild(Managers.Object.CatHouse.gameObject, "1", true)).transform.position;
                break;
            case 1:
                GameObject go1 = Managers.Resource.Instantiate("2", Managers.Object.CatHouse.transform);
                go1.transform.position = (Util.FindChild(Managers.Object.CatHouse.gameObject, "2", true)).transform.position;
                break;
            case 2:
                GameObject go2 = Managers.Resource.Instantiate("3", Managers.Object.CatHouse.transform);
                go2.transform.position = (Util.FindChild(Managers.Object.CatHouse.gameObject, "3", true)).transform.position;
                break;
            case 3:
                GameObject go3 = Managers.Resource.Instantiate("4", Managers.Object.CatHouse.transform);
                go3.transform.position = (Util.FindChild(Managers.Object.CatHouse.gameObject, "4", true)).transform.position;
                break;
            case 4:
                GameObject go4 = Managers.Resource.Instantiate("5", Managers.Object.CatHouse.transform);
                go4.transform.position = (Util.FindChild(Managers.Object.CatHouse.gameObject, "5", true)).transform.position;
                break;
        }
        Managers.Game.SaveData.currentFurniture++;
        Managers.UI.ClosePopupUI();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
