using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UnlockRoomPopup : UI_Popup
{
    enum Texts
    {
        GoldText,
    }

    enum Buttons
    {
        CloseButton,
        OkayButton,
        CancleButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.GoldText).text = "$999,999";
        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OnOkayButton);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(OnCloseButton);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnOkayButton(PointerEventData evt)
    {
        if (Managers.Game.SaveData.Gold < -1)
        {
            Debug.Log("not enough money");
            Managers.UI.ClosePopupUI();
        }
        else
        {
            
            switch(Managers.Game.SaveData.Level)
            {
                case 0:
                    Util.FindChild(Managers.Object.CatHouse.gameObject, "living_room1_Hide", recursive: true).SetActive(false);
                    Managers.UI.MakeWorldSpaceUI<UI_RestTime>();
                    break;
                case 1:
                    Util.FindChild(Managers.Object.CatHouse.gameObject, "living_room2_Hide", recursive: true).SetActive(false);
                    break;
                case 2:
                    Util.FindChild(Managers.Object.CatHouse.gameObject, "small_room_Hide", recursive: true).SetActive(false);
                    break;
            }
            Managers.Game.SaveData.Level++;
            // Unlock
            // Spend Gold
            Managers.Game.SaveData.Gold -= 0;
            // Save Data
            //Managers.Game.SaveGame();
            Managers.UI.ClosePopupUI();
        }
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
