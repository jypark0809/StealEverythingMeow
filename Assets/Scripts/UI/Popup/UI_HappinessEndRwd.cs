using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class UI_HappinessEndRwd : UI_Popup
{
    int dia;
    int gold;

    enum Texts
    {
        Ment,
    }
    enum Buttons
    {
        CancelButton
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
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.Ment).text = $"´ÙÀÌ¾Æ {dia}°³¿Í °ñµå {gold}°³¸¦ \n È¹µæÇÏ¿´´Ù³É";

        GetButton((int)Buttons.CancelButton).gameObject.BindEvent(OnCloseButton);
    }

    private void OnCloseButton(PointerEventData evt)
    {
        Managers.Game.SaveData.Gold += gold;
        Managers.Game.SaveData.Gold += dia;
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();
        Managers.UI.CloseAllPopupUI();
    }
    public void Setinfo(int Gold, int Dia)
    {
        gold = Gold;
        dia = Dia;
    }
}
