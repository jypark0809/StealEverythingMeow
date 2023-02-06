using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem_ItemType : UI_Base
{
    [SerializeField]
    int itemType;

    enum Buttons
    {
        ShopItem_ItemType,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.ShopItem_ItemType).gameObject.BindEvent(OnButtonClicked);
    }

    void OnButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Managers.UI.ShowPopupUI<UI_PurchaseItem>();
        PlayerPrefs.SetInt("ItemType", itemType);
    }
}
