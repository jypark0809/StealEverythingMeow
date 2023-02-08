using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_FindHelp : UI_Popup
{
    enum Images
    {
        Blocker,
        Image
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));

        GetImage((int)Images.Blocker).gameObject.BindEvent(OnCloseButtonClicked);
        GetImage((int)Images.Image).gameObject.BindEvent(OnCloseButtonClicked);
    }

    void OnCloseButtonClicked(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        ClosePopupUI();
    }
}
