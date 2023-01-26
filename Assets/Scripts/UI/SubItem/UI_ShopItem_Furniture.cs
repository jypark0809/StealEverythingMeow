using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem_Furniture : UI_Base
{
    public int id;
    public string itemName;
    public string itemDesc;
    public string itemPrice;
    public string spritePath;
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

        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnButtonClicked);
        GetButton((int)Buttons.PurchaseButton).interactable = isPurchasable;

        GetImage((int)Images.ItemImage).sprite = Managers.Resource.Load<Sprite>(spritePath);
        GetImage((int)Images.ItemImage).SetNativeSize();
        GetImage((int)Images.ItemImage).rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        GetText((int)Texts.ItemName).text = itemName;
        GetText((int)Texts.ItemDesc).text = itemDesc;
        GetText((int)Texts.PriceText).text = itemPrice;
    }

    void OnButtonClicked(PointerEventData evt)
    {
        if (GetButton((int)Buttons.PurchaseButton).interactable)
        {

        }
    }
}
