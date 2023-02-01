using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Stat : UI_Popup
{
    enum GameObjects
    {
        CatStat,
        CatExpress
    }
    enum Buttons
    {
        CloseButton,
    }
    enum Images
    {
        Cat,
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


        SetCat();
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();

    }
    void SetCat()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.CatStat);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < 5; i++)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_CatSet");
            Item.transform.SetParent(gridPanel.transform);
            UI_CatSet inven_Food = Util.GetOrAddComponent<UI_CatSet>(Item);
            inven_Food.SetInfo(i);
        }
    }
}