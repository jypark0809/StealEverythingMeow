using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UpgradeSom : UI_Popup
{
    enum Texts
    {
        CurrentSoomLevel,
        CurText,
        NextText,
        B1,
        B2,
        B3,
        B4,
    }

    enum Images
    {
        CurImages,
        NextImages
    }
    enum Buttons
    {
        CloseButton,
        OkayButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.CurText).text = "Lv " + Managers.Game.SaveData.SoomLevel.ToString();
        GetText((int)Texts.NextText).text = "Lv " + (Managers.Game.SaveData.SoomLevel + 1).ToString();

        GetImage((int)Images.CurImages).sprite = Resources.Load<Sprite>(("Sprites/Furniture/Soom/" + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel].Soom_Int_Name).Split(".")[0]);
        GetImage((int)Images.NextImages).sprite = Resources.Load<Sprite>(("Sprites/Furniture/Soom/" + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Soom_Int_Name).Split(".")[0]);

        GetText((int)Texts.B1).text = "- ����� ���뷮 +2";
        GetText((int)Texts.B2).text = "- �����ر� ���� Ȯ��";
        GetText((int)Texts.B3).text = "- �ູ�� + " + (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Happiness).ToString();
        GetText((int)Texts.B4).text = "- ����ǥ�� 1�� ȹ��";

        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OnOpenUpCon);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnOpenUpCon(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Condition>();
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
