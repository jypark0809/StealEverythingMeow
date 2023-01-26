using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Bag : UI_Popup
{
    enum GameObjects
    {
        Inven,
        Food,

    }
    enum Buttons
    {
        InvenButton,
        FoodButton,
        CloseButton,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.Inven);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        //실제 정보 참조
        if (Managers.Game.SaveData.Cotton != 0)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_Inven_Item");
            Item.transform.SetParent(gridPanel.transform);
            UI_Inven_Item inven_item = Util.GetOrAddComponent<UI_Inven_Item>(Item);
            inven_item.SetInfo("Cotton", Managers.Game.SaveData.Cotton);
        }
        if (Managers.Game.SaveData.Wood != 0)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_Inven_Item");
            Item.transform.SetParent(gridPanel.transform);
            UI_Inven_Item inven_item = Util.GetOrAddComponent<UI_Inven_Item>(Item);
            inven_item.SetInfo("Wood", Managers.Game.SaveData.Wood);
        }
        if (Managers.Game.SaveData.Stone != 0)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_Inven_Item");
            Item.transform.SetParent(gridPanel.transform);
            UI_Inven_Item inven_item = Util.GetOrAddComponent<UI_Inven_Item>(Item);
            inven_item.SetInfo("Stone", Managers.Game.SaveData.Stone);
        }


        GetButton((int)Buttons.InvenButton).gameObject.BindEvent(OpenInven);
        GetButton((int)Buttons.FoodButton).gameObject.BindEvent(OpenFood);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);

    }

    void OpenFood(PointerEventData evt)
    {
        Get<GameObject>((int)GameObjects.Food).SetActive(true);
        Get<GameObject>((int)GameObjects.Inven).SetActive(false);
        GetButton((int)Buttons.FoodButton).interactable = false;
        GetButton((int)Buttons.InvenButton).interactable = true;
    }
    void OpenInven(PointerEventData evt)
    {
        Get<GameObject>((int)GameObjects.Food).SetActive(false);
        Get<GameObject>((int)GameObjects.Inven).SetActive(true);
        GetButton((int)Buttons.InvenButton).interactable = false;
        GetButton((int)Buttons.FoodButton).interactable = true;
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
