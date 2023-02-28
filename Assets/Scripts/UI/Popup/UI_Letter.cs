using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class UI_Letter : UI_Popup
{
    string _Catname ="";
    int _CatIndex;
    enum Texts
    {
        CatText
    }
    enum Buttons
    {
        OkButton,
        CloseButton,
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

        GetText((int)Texts.CatText).text = $"{_Catname}의 속마음 일기가 도착했습니다. \n 확인하시겠습니까?";

        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnOpenEvent);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnOpenEvent(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_HappinessStory>().SetInfo(_CatIndex);
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
    public void SetInfo(string CatName, int i)
    {
        _Catname = CatName;
        _CatIndex = i;
    }
}
