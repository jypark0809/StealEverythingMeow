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
    int Index;
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

        GetText((int)Texts.Ment).text = $"���̾� {dia}���� ��� {gold}���� \n ȹ���Ͽ��ٳ�";
        GetButton((int)Buttons.CancelButton).gameObject.BindEvent(OnCloseButton);
    }

    private void OnCloseButton(PointerEventData evt)
    {
        Managers.Game.SaveData.Gold += gold;
        Managers.Game.SaveData.Gold += dia;
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();
        Managers.Game.SaveData.IsViewStory[Index] = true;
        Managers.Sound.Play(Define.Sound.Bgm, "BGM/BGM_Home", volume: 0.4f);
        Managers.Game.SaveGame();
        Managers.UI.CloseAllPopupUI();
    }
    public void Setinfo(int Gold, int Dia, int i)
    {
        gold = Gold;
        dia = Dia;
        Index = i;
    }
}
