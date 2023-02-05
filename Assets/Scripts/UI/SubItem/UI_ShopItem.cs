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
        GetButton((int)Buttons.GoldButton).gameObject.BindEvent(OnClickButton);
        GetButton((int)Buttons.DiamondButton).gameObject.BindEvent(OnClickButton);
        GetButton((int)Buttons.AdsButton).gameObject.BindEvent(OnClickButton);
        GetButton((int)Buttons.CashButton).gameObject.BindEvent(OnClickButton);
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

    void OnClickButton(PointerEventData evt)
    {
        switch(_iData.Shop_Type)
        {
            // 골드로 구매하는 상품
            case (int)ShopPurchaseType.Gold:
                UI_ConfirmPauchasePopup goldUI = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
                goldUI.SetItemInfo(_iData);
                break;

            // 다이아로 구매하는 상품
            case (int)ShopPurchaseType.Diamond:
                UI_ConfirmPauchasePopup diaUI = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
                diaUI.SetItemInfo(_iData);
                break;

            // IAP
            case (int)ShopPurchaseType.Cash:
                //Managers.IAP.Purchase(_iData.productID, (product, failureReason) =>
                //{
                //    Debug.Log($"Purchase Done {product.transactionID} {failureReason}");
                //    // 성공했는지 확인
                //    if (failureReason == PurchaseFailureReason.Unknown)
                //        GiveReward();
                //});
                break;

            // Ads
            case (int)ShopPurchaseType.Ads:
                Managers.Ads.ShowRewardedAds(() => { GiveReward(); });
                break;
        }
    }

    void GiveReward()
    {
        
        //switch (_iData.Shop_Type)
        //{
        //    case (int)ShopPurchaseType.Cash:
        //        {
        //            switch(_iData.Shop_Id)
        //            {
        //                case 1615:
        //                case 1616:
        //                case 1617:
        //                case 1618:
        //                case 1619:
        //                    Managers.Game.SaveData.Dia += _iData.Value;
        //                    break;
        //                case 1623:
        //                    Managers.Game.SaveData.Gold += 70000;
        //                    Managers.Game.SaveData.Dia += 400;
        //                    Managers.Game.SaveData.Jelly += 10;
        //                    break;
        //                case 1624:
        //                    Managers.Game.SaveData.Wood += 200;
        //                    Managers.Game.SaveData.Stone += 70;
        //                    Managers.Game.SaveData.Cotton += 30;
        //                    break;
        //                case 1625:
        //                    Managers.Game.SaveData.Wood += 300;
        //                    Managers.Game.SaveData.Stone += 12;
        //                    Managers.Game.SaveData.Cotton += 63;
        //                    break;
        //                case 1626:
        //                    Managers.Game.SaveData.Gold += 20000;
        //                    Managers.Game.SaveData.Dia += 500;
        //                    Managers.Game.SaveData.Jelly += 15;
        //                    break;
        //            }
        //            // Refresh Scene UI
        //        }
        //        break;

        //    case (int)ShopPurchaseType.Ads:
        //        {
        //            switch (_iData.Shop_Id)
        //            {
        //                case 1610:
        //                    Managers.Game.SaveData.Jelly++;
        //                    break;
        //                case 1614:
        //                    Managers.Game.SaveData.Dia += 10;
        //                    break;
        //                case 1620:
        //                    Managers.Game.SaveData.Gold += 1000;
        //                    break;
        //            }
        //        }
        //        break;
        //}
    }
}
