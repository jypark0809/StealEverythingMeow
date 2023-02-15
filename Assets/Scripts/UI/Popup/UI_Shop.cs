using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    GameObject _contentRoom;
    GameObject _contentItem;

    enum GameObjects
    {
        ScrollViewPanel,
        RoomToggle,
        ItemToggle,
        Content_Room,
        Content_Item,
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
        GetObject((int)GameObjects.ItemToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnItemToggleSelected);
        _contentRoom = GetObject((int)GameObjects.Content_Room);
        _contentItem = GetObject((int)GameObjects.Content_Item);
        _contentItem.SetActive(false);
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

    #region Toggle EventHandler
    void OnRoomToggleSelected(bool boolean)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
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
    void OnItemToggleSelected(bool boolean)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        if (boolean)
        {
            GetObject((int)GameObjects.Content_Item).SetActive(boolean);
            GetObject((int)GameObjects.ScrollViewPanel).GetComponent<ScrollRect>().content = _contentItem.GetComponent<RectTransform>();
        }
        else
        {
            GetObject((int)GameObjects.Content_Item).SetActive(boolean);
        }
    }
    void OnCloseButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        ClosePopupUI();
    }
    #endregion
}
