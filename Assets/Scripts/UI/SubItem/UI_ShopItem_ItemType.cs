using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem_ItemType : UI_Base
{
    public ScrollRect _scrollRect;
    bool isDrag = false;

    [SerializeField]
    int itemType;

    enum Buttons
    {
        ShopItem_ItemType,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.ShopItem_ItemType).gameObject.BindEvent(OnButtonClicked, Define.UIEvent.Click);
        GetButton((int)Buttons.ShopItem_ItemType).gameObject.BindEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        GetButton((int)Buttons.ShopItem_ItemType).gameObject.BindEvent(OnDrag, Define.UIEvent.Drag);
        GetButton((int)Buttons.ShopItem_ItemType).gameObject.BindEvent(OnEndDrag, Define.UIEvent.EndDrag);
    }

    void OnButtonClicked(PointerEventData evt)
    {
        // Disable click when draging
        if (isDrag == true)
            return;

        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        // Temp
        if (itemType == 2)
            return;

        Managers.UI.ShowPopupUI<UI_PurchaseItem>();
        PlayerPrefs.SetInt("ItemType", itemType);
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
