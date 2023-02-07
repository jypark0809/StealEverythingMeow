using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StageClear : UI_Popup
{
    TextMeshProUGUI PlaytimeText;
    TextMeshProUGUI GoldText;
    TextMeshProUGUI ExtraGoldText;
    TextMeshProUGUI TotalGoldText;
    TextMeshProUGUI WoodText;
    TextMeshProUGUI RockText;
    TextMeshProUGUI CottonText;
    SpaceData _sData;

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

        Managers.Data.Spaces.TryGetValue(Managers.Game.SaveData.SpaceLevel + 1200, out _sData);

        PlaytimeText = GetText((int)Texts.PlaytimeText);
        GoldText = GetText((int)Texts.GoldText);
        ExtraGoldText = GetText((int)Texts.ExtraGoldText);
        TotalGoldText = GetText((int)Texts.TotalGoldText);
        WoodText = GetText((int)Texts.WoodText);
        RockText = GetText((int)Texts.RockText);
        CottonText = GetText((int)Texts.CottonText);

        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(OnCloseButton);

        RefreshUI();
        SaveGameResult();

        Managers.Sound.Play(Define.Sound.Effect, "Effects/StageClear", volume: 0.2f);
    }

    void RefreshUI()
    {
        int totalGold = Managers.Object.Player.Stat.Gold * _sData.Space_Gold_Plus / 100;

        PlaytimeText.text = $"ÇÃ·¹ÀÌ ½Ã°£ : {UpdateTime((Managers.Scene.CurrentScene as GameScene)._playTime)}";
        GoldText.text = $"È¹µæ °ñµå : {Managers.Object.Player.Stat.Gold}";
        ExtraGoldText.text = $"°ø°£ Lv Bonus : {_sData.Space_Gold_Plus}%";
        TotalGoldText.text = $"ÇÕ°è : {totalGold}";
        WoodText.text = Managers.Object.Player.Stat.Wood.ToString();
        RockText.text = Managers.Object.Player.Stat.Rock.ToString();
        CottonText.text = Managers.Object.Player.Stat.Cotton.ToString();
    }

    void SaveGameResult()
    {
        int totalGold = Managers.Object.Player.Stat.Gold * _sData.Space_Gold_Plus / 100;

        Managers.Game.SaveData.Gold += totalGold;
        Managers.Game.SaveData.Wood += Managers.Object.Player.Stat.Wood;
        Managers.Game.SaveData.Stone += Managers.Object.Player.Stat.Rock;
        Managers.Game.SaveData.Cotton += Managers.Object.Player.Stat.Cotton;

        Managers.Game.SaveGame();
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
        //LoadingScene.LoadScene("CatHouseScene");
        Managers.Scene.LoadScene(Define.SceneType.CatHouseScene);


        // Save Data

        ClosePopupUI();
    }
    #endregion
}
