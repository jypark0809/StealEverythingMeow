using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class UI_ShopItem : UI_Base
{
    public int id;
    ShopItemData _iData;

    enum Images
    {
        ItemImage,
    }

    enum Buttons
    {
        GoldButton,
        DiamondButton,
        AdsButton,
        CashButton
    }

    enum Texts
    {
        ItemName,
        ItemDesc,
        PurchaseCountText,
        GoldText,
        DiamondText,
        AdsText,
        CashText
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
        GetButton((int)Buttons.GoldButton).gameObject.BindEvent(OnGoldButtonClicked);
        GetButton((int)Buttons.DiamondButton).gameObject.BindEvent(OnDiaButtonClicked);
        GetButton((int)Buttons.AdsButton).gameObject.BindEvent(OnAdsButtonClicked);
        GetButton((int)Buttons.CashButton).gameObject.BindEvent(OnIAPButtonClicked);
    }

    public void SetInfo(ShopItemData iData)
    {
        _iData = iData;
        RefreshUI();
    }

    void RefreshUI()
    {
        GetImage((int)Images.ItemImage).sprite = Managers.Resource.Load<Sprite>(_iData.ImgPath);
        GetImage((int)Images.ItemImage).SetNativeSize();
        GetImage((int)Images.ItemImage).rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        GetText((int)Texts.ItemName).text = _iData.Shop_Name;
        GetText((int)Texts.ItemDesc).text = _iData.Shop_Desc;

        if (_iData.Shop_Limit_Count == 0)
            GetText((int)Texts.PurchaseCountText).gameObject.SetActive(false);
        else
            GetText((int)Texts.PurchaseCountText).text = $"0/{_iData.Shop_Limit_Count}";

        switch(_iData.PaymentType)
        {
            case (int)ShopPurchaseType.Gold:
                GetButton((int)Buttons.DiamondButton).gameObject.SetActive(false);
                GetButton((int)Buttons.AdsButton).gameObject.SetActive(false);
                GetButton((int)Buttons.CashButton).gameObject.SetActive(false);
                GetText((int)Texts.GoldText).text = $"{_iData.Value.ToString("N0")}G";
                break;
            case (int)ShopPurchaseType.Diamond:
                GetButton((int)Buttons.GoldButton).gameObject.SetActive(false);
                GetButton((int)Buttons.AdsButton).gameObject.SetActive(false);
                GetButton((int)Buttons.CashButton).gameObject.SetActive(false);
                GetText((int)Texts.DiamondText).text = _iData.Value.ToString();
                break;
            case (int)ShopPurchaseType.Cash:
                GetButton((int)Buttons.GoldButton).gameObject.SetActive(false);
                GetButton((int)Buttons.DiamondButton).gameObject.SetActive(false);
                GetButton((int)Buttons.AdsButton).gameObject.SetActive(false);
                GetText((int)Texts.CashText).text = $"{_iData.Value.ToString("C")}";
                break;
            case (int)ShopPurchaseType.Ads:
                GetButton((int)Buttons.GoldButton).gameObject.SetActive(false);
                GetButton((int)Buttons.DiamondButton).gameObject.SetActive(false);
                GetButton((int)Buttons.CashButton).gameObject.SetActive(false);
                break;
        }
    }

    void OnGoldButtonClicked(PointerEventData evt)
    {
        UI_ConfirmPauchasePopup goldUI = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
        goldUI.SetItemInfo(_iData);
    }

    void OnDiaButtonClicked(PointerEventData evt)
    {
        UI_ConfirmPauchasePopup diaUI = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
        diaUI.SetItemInfo(_iData);
    }

    void OnAdsButtonClicked(PointerEventData evt)
    {
        Managers.Ads.ShowRewardedAds(() => { GetReward(); });
    }

    void OnIAPButtonClicked(PointerEventData evt)
    {
        //Managers.IAP.Purchase(_iData.productID, (product, failureReason) =>
        //{
        //    Debug.Log($"Purchase Done {product.transactionID} {failureReason}");
        //    // 성공했는지 확인
        //    if (failureReason == PurchaseFailureReason.Unknown)
        //        GetReward();
        //});
    }

    void GetReward()
    {
        // Get Reward DB
        Debug.Log("GetReward");
    }
}
