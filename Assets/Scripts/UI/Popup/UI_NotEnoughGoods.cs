using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_NotEnoughGoods : UI_Popup
{
    FurnitureData _fData;
    ShopItemData _iData;

    enum PurchaseType
    {
        Furniture,
        Item
    }
    PurchaseType _purchaseType;

    enum Images
    {
        Blocker,
        GoldImage,
        DiaImage
    }

    void Awake()
    {
        Bind<Image>(typeof(Images));
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        GetImage((int)Images.Blocker).gameObject.BindEvent(OnBlockerClicked);
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
                GetImage((int)Images.DiaImage).gameObject.SetActive(false);
                break;
            case (int)PurchaseType.Item:

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

    void OnBlockerClicked(PointerEventData evt)
    {
        ClosePopupUI();
    }
}
