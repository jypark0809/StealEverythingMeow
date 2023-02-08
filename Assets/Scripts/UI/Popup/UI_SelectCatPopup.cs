using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SelectCatPopup : UI_Popup
{
    enum Toggles
    {
        WhiteToggle,
        BlackToggle,
        CalicoToggle,
        TabbyToggle,
        GrayToggle,
    }

    enum Images
    {
        WhiteImage,
        BlackImage,
        CalicoImage,
        TabbyImage,
        GrayImage,
    }

    enum Buttons
    {
        StartButton,
        CloseButton
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(Toggles));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        GetObject((int)Toggles.WhiteToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnWhiteToggleSelected);
        GetObject((int)Toggles.BlackToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnBlackToggleSelected);
        GetObject((int)Toggles.CalicoToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnCalicoToggleSelected);
        GetObject((int)Toggles.TabbyToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnTabbyToggleSelected);
        GetObject((int)Toggles.GrayToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnGrayToggleSelected);
        GetObject((int)Toggles.WhiteToggle).GetComponent<Toggle>().isOn = true;
        PlayerPrefs.SetInt("SelectedCatNum", 0);

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnStartButtonClicked);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
    }

    #region EventHandler
    void OnWhiteToggleSelected(bool boolean)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        if (boolean)
        {
            GetImage((int)Images.WhiteImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectedCatNum", 0);
            Debug.Log(PlayerPrefs.GetInt("SelectedCatNum"));
        }
        else
        {
            GetImage((int)Images.WhiteImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }

    void OnBlackToggleSelected(bool boolean)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        if (boolean)
        {
            GetImage((int)Images.BlackImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectedCatNum", 1);
        }
        else
        {
            GetImage((int)Images.BlackImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }

    void OnCalicoToggleSelected(bool boolean)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        if (boolean)
        {
            GetImage((int)Images.CalicoImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectedCatNum", 2);
        }
        else
        {
            GetImage((int)Images.CalicoImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }

    void OnTabbyToggleSelected(bool boolean)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        if (boolean)
        {
            GetImage((int)Images.TabbyImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectedCatNum", 3);
        }
        else
        {
            GetImage((int)Images.TabbyImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }

    void OnGrayToggleSelected(bool boolean)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        if (boolean)
        {
            GetImage((int)Images.GrayImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectedCatNum", 4);
        }
        else
        {
            GetImage((int)Images.GrayImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }
    #endregion

    void OnStartButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        //LoadingScene.LoadScene("GameScene", true);
        Managers.Scene.LoadScene(Define.SceneType.GameScene);
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Managers.UI.ClosePopupUI();
    }
}
