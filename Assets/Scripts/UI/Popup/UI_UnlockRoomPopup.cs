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
        CottonValue,
        StoneValue,
        WoodValue,
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

        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OnOkayButton);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(OnCloseButton);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);

        
        GetText((int)Texts.GoldText).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.RoomLevel + 1].Gold.ToString();
        GetText((int)Texts.CottonValue).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.RoomLevel + 1].Cotton.ToString();
        GetText((int)Texts.StoneValue).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.RoomLevel + 1].Stone.ToString();
        GetText((int)Texts.WoodValue).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.RoomLevel + 1].Wood.ToString();

    }

    void OnOkayButton(PointerEventData evt)
    {
        Managers.Object.CatHouse.GetComponent<TileManager>().Open();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
