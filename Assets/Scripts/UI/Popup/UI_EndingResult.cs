using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EndingResult : UI_Popup
{
    private int SumLevel = 0;

    enum GameObjects
    {
        HappySet,
    }
    enum Texts
    {
        LevelText,
    }
    enum Buttons
    {
        OkButton,
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
        Bind<GameObject>(typeof(GameObjects));

        SetLevel();
        SetHerats();
        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnCreditEvent);
    }


    void SetLevel()
    {
        for(int i =0; i<Managers.Game.SaveData.CatHappinessLevel.Length; i++)
        {
            if (Managers.Game.SaveData.CatHave[i])
                SumLevel += Managers.Game.SaveData.CatHappinessLevel[i];
        }
        SumLevel /= Managers.Game.SaveData.CatHave.Length;

        GetText((int)Texts.LevelText).text = "Lv. "+SumLevel.ToString();
    }

    void SetHerats()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.HappySet);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < 5; i++)
        {
            if (i < SumLevel)
            {
                UI_HeartSet Item1 = Managers.UI.MakeSubItem<UI_HeartSet>(gridPanel.transform);
                Item1.SetInfo(1, 1);
            }
            else if (i >= SumLevel)
            {
                UI_HeartSet Item1 = Managers.UI.MakeSubItem<UI_HeartSet>(gridPanel.transform);
                Item1.SetInfo(0, 1);
            }
        }
    }
    void OnCreditEvent(PointerEventData evt)
    {
        Managers.Scene.LoadScene(Define.SceneType.EndingScene);
        Managers.UI.CloseAllPopupUI();
    }
}
