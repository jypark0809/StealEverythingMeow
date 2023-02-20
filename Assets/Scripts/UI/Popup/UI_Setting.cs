using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Setting : UI_Popup
{
    enum GameObjects
    {
        BgmToggle,
        EffectSoundToggle,
        InputField
    }
    enum Buttons
    {
        InstagramButton,
        NaverBlogButton,
        HelpButton,
        CouponButton,
        CloseButton
    }

    enum Texts
    {
        CouponText
    }

    void Start()
    {
        Init();
    }


    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetObject((int)GameObjects.BgmToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnBgmToggleSelected);
        GetObject((int)GameObjects.BgmToggle).GetComponent<Toggle>().isOn = Managers.Game.BGMOn;
        GetObject((int)GameObjects.EffectSoundToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnEffectSoundToggleSelected);
        GetObject((int)GameObjects.EffectSoundToggle).GetComponent<Toggle>().isOn = Managers.Game.EffectSoundOn;

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
        GetButton((int)Buttons.InstagramButton).gameObject.BindEvent(OnInstagramButtonClicked);
        GetButton((int)Buttons.NaverBlogButton).gameObject.BindEvent(OnNaverBlogButtonClicked);
        GetButton((int)Buttons.HelpButton).gameObject.BindEvent(OnHelpButtonClicked);
        GetButton((int)Buttons.CouponButton).gameObject.BindEvent(OnCouponButtonClicked);
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopupUI();
        }
#endif
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
        Managers.UI.ShowPopupUI<UI_TutorialPopup>();
    }

    void OnCouponButtonClicked(PointerEventData evt)
    {
        string couponNum = "ILOVESTEALEVERYTHINGMEOW";
        // string inputText = GetText((int)Texts.CouponText).text;
        string inputText = GetObject((int)GameObjects.InputField).GetComponent<TMP_InputField>().text;
        Debug.Log($"couponNum : {couponNum}");
        Debug.Log($"inputText : {inputText}");
        if (couponNum.Equals(inputText))
        {
            // 1È¸ Á¦ÇÑ
            if (PlayerPrefs.HasKey("ILoveCoupon"))
            {
                Managers.UI.ShowPopupUI<UI_UsedCouponPopup>();
                return;
            }

            // TODO
            Managers.Game.SaveData.Gold += 15000;
            Managers.Game.SaveData.Wood += 320;
            Managers.Game.SaveData.Stone += 150;
            Managers.Game.SaveData.Cotton += 20;

            // Refresh UI
            (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();

            // Save Data
            Managers.Game.SaveGame();

            PlayerPrefs.SetInt("ILoveCoupon", 1);
        }
        else
        {
            // UI_WrongCouponPopup
            Managers.UI.ShowPopupUI<UI_WrongCouponPopup>();
        }
    }
}
