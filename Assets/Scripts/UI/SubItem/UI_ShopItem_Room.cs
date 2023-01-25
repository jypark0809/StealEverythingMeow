using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem_Room : UI_Base
{
    [SerializeField]
    int spaceLevel;
    
    enum Buttons
    {
        ShopItem_Room,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.ShopItem_Room).gameObject.BindEvent(OnButtonClicked);
    }

    void OnButtonClicked(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_PurchaseFurniture>();
        PlayerPrefs.SetInt("SpaceLevel", spaceLevel);
    }
}
