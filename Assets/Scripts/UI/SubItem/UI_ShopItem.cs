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
    ShopItemData _iData;
    RectTransform _goldGroup, _diaGroup;
    public ScrollRect _scrollRect;
    bool isDrag = false;

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
        _scrollRect = transform.parent.parent.parent.GetComponent<ScrollRect>();
        _goldGroup = GetObject((int)GameObjects.GoldGroup).GetComponent<RectTransform>();
        _diaGroup = GetObject((int)GameObjects.DiaGroup).GetComponent<RectTransform>();

        GetButton((int)Buttons.GoldButton).gameObject.BindEvent(OnGoldButtonClicked, Define.UIEvent.Click);
        GetButton((int)Buttons.GoldButton).gameObject.BindEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        GetButton((int)Buttons.GoldButton).gameObject.BindEvent(OnDrag, Define.UIEvent.Drag);
        GetButton((int)Buttons.GoldButton).gameObject.BindEvent(OnEndDrag, Define.UIEvent.EndDrag);

        GetButton((int)Buttons.DiamondButton).gameObject.BindEvent(OnDiaButtonClicked, Define.UIEvent.Click);
        GetButton((int)Buttons.DiamondButton).gameObject.BindEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        GetButton((int)Buttons.DiamondButton).gameObject.BindEvent(OnDrag, Define.UIEvent.Drag);
        GetButton((int)Buttons.DiamondButton).gameObject.BindEvent(OnEndDrag, Define.UIEvent.EndDrag);

        GetButton((int)Buttons.AdsButton).gameObject.BindEvent(OnAdsButtonClicked, Define.UIEvent.Click);
        GetButton((int)Buttons.AdsButton).gameObject.BindEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        GetButton((int)Buttons.AdsButton).gameObject.BindEvent(OnDrag, Define.UIEvent.Drag);
        GetButton((int)Buttons.AdsButton).gameObject.BindEvent(OnEndDrag, Define.UIEvent.EndDrag);

        GetButton((int)Buttons.CashButton).gameObject.BindEvent(OnIAPButtonClicked, Define.UIEvent.Click);
        GetButton((int)Buttons.CashButton).gameObject.BindEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        GetButton((int)Buttons.CashButton).gameObject.BindEvent(OnDrag, Define.UIEvent.Drag);
        GetButton((int)Buttons.CashButton).gameObject.BindEvent(OnEndDrag, Define.UIEvent.EndDrag);
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

        if (_iData.Pay_Type != (int)ShopPurchaseType.Ads)
            GetText((int)Texts.PurchaseCountText).gameObject.SetActive(false);
        else
        {
            switch(_iData.Shop_Id)
            {
                case 1610:
                    GetText((int)Texts.PurchaseCountText).text = $"{Managers.Game.SaveData.adsData.JellyAds}/{_iData.Shop_Limit_Num}";
                    break;
                case 1614:
                    GetText((int)Texts.PurchaseCountText).text = $"{Managers.Game.SaveData.adsData.DiaAds}/{_iData.Shop_Limit_Num}";
                    break;
                case 1620:
                    GetText((int)Texts.PurchaseCountText).text = $"{Managers.Game.SaveData.adsData.GoldAds}/{_iData.Shop_Limit_Num}";
                    break;
            }
        }
            

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

        if (_iData.Shop_Id == 1610)
        {
            Managers.Game.SaveData.adsData.JellyAds--;
            GetText((int)Texts.PurchaseCountText).text = $"{Managers.Game.SaveData.adsData.JellyAds}/{_iData.Shop_Limit_Num}";
        }
        else if (_iData.Shop_Id == 1614)
        {
            Managers.Game.SaveData.adsData.DiaAds--;
            GetText((int)Texts.PurchaseCountText).text = $"{Managers.Game.SaveData.adsData.DiaAds}/{_iData.Shop_Limit_Num}";
        }
        else if (_iData.Shop_Id == 1620)
        {
            switch (Managers.Game.SaveData.SpaceLevel)
            {
                case 1:
                    Managers.Game.SaveData.Gold += 2000;
                    break;
                case 2:
                    Managers.Game.SaveData.Gold += 3291;
                    break;
                case 3:
                    Managers.Game.SaveData.Gold += 3590;
                    break;
                case 4:
                    Managers.Game.SaveData.Gold += 3889;
                    break;
                case 5:
                    Managers.Game.SaveData.Gold += 4488;
                    break;
                case 6:
                    Managers.Game.SaveData.Gold += 4787;
                    break;
                case 7:
                    Managers.Game.SaveData.Gold += 5086;
                    break;
                case 8:
                    Managers.Game.SaveData.Gold += 5385;
                    break;
                case 9:
                    Managers.Game.SaveData.Gold += 5684;
                    break;
                case 10:
                    Managers.Game.SaveData.Gold += 5984;
                    break;
            }

            Managers.Game.SaveData.adsData.GoldAds--;
            GetText((int)Texts.PurchaseCountText).text = $"{Managers.Game.SaveData.adsData.GoldAds}/{_iData.Shop_Limit_Num}";
        }
        

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
        // Disable click when draging
        if (isDrag == true)
            return;

        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        UI_ConfirmPauchasePopup goldUI = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
        goldUI.SetItemInfo(_iData);
    }

    // Dia
    void OnDiaButtonClicked(PointerEventData evt)
    {
        // Disable click when draging
        if (isDrag == true)
            return;

        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        UI_ConfirmPauchasePopup diaUI = Managers.UI.ShowPopupUI<UI_ConfirmPauchasePopup>();
        diaUI.SetItemInfo(_iData);
    }

    // IAP
    void OnIAPButtonClicked(PointerEventData evt)
    {
        // Disable click when draging
        if (isDrag == true)
            return;

        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Debug.Log("OnIAPButtonClicked");
        Managers.IAP.Purchase(_iData.Shop_Int_Name.ToString(), (product, failureReason) =>
        {
            Debug.Log($"Purchase Done {product.transactionID} {failureReason}");
            // 성공했는지 확인
            if (failureReason == PurchaseFailureReason.Unknown)
                GetReward();
        });
    }

    // Ads
    void OnAdsButtonClicked(PointerEventData evt)
    {
        // Disable click when draging
        if (isDrag == true)
            return;

        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        // Ads limit count <= 0
        if (GetText((int)Texts.PurchaseCountText).text == "0/3")
        {
            Debug.Log("Count is 0");
            return;
        }
        
        Managers.Ads.ShowRewardedAds(() => { GetReward(); });
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
    #endregion
}
