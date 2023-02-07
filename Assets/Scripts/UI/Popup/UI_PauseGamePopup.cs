using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PauseGamePopup : UI_Popup
{
    enum Toggles
    {
        BgmToggle,
        EffectSoundToggle
    }

    enum Buttons
    {
        CloseButton,
        MainMenuButton,
        HelpButton
    }

    enum Texts
    {
        WoodText,
        RockText,
        CottonText,
        SpeedText,
        SightText,
        MagnetText
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
        Bind<GameObject>(typeof(Toggles));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
        GetButton((int)Buttons.MainMenuButton).gameObject.BindEvent(OnMainMenuButtonClicked);
        GetButton((int)Buttons.HelpButton).gameObject.BindEvent(OnHelpButtonButtonClicked);

        GetObject((int)Toggles.BgmToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnBgmToggleSelected);
        GetObject((int)Toggles.BgmToggle).GetComponent<Toggle>().isOn = Managers.Game.BGMOn;
        GetObject((int)Toggles.EffectSoundToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnEffectSoundToggleSelected);
        GetObject((int)Toggles.EffectSoundToggle).GetComponent<Toggle>().isOn = Managers.Game.EffectSoundOn;


        GetText((int)Texts.WoodText).text = Managers.Object.Player.Stat.Wood.ToString();
        GetText((int)Texts.RockText).text = Managers.Object.Player.Stat.Rock.ToString();
        GetText((int)Texts.CottonText).text = Managers.Object.Player.Stat.Cotton.ToString();
        GetText((int)Texts.SpeedText).text = $"Lv. {Managers.Object.Player.Stat.SpeedLv.ToString()}";
        GetText((int)Texts.SightText).text = $"Lv. {Managers.Object.Player.Stat.CooltimeLv.ToString()}";
        GetText((int)Texts.MagnetText).text = $"Lv. {Managers.Object.Player.Stat.MagnetLv.ToString()}";

    }

    #region EventHandler
    void OnBgmToggleSelected(bool boolean)
    {
        Managers.Game.BGMOn = boolean;

        if (boolean)
            Managers.Sound.Play(Define.Sound.Bgm);
        else
            Managers.Sound.Stop(Define.Sound.Bgm);

        Managers.Game.SaveGame();
    }

    void OnEffectSoundToggleSelected(bool boolean)
    {
        Managers.Game.EffectSoundOn = boolean;

        if (boolean)
        {
            Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        }

        Managers.Game.SaveGame();
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Time.timeScale = 1;
        Managers.UI.ClosePopupUI();
    }

    void OnMainMenuButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Time.timeScale = 1;
        Managers.Scene.LoadScene(Define.SceneType.CatHouseScene);
    }

    void OnHelpButtonButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Managers.UI.ShowPopupUI<UI_GameTutorialPopup>();
    }
    #endregion
}
