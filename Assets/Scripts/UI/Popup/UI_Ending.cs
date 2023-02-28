using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Ending : UI_Popup
{
    enum Texts
    {
    }
    enum Buttons
    {
        OkButton,
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
        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnCloseButton);
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.Scene.LoadScene(Define.SceneType.EndingScene);
        Managers.UI.ClosePopupUI();
    }
}
