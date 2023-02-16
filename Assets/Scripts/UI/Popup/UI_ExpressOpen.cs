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
    private bool ind1;
    private bool ind2;

    private TextMeshProUGUI EmotionText;
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
    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopupUI();
        }
#endif
    }
    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        EmotionText = GetText((int)Texts.RwdText);

        /*
        if(ind1 && ind2)
        {
            EmotionText.text = " '" + Managers.Data.ExpressBooks[1501 + index1].Express_Name + "' , '" + Managers.Data.ExpressBooks[1501 + index2].Express_Name + "'";

        }
        else
        {
            if (ind1)
            {
                EmotionText.text += " '" + Managers.Data.ExpressBooks[1501 + index1].Express_Name + "' ";
            }
            if (ind2)
            {
                EmotionText.text += " '" + Managers.Data.ExpressBooks[1501 + index2].Express_Name + "'";
            }
        }      
        */ 
        EmotionText.text = "새로운 감정표현을 획득했어요 \n 도감에서 확인해주세요!";
        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnCloseButton);
    }

    public void Setinfo(int _index1 ,int _index2, bool _i1, bool _i2)
    {
        index1 = _index1;
        index2 = _index2;
        ind1 = _i1;
        ind2 = _i2;
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
