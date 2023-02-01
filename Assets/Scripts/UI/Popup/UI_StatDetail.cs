using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UI_StatDetail : UI_Popup
{
    int Index;
    GameObject HaveGo, NotHaveGo;
    TextMeshProUGUI HaveText , NotHaveText;

    enum GameObjects
    {
        NotHavePanel,
        HavePanel
    }

    enum Buttons
    {
        RightButton,
        LeftButton,
        CloseButton
    }
    enum Texts
    {
        NotHaveName,
        HaveName

    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));


        HaveGo = GetObject((int)GameObjects.HavePanel);
        NotHaveGo = GetObject((int)GameObjects.NotHavePanel);
        HaveText = GetText((int)Texts.HaveName);
        NotHaveText = GetText((int)Texts.NotHaveName);


        if (Managers.Game.SaveData.CatHave[Index])
        {
            SetHave(Index);
        }
        else
        {
            SetNotHave(Index);
        }

        GetButton((int)Buttons.RightButton).gameObject.BindEvent(RightButtonIndex);
        GetButton((int)Buttons.LeftButton).gameObject.BindEvent(LeftButtonIndex);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }
    private void SetHave(int _index)
    {
        HaveGo.SetActive(true);
        NotHaveGo.SetActive(false);

        //정보설정
        HaveText.text = Managers.Data.CatBooks[1401 + _index].Cat_Name;
    }
    private void SetNotHave(int _index)
    {
        HaveGo.SetActive(false);
        NotHaveGo.SetActive(true);

        //정보설정
        NotHaveText.text = Managers.Data.CatBooks[1401 + _index].Cat_Name;
    }
    public void SetInfo(int _index)
    {
        Index = _index;
    }
    void RightButtonIndex(PointerEventData evt)
    {
        Index++;
        if(Index == Managers.Game.SaveData.CatHave.Length)
        {
            Index = 0;
        }

        if(Managers.Game.SaveData.CatHave[Index])
        {
            SetHave(Index);
        }
        else
        {
            SetNotHave(Index);
        }
    }
    void LeftButtonIndex(PointerEventData evt)
    {
        Index--;
        if (Index == -1)
        {
            Index = Managers.Game.SaveData.CatHave.Length-1;
        }

        if (Managers.Game.SaveData.CatHave[Index])
        {
            SetHave(Index);
        }
        else
        {
            SetNotHave(Index);
        }
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();

    }
}
