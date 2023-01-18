using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PauseGamePopup : UI_Popup
{
    enum Buttons
    {
        CloseButton,
    }

    enum Texts
    {
        WoodText,
        RockText,
        CottonText,
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

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
        GetText((int)Texts.WoodText).text = $"x {Managers.Object.Player.Stat.Wood.ToString()}";
        GetText((int)Texts.RockText).text = $"x {Managers.Object.Player.Stat.Rock.ToString()}";
        GetText((int)Texts.CottonText).text = $"x {Managers.Object.Player.Stat.Cotton.ToString()}";
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        Time.timeScale = 1;
        Managers.UI.ClosePopupUI();
    }
}
