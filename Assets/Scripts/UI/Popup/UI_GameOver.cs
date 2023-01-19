using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
{
    enum Texts
    {
        PlaytimeText,
        GoldText,
        ExtraGoldText,
        TotalGoldText,
        WoodText,
        RockText,
        CottonText
    }

    enum Buttons
    {
        ExitButton,
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

        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(OnCloseButton);
        GetText((int)Texts.PlaytimeText).text = 
            $"플레이 시간 : {UpdateTime((Managers.Scene.CurrentScene as GameScene)._playTime)}";
        GetText((int)Texts.GoldText).text =
            $"훔친 골드 : {Managers.Object.Player.Stat.Gold}";
        GetText((int)Texts.TotalGoldText).text = Managers.Object.Player.Stat.Gold.ToString();
        GetText((int)Texts.WoodText).text = Managers.Object.Player.Stat.Wood.ToString();
        GetText((int)Texts.RockText).text = Managers.Object.Player.Stat.Rock.ToString();
        GetText((int)Texts.CottonText).text = Managers.Object.Player.Stat.Cotton.ToString();
    }

    string UpdateTime(float time)
    {
        int min = (int)time / 60;
        int sec = (int)time % 60;
        string result = sec.ToString("D2");
        return $"{min}:{result}";
    }

    #region EventHandler
    void OnCloseButton(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Time.timeScale = 1;
        Managers.Scene.LoadScene(Define.SceneType.CatHouseScene);

        // Save Data

        ClosePopupUI();
    }
    #endregion
}
