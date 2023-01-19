using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CatHouseSceneBottom : UI_Base
{
    enum Buttons
    {
        StartGameButton
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.StartGameButton).gameObject.BindEvent(OnStartButtonClicked);
    }

    void OnStartButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Managers.UI.ShowPopupUI<UI_SelectCatPopup>();
    }
}
