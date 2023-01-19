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

        if (Util.FindChild(Managers.Object.CatHouse.gameObject, "Somsom", true))
        {
            GetButton((int)Buttons.BuySomsomButton).interactable = false;
        }
        else
        {
            GetButton((int)Buttons.BuySomsomButton).gameObject.BindEvent(BuySomsomButton);
        }
        if (Managers.Game.SaveData.curFurnitureCount >= Managers.Game.SaveData.MaxFurniture[Managers.Game.SaveData.RoomLevel])
            GetButton((int)Buttons.BuyFurnitureButton).interactable = false;
        else
        {
            GetButton((int)Buttons.BuyFurnitureButton).gameObject.BindEvent(BuyFurnitureButton);
        }
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
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
        Managers.Object.CatHouse.GetComponent<TileManager>().OpenF();
        //Managers.UI.ClosePopupUI();
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
