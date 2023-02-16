using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PurchaseFurniture : UI_Popup
{
    // int _curFurnitureCount; // 현재 방에 있는 가구 개수

    enum GameObjects
    {
        Blocker,
        Content,
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

        GetObject((int)GameObjects.Blocker).BindEvent(OnCloseButtonClicked);
        GetObject((int)GameObjects.CloseButton).BindEvent(OnCloseButtonClicked);
        
        Transform parent = GetObject((int)GameObjects.Content).transform;

        for (int i = 0; i < Managers.Data.Furnitures.Count; i++)
        {
            FurnitureData fData;
            Managers.Data.Furnitures.TryGetValue(i + 1101, out fData);

            if (fData.F_Space_Num == PlayerPrefs.GetInt("SpaceLevel"))
            {
                UI_ShopItem_Furniture item = Managers.UI.MakeSubItem<UI_ShopItem_Furniture>(parent.transform);
                item.SetInfo(fData);
                
                if (Managers.Game.SaveData.FList.Contains(fData))
                {
                    item.isPurchasable = false;
                }
                else
                {
                    item.isPurchasable = true;
                }
            }
        }
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

    void OnCloseButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        ClosePopupUI();
    }
}
