using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CatHouseDropDown : UI_Base
{
    enum DropDown
    {
        MenuDropDown
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TMP_Dropdown>(typeof(DropDown));

        GetDropDown((int)DropDown.MenuDropDown).onValueChanged.AddListener(OnDropdownEvent);
    }

    public void OnDropdownEvent(int index)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        GetDropDown((int)DropDown.MenuDropDown).value = GetDropDown((int)DropDown.MenuDropDown).options.Capacity -1;
        switch (index)
        {
            case 0://셋팅
                Managers.UI.ShowPopupUI<UI_Setting>();
                break;
            case 1://스탯
                Managers.UI.ShowPopupUI<UI_Stat>();
                break;
            case 2://상점
                Managers.UI.ShowPopupUI<UI_Shop>();
                break;
            case 3://퀘스트
                Managers.UI.ShowPopupUI<UI_Quest>();
                break;
            case 4://가방
                Managers.UI.ShowPopupUI<UI_Bag>();
                break;
        }
    }
}
