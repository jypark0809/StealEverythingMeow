using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class UI_DiaUp : UI_Popup
{
    enum Texts
    {
        Ment,
    }
    enum Buttons
    {
        OkButton,
        CancelButton
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

        GetText((int)Texts.Ment).text = $"���̾Ƹ� <color=red>{Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Diamond}</color>�� �Ҹ��� " + System.Environment.NewLine + "������ ������ ���׷��̵� �ϰڽ��ϱ�?";

        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnOkeButton);
        GetButton((int)Buttons.CancelButton).gameObject.BindEvent(OnCloseButton);
    }

    private void OnOkeButton(PointerEventData evt)
    {
        Util.FindChild(Managers.Object.CatHouse.gameObject, "Soom", true).GetComponent<Soom>().SomUpgrade();
        Managers.UI.CloseAllPopupUI();
    }
    private void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.CloseAllPopupUI();
    }
}