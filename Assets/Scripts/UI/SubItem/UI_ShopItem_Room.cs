using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem_Room : UI_Base
{
    public ScrollRect _scrollRect;
    GameObject _blocker;
    bool isDrag = false;

    [SerializeField]
    int spaceLevel;
    
    enum GameObjects
    {
        Blocker
    }

    enum Buttons
    {
        ShopItem_Room,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        _blocker = GetObject((int)GameObjects.Blocker);

        GetButton((int)Buttons.ShopItem_Room).gameObject.BindEvent(OnButtonClicked, Define.UIEvent.Click);
        GetButton((int)Buttons.ShopItem_Room).gameObject.BindEvent(OnBeginDrag, Define.UIEvent.BeginDrag);
        GetButton((int)Buttons.ShopItem_Room).gameObject.BindEvent(OnDrag, Define.UIEvent.Drag);
        GetButton((int)Buttons.ShopItem_Room).gameObject.BindEvent(OnEndDrag, Define.UIEvent.EndDrag);

        OpenShopItem();
    }

    void OpenShopItem()
    {
        if (Managers.Game.SaveData.SpaceLevel >= spaceLevel)
        {
            _blocker.SetActive(false);
            GetButton((int)Buttons.ShopItem_Room).interactable = true;
        }
    }

    void OnButtonClicked(PointerEventData evt)
    {
        // Disable click when draging
        if (isDrag == true)
            return;

        if (GetButton((int)Buttons.ShopItem_Room).interactable)
        {
            Managers.UI.ShowPopupUI<UI_PurchaseFurniture>();
            PlayerPrefs.SetInt("SpaceLevel", spaceLevel);
        }
        else
            Debug.Log("Need to upgrade space Lv");
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
