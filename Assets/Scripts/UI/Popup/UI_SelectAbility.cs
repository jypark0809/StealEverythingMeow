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

        #region Init Text
        if (Managers.Object.Player.Stat.SpeedLv > Managers.Data.StatSpeeds.Count - 1)
        {
            GetButton((int)Buttons.MoveSpeedButton).interactable = false;
            GetText((int)Texts.SpeedLevelText).text = "Lv Max";
        }
        else
        {
            GetText((int)Texts.SpeedLevelText).text =
                $"Lv {Managers.Object.Player.Stat.SpeedLv} >> Lv {Managers.Object.Player.Stat.SpeedLv + 1}";
        }

        if (Managers.Object.Player.Stat.SightLv > Managers.Data.StatSights.Count - 1)
        {
            GetButton((int)Buttons.SightRangeButton).interactable = false;
            GetText((int)Texts.SightLevelText).text = "Lv Max";
        }
        else
        {
            GetText((int)Texts.SightLevelText).text =
                $"Lv {Managers.Object.Player.Stat.SightLv} >> Lv {Managers.Object.Player.Stat.SightLv + 1}";
        }

        if (Managers.Object.Player.Stat.MagnetLv > Managers.Data.StatMagnets.Count - 1)
        {
            GetButton((int)Buttons.MagnetRangeButton).interactable = false;
            GetText((int)Texts.MagnetLevelText).text = "Lv Max";
        }
        else
        {
            GetText((int)Texts.MagnetLevelText).text =
                $"Lv {Managers.Object.Player.Stat.MagnetLv} >> Lv {Managers.Object.Player.Stat.MagnetLv + 1}";
        }
        #endregion

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
        if (GetButton((int)Buttons.SightRangeButton).interactable)
        {
            Managers.Object.Player.Stat.SightLv++;
            Time.timeScale = 1;
            Managers.UI.ClosePopupUI();
        }
    }

    void OnMagnetRangeButtonClicked(PointerEventData evt)
    {
        if (GetButton((int)Buttons.MagnetRangeButton).interactable)
        {
            Managers.Object.Player.Stat.MagnetLv++;
            Time.timeScale = 1;
            Managers.UI.ClosePopupUI();
        }
    }
}
