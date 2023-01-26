using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Stat : UI_Popup
{
    enum Buttons
    {
        CloseButton,
        RightButton,
        LeftButton

    }
    enum Images
    {
        Cat,
    }
    enum Texts
    {
        SkillText,
        Name,
    }
    void Start()
    {
        Init();
    }


    int Catindex =1;

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);

        GetButton((int)Buttons.RightButton).gameObject.BindEvent(RightIndex);
        GetButton((int)Buttons.LeftButton).gameObject.BindEvent(LeftIndex);

        if(Managers.Data.CatBooks[1400 + Catindex].Cat_Skill_Name == "Null")
        {
            GetText((int)Texts.SkillText).text = "";
        }
        else
        {
            GetText((int)Texts.SkillText).text = Managers.Data.CatBooks[1400 + Catindex].Cat_Skill_Name;
        }
        GetText((int)Texts.Name).text = Managers.Data.CatBooks[1400 + Catindex].Cat_Name;
        GetImage((int)Images.Cat).sprite = Resources.Load<Sprite>(("Sprites/"+Managers.Data.CatBooks[1400+Catindex].Cat_Int_Name));
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();

    }

    void RightIndex(PointerEventData evt)
    {
        Catindex++; //예외처리할것
        if (Catindex == 6)
            Catindex = 1;
        if (Managers.Data.CatBooks[1400 + Catindex].Cat_Skill_Name == "Null")
        {
            GetText((int)Texts.SkillText).text = "";
        }
        else
        {
            GetText((int)Texts.SkillText).text = Managers.Data.CatBooks[1400 + Catindex].Cat_Skill_Name;
        }
        GetText((int)Texts.Name).text = Managers.Data.CatBooks[1400 + Catindex].Cat_Name;
        GetImage((int)Images.Cat).sprite = Resources.Load<Sprite>(("Sprites/" + Managers.Data.CatBooks[1400 + Catindex].Cat_Int_Name));
    }
    void LeftIndex(PointerEventData evt)
    {
        Catindex--; //예외처리할것
        if (Catindex == 0)
            Catindex = 5;
        if (Managers.Data.CatBooks[1400 + Catindex].Cat_Skill_Name == "Null")
        {
            GetText((int)Texts.SkillText).text = "";
        }
        else
        {
            GetText((int)Texts.SkillText).text = Managers.Data.CatBooks[1400 + Catindex].Cat_Skill_Name;
        }
        GetText((int)Texts.Name).text = Managers.Data.CatBooks[1400 + Catindex].Cat_Name;
        GetImage((int)Images.Cat).sprite = Resources.Load<Sprite>(("Sprites/" + Managers.Data.CatBooks[1400 + Catindex].Cat_Int_Name));
    }
}