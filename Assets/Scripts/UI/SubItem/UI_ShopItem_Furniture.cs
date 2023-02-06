using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem_Furniture : UI_Base
{
    FurnitureData _fData;
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

    void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnButtonClicked);
        GetButton((int)Buttons.PurchaseButton).interactable = isPurchasable;
    }

    public void SetInfo(FurnitureData fData)
    {
        _fData = fData;
        RefreshUI();
    }

    void RefreshUI()
    {
        GetImage((int)Images.ItemImage).sprite = Managers.Resource.Load<Sprite>(_fData.F_Path);
        GetImage((int)Images.ItemImage).SetNativeSize();
        GetImage((int)Images.ItemImage).rectTransform.localScale = new Vector3(_fData.F_Size, _fData.F_Size, _fData.F_Size);

        GetText((int)Texts.ItemName).text = _fData.F_Name;
        GetText((int)Texts.ItemDesc).text = _fData.F_Desc;
        GetText((int)Texts.PriceText).text = $"{_fData.F_Gold.ToString("N0")}";
    }

    void OnButtonClicked(PointerEventData evt)
    {
        if (GetButton((int)Buttons.PurchaseButton).interactable)
        {
            UI_ConfirmPauchasePopup ui = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
            ui.SetFurnitureInfo(_fData);
        }
    }
}
