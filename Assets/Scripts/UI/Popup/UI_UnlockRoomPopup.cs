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
            // Unlock
            Util.FindChild(Managers.Object.CatHouse.gameObject, "LobbyHide", recursive: true).SetActive(false);

            // Spend Gold
            Managers.Game.SaveData.Gold -= 0;

            // Save Data
            Managers.Game.SaveGame();

            Managers.UI.ClosePopupUI();
        }
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
