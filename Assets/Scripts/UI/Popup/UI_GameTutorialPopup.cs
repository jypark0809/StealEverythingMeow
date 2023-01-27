using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameTutorialPopup : UI_Popup
{
    int _index = 0;
    int Index
    {
        get { return _index; }
        set
        {
            _index = value;

            if (_index == 0)
            {
                _leftButton.SetActive(false);
                _rightButton.SetActive(true);
            }
            else if (_index == _images.Length - 1)
            {
                _leftButton.SetActive(true);
                _rightButton.SetActive(false);
            }
            else
            {
                _leftButton.SetActive(true);
                _rightButton.SetActive(true);
            }
        }
    }
    public Image[] _images;

    GameObject _leftButton;
    GameObject _rightButton;

    enum GameObjects
    {
        LeftButton,
        RightButton,
        CloseButton
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        _leftButton = GetObject((int)GameObjects.LeftButton);
        _rightButton = GetObject((int)GameObjects.RightButton);
        _leftButton.SetActive(false);

        _images[Index].gameObject.SetActive(true);

        _leftButton.BindEvent(OnLeftButtonClicked);
        _rightButton.BindEvent(OnRightButtonClicked);
        GetObject((int)GameObjects.CloseButton).BindEvent(OnCloseButtonClicked);
    }

    void OnLeftButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        _images[Index].gameObject.SetActive(false);
        Index--;
        _images[Index].gameObject.SetActive(true);
    }

    void OnRightButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        _images[Index].gameObject.SetActive(false);
        Index++;
        _images[Index].gameObject.SetActive(true);
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Time.timeScale = 1;
        Managers.Game.SaveGame();
        Managers.UI.ClosePopupUI();
    }
}
