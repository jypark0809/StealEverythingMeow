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
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(Toggles));
        Bind<Image>(typeof(Images));

        GetObject((int)Toggles.WhiteToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnWhiteToggleSelected);
        GetObject((int)Toggles.BlackToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnBlackToggleSelected);
        GetObject((int)Toggles.CalicoToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnCalicoToggleSelected);
        GetObject((int)Toggles.TabbyToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnTabbyToggleSelected);
        GetObject((int)Toggles.GrayToggle).GetComponent<Toggle>().onValueChanged.AddListener(OnGrayToggleSelected);
        GetObject((int)Toggles.WhiteToggle).GetComponent<Toggle>().isOn = true;

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnStartButtonClicked);
    }

    #region Toggle
    void OnWhiteToggleSelected(bool boolean)
    {
        if (boolean)
        {
            GetImage((int)Images.WhiteImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectCat", 0);
        }
        else
        {
            GetImage((int)Images.WhiteImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }

    void OnBlackToggleSelected(bool boolean)
    {
        if (boolean)
        {
            GetImage((int)Images.BlackImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectCat", 1);
        }
        else
        {
            GetImage((int)Images.BlackImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }

    void OnCalicoToggleSelected(bool boolean)
    {
        if (boolean)
        {
            GetImage((int)Images.CalicoImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectCat", 2);
        }
        else
        {
            GetImage((int)Images.CalicoImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }

    void OnTabbyToggleSelected(bool boolean)
    {
        if (boolean)
        {
            GetImage((int)Images.TabbyImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectCat", 3);
        }
        else
        {
            GetImage((int)Images.TabbyImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }

    void OnGrayToggleSelected(bool boolean)
    {
        if (boolean)
        {
            GetImage((int)Images.GrayImage).gameObject.GetComponent<Animator>().Play("Selected");
            PlayerPrefs.SetInt("SelectCat", 4);
        }
        else
        {
            GetImage((int)Images.GrayImage).gameObject.GetComponent<Animator>().Play("Unselected");
        }
    }
    #endregion

    void OnStartButtonClicked(PointerEventData evt)
    {
        // Load

        Managers.Scene.LoadScene(Define.SceneType.GameScene);
    }
}
