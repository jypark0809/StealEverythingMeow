using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem_Furniture : UI_Base
{
    public ScrollRect _scrollRect;
    bool isDrag = false;

    FurnitureData _fData;
    public bool isPurchasable; // true면 구매 가능

    enum Images
    {
        ItemImage,
    }

    enum Buttons
    {
        PurchaseButton,
        PurchasedButton
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
        _scrollRect = transform.parent.parent.parent.GetComponent<ScrollRect>();

        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnPurchaseButtonClicked, Define.UIEvent.Click);
        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnDrag, Define.UIEvent.Drag);
        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnEndDrag, Define.UIEvent.EndDrag);

        GetButton((int)Buttons.PurchasedButton).gameObject.BindEvent(OnPurchasedButtonClicked, Define.UIEvent.Click);
        GetButton((int)Buttons.PurchasedButton).gameObject.BindEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        GetButton((int)Buttons.PurchasedButton).gameObject.BindEvent(OnDrag, Define.UIEvent.Drag);
        GetButton((int)Buttons.PurchasedButton).gameObject.BindEvent(OnEndDrag, Define.UIEvent.EndDrag);

        if (isPurchasable)
        {
            GetButton((int)Buttons.PurchasedButton).gameObject.SetActive(false);
        }
        else
        {
            GetButton((int)Buttons.PurchaseButton).gameObject.SetActive(false);
        }
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

    void OnPurchaseButtonClicked(PointerEventData evt)
    {
        // Disable click when draging
        if (isDrag == true)
            return;

        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        UI_ConfirmPauchasePopup ui = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
        ui.SetFurnitureInfo(_fData);
    }

    void OnPurchasedButtonClicked(PointerEventData evt)
    {
        // Disable click when draging
        if (isDrag == true)
            return;

        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        Debug.Log("This furniture is already purchased");
    }

    void OnBeginDrag(PointerEventData evt)
    {
        isDrag = true;
        _scrollRect.OnBeginDrag(evt);
    }

    void OnDrag(PointerEventData evt)
    {
        _scrollRect.OnDrag(evt);
    }

    void OnEndDrag(PointerEventData evt)
    {
        isDrag = false;
        _scrollRect.OnEndDrag(evt);
    }
}
