using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using static Define;

public class UI_ShopItem : UI_Base
{
    public int id;
    ShopItemData _iData;
    RectTransform _goldGroup, _diaGroup;

    enum GameObjects
    {
        GoldGroup,
        DiaGroup
    }

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
        Bind<GameObject>(typeof(GameObjects));
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        _goldGroup = GetObject((int)GameObjects.GoldGroup).GetComponent<RectTransform>();
        _diaGroup = GetObject((int)GameObjects.DiaGroup).GetComponent<RectTransform>();

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
        GetImage((int)Images.ItemImage).rectTransform.localScale = new Vector3(_iData.Scale, _iData.Scale, _iData.Scale);

        GetText((int)Texts.ItemName).text = _iData.Shop_Name;
        GetText((int)Texts.ItemDesc).text = _iData.Shop_Desc;

        if (_iData.Shop_Limit_Num == 0)
            GetText((int)Texts.PurchaseCountText).gameObject.SetActive(false);
        else
            GetText((int)Texts.PurchaseCountText).text = $"0/{_iData.Shop_Limit_Num}";

        switch (_iData.Pay_Type)
        {
            case (int)ShopPurchaseType.Gold:
                TextMeshProUGUI goldText = GetText((int)Texts.GoldText);
                GetButton((int)Buttons.DiamondButton).gameObject.SetActive(false);
                GetButton((int)Buttons.AdsButton).gameObject.SetActive(false);
                GetButton((int)Buttons.CashButton).gameObject.SetActive(false);
                goldText.text = $"{_iData.Pay_Value.ToString("N0")}";
                break;
            case (int)ShopPurchaseType.Diamond:
                GetButton((int)Buttons.GoldButton).gameObject.SetActive(false);
                GetButton((int)Buttons.AdsButton).gameObject.SetActive(false);
                GetButton((int)Buttons.CashButton).gameObject.SetActive(false);
                GetText((int)Texts.DiamondText).text = _iData.Pay_Value.ToString();
                break;
            case (int)ShopPurchaseType.Cash:
                GetButton((int)Buttons.GoldButton).gameObject.SetActive(false);
                GetButton((int)Buttons.DiamondButton).gameObject.SetActive(false);
                GetButton((int)Buttons.AdsButton).gameObject.SetActive(false);
                GetText((int)Texts.CashText).text = $"{_iData.Pay_Value.ToString("C")}";
                break;
            case (int)ShopPurchaseType.Ads:
                GetButton((int)Buttons.GoldButton).gameObject.SetActive(false);
                GetButton((int)Buttons.DiamondButton).gameObject.SetActive(false);
                GetButton((int)Buttons.CashButton).gameObject.SetActive(false);
                break;
        }

        // arrangeUI();
    }

    void arrangeUI()
    {
        switch (_iData.Pay_Type)
        {
            case (int)ShopPurchaseType.Gold:
                RectTransform goldTextRect = GetText((int)Texts.GoldText).gameObject.GetComponent<RectTransform>();
                {
                    float textWidth = goldTextRect.sizeDelta.x;
                    float imageWidth = 16 * 0.4f;
                    float spaceInterval = 3;
                    _goldGroup.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, goldTextRect.sizeDelta.x + imageWidth + spaceInterval);
                }
                break;
            case (int)ShopPurchaseType.Diamond:
                RectTransform diaTextRect = GetText((int)Texts.DiamondText).gameObject.GetComponent<RectTransform>();
                {
                    float textWidth = diaTextRect.sizeDelta.x;
                    float imageWidth = 19 * 0.4f;
                    float spaceInterval = 3;
                    _goldGroup.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, diaTextRect.sizeDelta.x + imageWidth + spaceInterval);
                }
                break;
        }
    }

    void GetReward()
    {
        // Get Reward DB
        RewardData rData;
        Managers.Data.Rewards.TryGetValue(_iData.Reward, out rData);

        Managers.Game.SaveData.Gold += rData.Gold;
        Managers.Game.SaveData.Dia += rData.Diamond;
        Managers.Game.SaveData.Wood += rData.Wood;
        Managers.Game.SaveData.Stone += rData.Stone;
        Managers.Game.SaveData.Cotton += rData.Cotton;
        Managers.Game.SaveData.Jelly += rData.Jelly;

        // Save Data
        Managers.Game.SaveGame();

        // Refresh UI
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();

        Debug.Log("GetReward");
    }

    #region EventHandler
    // Gold
    void OnGoldButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        UI_ConfirmPauchasePopup goldUI = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
        goldUI.SetItemInfo(_iData);
    }

    // Dia
    void OnDiaButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        UI_ConfirmPauchasePopup diaUI = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
        diaUI.SetItemInfo(_iData);
    }

    // IAP
    void OnIAPButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Debug.Log("OnIAPButtonClicked");
        Managers.IAP.Purchase(_iData.Shop_Id.ToString(), (product, failureReason) =>
        {
            Debug.Log($"Purchase Done {product.transactionID} {failureReason}");
            // �����ߴ��� Ȯ��
            if (failureReason == PurchaseFailureReason.Unknown)
                GetReward();
        });
    }

    // Ads
    void OnAdsButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Managers.Ads.ShowRewardedAds(() => { GetReward(); });
    }
    #endregion
}
