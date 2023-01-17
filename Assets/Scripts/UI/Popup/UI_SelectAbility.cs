using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SelectAbility : UI_Popup
{
    enum Buttons
    {
        MoveSpeedButton,
        SightRangeButton,
        MagnetRangeButton,
    }

    enum Texts
    {
        SpeedLevelText,
        SightLevelText,
        MagnetLevelText
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

       
        if (Managers.Object.Player.Stat.SpeedLv > 5)
        {
            GetButton((int)Buttons.MoveSpeedButton).interactable = false;
            GetText((int)Texts.SpeedLevelText).text = "Lv Max";
        }
        else
        {
            GetText((int)Texts.SpeedLevelText).text =
                $"Lv {Managers.Object.Player.Stat.SpeedLv} >> Lv {Managers.Object.Player.Stat.SpeedLv + 1}";
        }
        

        GetButton((int)Buttons.MoveSpeedButton).gameObject.BindEvent(OnMoveSpeedButtonClicked);
        GetButton((int)Buttons.SightRangeButton).gameObject.BindEvent(OnSightRangeButtonClicked);
        GetButton((int)Buttons.MagnetRangeButton).gameObject.BindEvent(OnMagnetRangeButtonClicked);

        
    }

    void OnMoveSpeedButtonClicked(PointerEventData evt)
    {
        if(GetButton((int)Buttons.MoveSpeedButton).interactable)
        {
            Managers.Object.Player.Stat.SpeedLv++;
            Time.timeScale = 1;
            Managers.UI.ClosePopupUI();
        }
    }

    void OnSightRangeButtonClicked(PointerEventData evt)
    {
        Time.timeScale = 1;
        Managers.UI.ClosePopupUI();
    }

    void OnMagnetRangeButtonClicked(PointerEventData evt)
    {
        Time.timeScale = 1;
        Managers.UI.ClosePopupUI();
    }
}
