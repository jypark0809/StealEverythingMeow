using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PurchaseItem : UI_Popup
{
    enum GameObjects
    {
        Blocker,
        Content,
        CloseButton,
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
        GetObject((int)GameObjects.CloseButton).BindEvent(OnCloseButtonClicked);

        Transform parent = GetObject((int)GameObjects.Content).transform;

        for (int i = 0; i < Managers.Data.ShopItems.Count; i++)
        {
            ShopItemData iData;
            Managers.Data.ShopItems.TryGetValue(i + 1601, out iData);

            if (iData.Shop_Type == PlayerPrefs.GetInt("ItemType"))
            {
                UI_ShopItem shopItem = Managers.UI.MakeSubItem<UI_ShopItem>(parent.transform);
                shopItem.SetInfo(iData);
            }
        }
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        ClosePopupUI();
    }
}
