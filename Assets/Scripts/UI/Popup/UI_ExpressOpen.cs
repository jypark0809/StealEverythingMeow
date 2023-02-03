using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_ExpressOpen : UI_Popup
{
    private int index1;
    private int index2;
    enum Texts
    {
        RwdText
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

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        GetText((int)Texts.RwdText).text = " ' " + Managers.Data.ExpressBooks[1501 + index1].Express_Name + " ' 와 ' " + Managers.Data.ExpressBooks[1501 + index2].Express_Name + "'를 획득했어요 \n 도감에서 확인해주세요!";
        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnCloseButton);
    }

    public void Setinfo(int _index1 ,int _index2)
    {
        index1 = _index1;
        index2 = _index2;
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();

    }
}
