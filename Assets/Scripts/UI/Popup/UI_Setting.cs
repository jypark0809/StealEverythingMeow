using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UI_Setting : UI_Popup
{

    enum Toggles
    {
        BgmToggle,
        EffectSoundToggle
    }
    enum Buttons
    {
        InstagramButton,
        NaverBlogButton,
        HelpButton,
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
        Bind<GameObject>(typeof(Toggles));

        GetObject((int)Toggles.BgmToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnBgmToggleSelected);
        GetObject((int)Toggles.BgmToggle).GetComponent<Toggle>().isOn = Managers.Game.BGMOn;
        GetObject((int)Toggles.EffectSoundToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnEffectSoundToggleSelected);
        GetObject((int)Toggles.EffectSoundToggle).GetComponent<Toggle>().isOn = Managers.Game.EffectSoundOn;

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
        GetButton((int)Buttons.InstagramButton).gameObject.BindEvent(OnInstagramButtonClicked);
        GetButton((int)Buttons.NaverBlogButton).gameObject.BindEvent(OnNaverBlogButtonClicked);
        GetButton((int)Buttons.HelpButton).gameObject.BindEvent(OnHelpButtonClicked);
    }

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
        Managers.UI.ClosePopupUI();
    }

    void OnInstagramButtonClicked(PointerEventData evt)
    {
        Application.OpenURL("https://www.instagram.com/stealeverything_meow/");
    }

    void OnNaverBlogButtonClicked(PointerEventData evt)
    {
        Application.OpenURL("https://blog.naver.com/stealeverything_meow");
    }

    void OnHelpButtonClicked(PointerEventData evt)
    {
        // Help
    }
}
