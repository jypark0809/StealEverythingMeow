using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_TmpUp : UI_Popup
{
    enum Buttons
    {
        OkButton,

    }
    enum Texts
    {
        Ment,
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
        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnCloseButton);

        if (Managers.Game.SaveData.RoomLevel == 7)
        {
            GetText((int)Texts.Ment).text = "숨숨집 3단계를 기대해달라냥";
            return;
        }
        if ((Managers.Game.SaveData.curFurnitureCount == Managers.Game.SaveData.MaxFurniture[Managers.Game.SaveData.RoomLevel]))
            GetText((int)Texts.Ment).text = "숨숨집을 눌러 공간을 확장하라냥";
    }

    private void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
