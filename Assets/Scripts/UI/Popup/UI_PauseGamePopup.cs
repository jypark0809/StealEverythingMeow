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
        Bind<GameObject>(typeof(Toggles));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
        GetObject((int)Toggles.BgmToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnBgmToggleSelected);
        GetObject((int)Toggles.BgmToggle).GetComponent<Toggle>().isOn = Managers.Game.BGMOn;
        GetObject((int)Toggles.EffectSoundToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnEffectSoundToggleSelected);
        GetObject((int)Toggles.EffectSoundToggle).GetComponent<Toggle>().isOn = Managers.Game.BGMOn;


        GetText((int)Texts.WoodText).text = $"x {Managers.Object.Player.Stat.Wood.ToString()}";
        GetText((int)Texts.RockText).text = $"x {Managers.Object.Player.Stat.Rock.ToString()}";
        GetText((int)Texts.CottonText).text = $"x {Managers.Object.Player.Stat.Cotton.ToString()}";
    }

    #region EventHandler
    void OnBgmToggleSelected(bool boolean)
    {
        Managers.Game.BGMOn = boolean;

        if (boolean)
            Managers.Sound.Play(Define.Sound.Bgm);
        else
            Managers.Sound.Stop(Define.Sound.Bgm);

        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
    }

    void OnEffectSoundToggleSelected(bool boolean)
    {
        Managers.Game.EffectSoundOn = boolean;

        if (boolean)
            Managers.Sound.Play(Define.Sound.Effect);
        else
            Managers.Sound.Stop(Define.Sound.Effect);

        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Time.timeScale = 1;
        Managers.UI.ClosePopupUI();
    }
    #endregion
}
