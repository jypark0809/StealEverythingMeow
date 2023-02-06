using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Bag : UI_Popup
{
    private string[] FoodName = { "CatnipCandy", "Churu", "Jerky", "Mackerel", "Salmon", "Tuna" };
    private bool[] CheckFood = new bool[6];

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

        SetInven();
        SetFood();

        GetButton((int)Buttons.InvenButton).gameObject.BindEvent(OpenInven);
        GetButton((int)Buttons.FoodButton).gameObject.BindEvent(OpenFood);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);

        GetButton((int)Buttons.InvenButton).interactable = false;
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
    void SetInven()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.Inven);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        //실제 정보 참조
        UI_Inven_Item Item1 = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform);
        Item1.SetInfo("Wood", Managers.Game.SaveData.Wood);

        UI_Inven_Item Item2 = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform);
        Item2.SetInfo("Rock", Managers.Game.SaveData.Stone);

        UI_Inven_Item Item3 = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform);
        Item3.SetInfo("Cotton", Managers.Game.SaveData.Cotton);


    }
    void SetFood()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.Food);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < 6; i++)
        {
            UI_Food_Item Item1 = Managers.UI.MakeSubItem<UI_Food_Item>(gridPanel.transform);
            Item1.SetInfo(FoodName[i], i);

        }
    }

}
