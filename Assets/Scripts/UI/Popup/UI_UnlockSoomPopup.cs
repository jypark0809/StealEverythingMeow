using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UnlockSoomPopup : UI_Popup
{
    enum GameObjects
    {
        Upgrade,
        Full,
    }

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
        OkayButton,
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
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        if (Managers.Game.SaveData.SoomLevel == 3)
        {
            GetObject((int)GameObjects.Upgrade).gameObject.SetActive(false);
            GetObject((int)GameObjects.Full).gameObject.SetActive(true);
        }
        else
        {
            GetObject((int)GameObjects.Upgrade).gameObject.SetActive(true);
            GetObject((int)GameObjects.Full).gameObject.SetActive(false);

            GetText((int)Texts.CurrentSoomLevel).text = "Lv " + Managers.Game.SaveData.SoomLevel.ToString();
            GetText((int)Texts.CurText).text = "Lv " + Managers.Game.SaveData.SoomLevel.ToString();
            GetText((int)Texts.NextText).text = "Lv " + (Managers.Game.SaveData.SoomLevel + 1).ToString();

            GetImage((int)Images.CurImages).sprite = Resources.Load<Sprite>(("Sprites/Furniture/Soom/" + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel].Soom_Int_Name).Split(".")[0]);
            GetImage((int)Images.NextImages).sprite = Resources.Load<Sprite>(("Sprites/Furniture/Soom/" + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Soom_Int_Name).Split(".")[0]);

            GetText((int)Texts.B1).text = "- 고양이 수용량 +2";
            GetText((int)Texts.B2).text = "- 공간해금 구간 확장";
            GetText((int)Texts.B3).text = "- 행복도 + " + (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Happiness).ToString();
            GetText((int)Texts.B4).text = "- 감정표현 1종 획득";

            GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OnOpenUpCon);
        }
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnOpenUpCon(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_UpgradeSoomPopUp>();
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
