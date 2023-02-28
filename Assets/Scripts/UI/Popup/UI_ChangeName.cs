using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class UI_ChangeName : UI_Popup
{
    int _catIndex;
    enum Gameobjects
    {
        TypeName,
    }
    enum Texts
    {
    }
    enum Buttons
    {
        CloseButton,
        OkButton
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
        Bind<GameObject>(typeof(Gameobjects));

        if (Managers.Game.SaveData.Dia >= 100)
            GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnChangeEvent);
        else
            GetButton((int)Buttons.OkButton).enabled = false;
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnChangeEvent(PointerEventData evt)
    {
        //재화소모

        //이름바꾸기
        Managers.Game.SaveData.CatName[_catIndex] = GetObject((int)Gameobjects.TypeName).GetComponent<TMP_InputField>().text;
        Managers.Game.SaveGame();

        Debug.Log(Managers.Game.SaveData.CatName[_catIndex]);
        Managers.UI.FindPopup<UI_StatDetail>().ReName();
        Managers.UI.ClosePopupUI();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
    public void Setinfo(int i)
    {
        _catIndex = i;
    }
}