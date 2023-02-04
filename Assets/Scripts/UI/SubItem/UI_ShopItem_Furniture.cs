using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem_Furniture : UI_Base
{
    public int id;
    FurnitureData fData;
    public bool isPurchasable; // true면 구매 가능

    enum Images
    {
        ItemImage,
    }

    enum Buttons
    {
        PurchaseButton,
    }

    enum Texts
    {
        ItemName,
        ItemDesc,
        PriceText
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        Managers.Data.Furnitures.TryGetValue(id, out fData);

        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnButtonClicked);
        GetButton((int)Buttons.PurchaseButton).interactable = isPurchasable;

        GetImage((int)Images.ItemImage).sprite = Managers.Resource.Load<Sprite>(fData.F_Path);
        GetImage((int)Images.ItemImage).SetNativeSize();
        GetImage((int)Images.ItemImage).rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        GetText((int)Texts.ItemName).text = fData.F_Name;
        GetText((int)Texts.ItemDesc).text = fData.F_Desc;
        GetText((int)Texts.PriceText).text = $"{fData.F_Gold.ToString("N0")}G";
    }

    void OnButtonClicked(PointerEventData evt)
    {
        if (GetButton((int)Buttons.PurchaseButton).interactable)
        {
            Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
            PlayerPrefs.SetInt("ItemId", id);
        }
    }
}
