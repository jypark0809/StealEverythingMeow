using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_CatHouse : UI_Scene
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
        base.Init();
        Bind<TMP_Dropdown>(typeof(DropDown));

        //GetDropDown((int)DropDown.MenuDropDown).gameObject.BindEvent(OnDropdownEvent);
        GetDropDown((int)DropDown.MenuDropDown).onValueChanged.AddListener(OnDropdownEvent);
    }

    public void OnDropdownEvent(int index)
    {
        switch (index)
        {
            case 1://����
                Managers.UI.ShowPopupUI<UI_Setting>();
                break;
            case 2://����
                Managers.UI.ShowPopupUI<UI_Stat>();
                break;
            case 3://����
                Managers.UI.ShowPopupUI<UI_Colletion>();
                break;
            case 4://����
                Managers.UI.ShowPopupUI<UI_Store>();
                break;
            case 5://����Ʈ
                Managers.UI.ShowPopupUI<UI_Quest>();
                break;
            case 6://����
                Managers.UI.ShowPopupUI<UI_Bag>();
                break;
        }
        GetDropDown((int)DropDown.MenuDropDown).value = 0;
    }
}
