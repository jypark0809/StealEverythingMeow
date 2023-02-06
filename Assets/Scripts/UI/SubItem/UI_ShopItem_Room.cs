using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem_Room : UI_Base
{
    GameObject _blocker;

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

        GetButton((int)Buttons.ShopItem_Room).gameObject.BindEvent(OnButtonClicked);

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
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        if (GetButton((int)Buttons.ShopItem_Room).interactable)
        {
            Managers.UI.ShowPopupUI<UI_PurchaseFurniture>();
            PlayerPrefs.SetInt("SpaceLevel", spaceLevel);
        }
        else
        {
            Debug.Log("Need to upgrade space Lv");
        }
    }
}
