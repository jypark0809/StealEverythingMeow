using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Bag : UI_Popup
{
    enum GameObjects
    {
        GridPanel,

    }

    enum Buttons
    {
        CloseButton
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

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        /*
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);
        //실제 정보 참조
        for(int i = 0; i <6; i++)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_Inven_Item");
            Item.transform.SetParent(gridPanel.transform);
            UI_Inven_Item inven_item = Util.GetOrAddComponent<UI_Inven_Item>(Item);
            inven_item.SetInfo("asd1");
        }
        */



        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
