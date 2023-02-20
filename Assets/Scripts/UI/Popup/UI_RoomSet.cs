using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_RoomSet : UI_Popup
{
    enum GameObjects
    {
        RoomContent
    }
    enum Buttons
    {
        CloseButton,
    }

    void Start()
    {
        Init();
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

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        SetRooms();

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    private int index = 0;
    void SetRooms()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.RoomContent);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i <9; i++)
        {
            index++;
            UI_Room Item = Managers.UI.MakeSubItem<UI_Room>(gridPanel.transform);
            Item.SetInfo(i);
        }
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
