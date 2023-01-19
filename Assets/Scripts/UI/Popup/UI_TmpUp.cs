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
            GetText((int)Texts.Ment).text = "������ 3�ܰ踦 ����ش޶��";
            return;
        }
        if ((Managers.Game.SaveData.curFurnitureCount == Managers.Game.SaveData.MaxFurniture[Managers.Game.SaveData.RoomLevel]))
            GetText((int)Texts.Ment).text = "�������� ���� ������ Ȯ���϶��";
    }

    private void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
