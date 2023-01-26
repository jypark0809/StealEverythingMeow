using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PurchaseFurniture : UI_Popup
{
    enum GameObjects
    {
        Blocker,
        Content,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.Blocker).BindEvent(OnCloseButtonClicked);

        Transform parent = GetObject((int)GameObjects.Content).transform;
        for (int i = 0; i < Managers.Data.Furnitures.Count; i++)
        {
            FurnitureData furniture;
            Managers.Data.Furnitures.TryGetValue(i + 1101, out furniture);

            if (furniture.F_Space_Num == PlayerPrefs.GetInt("SpaceLevel"))
            {
                UI_ShopItem_Furniture item = Managers.UI.MakeSubItem<UI_ShopItem_Furniture>(parent.transform);
                item.itemName = furniture.F_Name;
                item.itemDesc = furniture.F_Desc;
                item.itemPrice = furniture.F_Gold.ToString();
            }
        }
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        ClosePopupUI();
    }
}
