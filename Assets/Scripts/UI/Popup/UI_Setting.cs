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
        BlogButton,
        CloseButton,
        GoldButton,
        DiaButton
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


        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
        GetButton((int)Buttons.InstagramButton).gameObject.BindEvent(InstaOpen);
        GetButton((int)Buttons.BlogButton).gameObject.BindEvent(BlogOpen);
        GetButton((int)Buttons.GoldButton).gameObject.BindEvent(OnGoldButtonClicked);
        GetButton((int)Buttons.DiaButton).gameObject.BindEvent(OnDiaButtonClicked);
    }

    void OnBgmToggleSelected(bool boolean)
    {
        Managers.Game.BGMOn = boolean;

        if (boolean)
            Managers.Sound.Play(Define.Sound.Bgm);
        else
            Managers.Sound.Stop(Define.Sound.Bgm);
    }

    void OnEffectSoundToggleSelected(bool boolean)
    {
        Managers.Game.EffectSoundOn = boolean;

        if (boolean)
        {
            Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        }
    }



    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }

    void InstaOpen(PointerEventData evt)
    {
        Application.OpenURL("https://www.instagram.com/stealeverything_meow/");
    }

    void BlogOpen(PointerEventData evt)
    {
        Application.OpenURL("https://blog.naver.com/stealeverything_meow");
    }

    void OnGoldButtonClicked(PointerEventData evt)
    {
        Managers.Game.SaveData.Gold += 100000;
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();
    }

    void OnDiaButtonClicked(PointerEventData evt)
    {
        Managers.Game.SaveData.Dia += 100000;
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();
    }
}
