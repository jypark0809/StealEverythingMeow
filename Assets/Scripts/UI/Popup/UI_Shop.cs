using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    GameObject _contentRoom;
    GameObject _contentSnack;
    GameObject _contentDiamond;

    enum GameObjects
    {
        ScrollViewPanel,
        RoomToggle,
        SnackToggle,
        DiamondToggle,
        Content_Room,
        Content_Snack,
        Content_Diamond,
        CloseButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.CloseButton).BindEvent(OnCloseButtonClicked);
        GetObject((int)GameObjects.RoomToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnRoomToggleSelected);
        GetObject((int)GameObjects.SnackToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnSnackToggleSelected);
        GetObject((int)GameObjects.DiamondToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnDiamondToggleSelected);
        _contentRoom = GetObject((int)GameObjects.Content_Room);
        _contentSnack = GetObject((int)GameObjects.Content_Snack);
        _contentDiamond = GetObject((int)GameObjects.Content_Diamond);
        _contentSnack.SetActive(false);
        _contentDiamond.SetActive(false);
    }

    #region Toggle EventHandler
    void OnRoomToggleSelected(bool boolean)
    {
        if (boolean)
        {
            GetObject((int)GameObjects.Content_Room).SetActive(boolean);
            GetObject((int)GameObjects.ScrollViewPanel).GetComponent<ScrollRect>().content = _contentRoom.GetComponent<RectTransform>();
        }
        else
        {
            GetObject((int)GameObjects.Content_Room).SetActive(boolean);
        }
    }
    void OnSnackToggleSelected(bool boolean)
    {
        if (boolean)
        {
            GetObject((int)GameObjects.Content_Snack).SetActive(boolean);
            GetObject((int)GameObjects.ScrollViewPanel).GetComponent<ScrollRect>().content = _contentSnack.GetComponent<RectTransform>();
        }
        else
        {
            GetObject((int)GameObjects.Content_Snack).SetActive(boolean);
        }
    }
    void OnDiamondToggleSelected(bool boolean)
    {
        if (boolean)
        {
            GetObject((int)GameObjects.Content_Diamond).SetActive(boolean);
            GetObject((int)GameObjects.ScrollViewPanel).GetComponent<ScrollRect>().content = _contentDiamond.GetComponent<RectTransform>();

        }
        else
        {
            GetObject((int)GameObjects.Content_Diamond).SetActive(boolean);
        }
    }
    void OnCloseButtonClicked(PointerEventData evt)
    {
        ClosePopupUI();
    }
    #endregion


}
