using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class UI_ConfirmPauchasePopup : UI_Popup
{
    enum PurchaseType
    {
        Furniture,
        Item
    }

    FurnitureData _fData;
    ShopItemData _iData;
    PurchaseType _purchaseType;

    enum Texts
    {
        ItemNameText,
        PriceText,
    }

    enum Buttons
    {
        PurchaseButton,
        CancleButton
    }

    enum Images
    {
        GoldImage,
        DiaImage
    }

    void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnPurchaseButtonClicked);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CancleButtonClicked);
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopupUI();
        }
#endif
    }

    public void SetItemInfo(ShopItemData iData)
    {
        _iData = iData;
        _purchaseType = PurchaseType.Item;
        RefreshUI();
    }

    public void SetFurnitureInfo(FurnitureData fData)
    {
        _fData = fData;
        _purchaseType = PurchaseType.Furniture;
        RefreshUI();
    }

    void RefreshUI()
    {
        switch ((int)_purchaseType)
        {
            case (int)PurchaseType.Furniture:
                GetText((int)Texts.ItemNameText).text = _fData.F_Name;
                GetText((int)Texts.PriceText).text = _fData.F_Gold.ToString("N0");
                GetImage((int)Images.DiaImage).gameObject.SetActive(false);
                break;
            case (int)PurchaseType.Item:
                GetText((int)Texts.ItemNameText).text = _iData.Shop_Name;
                GetText((int)Texts.PriceText).text = _iData.Pay_Value.ToString("N0");

                if (_iData.Pay_Type == (int)Define.ShopPurchaseType.Gold)
                {
                    GetImage((int)Images.DiaImage).gameObject.SetActive(false);
                }
                else if (_iData.Pay_Type == (int)Define.ShopPurchaseType.Diamond)
                {
                    GetImage((int)Images.GoldImage).gameObject.SetActive(false);
                }

                break;
        }
    }

    void PurchaseFurniture()
    {
        // Not Enough Gold
        if (Managers.Game.SaveData.Gold < _fData.F_Gold)
        {
            UI_NotEnoughGoods ui = Managers.UI.ShowPopupUI<UI_NotEnoughGoods>();
            ui.SetFurnitureInfo(_fData);
            return;
        }

        // Happiness Point

        // Purchase
        Managers.Game.SaveData.Gold -= _fData.F_Gold;

        // Add furniture to fList of save data
        Managers.Game.SaveData.FList.Add(_fData);

        // arrange Furniture\
        
        GameObject go = Managers.Resource.Instantiate($"Furniture/{_fData.F_Space_Num}/{_fData.F_Int_Name}");
        go.transform.position = new Vector2(go.transform.localPosition.x + 0.5f, go.transform.localPosition.y + 0.5f);

        // Camera
        Camera.main.GetComponent<CameraMove>().Exam(go);

        // Save Data
        Managers.Game.SaveGame();

        // Refresh UI
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();

        // Close All Popup UI;
        Managers.UI.CloseAllPopupUI();
    }



    void PurchaseItem()
    {
        // Check Enough [Gold / Dia]
        if (_iData.Pay_Type == (int)ShopPurchaseType.Gold)
        {
            if (Managers.Game.SaveData.Gold < _iData.Pay_Value)
            {
                UI_NotEnoughGoods ui = Managers.UI.ShowPopupUI<UI_NotEnoughGoods>();
                ui.SetItemInfo(_iData);
                return;
            }

            Managers.Game.SaveData.Gold -= _iData.Pay_Value;
        }
        else if (_iData.Pay_Type == (int)ShopPurchaseType.Diamond)
        {
            if (Managers.Game.SaveData.Dia < _iData.Pay_Value)
            {
                UI_NotEnoughGoods ui = Managers.UI.ShowPopupUI<UI_NotEnoughGoods>();
                ui.SetItemInfo(_iData);
                return;
            }

            Managers.Game.SaveData.Dia -= _iData.Pay_Value;
        }

        // Get Reward DB
        RewardData rData;
        Managers.Data.Rewards.TryGetValue(_iData.Reward, out rData);
        Managers.Game.SaveData.Gold += rData.Gold;
        Managers.Game.SaveData.Dia += rData.Diamond;
        Managers.Game.SaveData.Wood += rData.Wood;
        Managers.Game.SaveData.Stone += rData.Stone;
        Managers.Game.SaveData.Cotton += rData.Cotton;
        Managers.Game.SaveData.Jelly += rData.Jelly;

        // Exception : Snack Item
        // Snack [CatnipCandy, Churu, Mackerel, Jerky, Tuna, Salmon]
        switch (_iData.Shop_Id)
        {
            case 1601:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Churu]++;
                break;
            case 1602:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Mackerel]++;
                break;
            case 1603:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Jerky]++;
                break;
            case 1604:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Tuna]++;
                break;
            case 1605:
                Managers.Game.SaveData.Food[(int)Define.SnackType.Salmon]++;
                break;
            case 1606:
                Managers.Game.SaveData.Food[(int)Define.SnackType.CatnipCandy]++;
                break;
        }

        // Savd Data
        Managers.Game.SaveGame();

        // Refresh UI
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();

        Managers.UI.ClosePopupUI();
    }

    #region EventHandler
    void OnPurchaseButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        switch ((int)_purchaseType)
        {
            case (int)PurchaseType.Furniture:
                PurchaseFurniture();
                break;
            case (int)PurchaseType.Item:
                PurchaseItem();
                break;
        }
    }

    void CancleButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        ClosePopupUI();
    }
    #endregion
    
}
