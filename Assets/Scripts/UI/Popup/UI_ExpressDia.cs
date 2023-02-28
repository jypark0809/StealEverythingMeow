using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ExpressDia : UI_Popup
{
    int Index;
    enum Texts
    {
        ConditionText,
        DiaPrice

    }
    enum Buttons
    {
        OkButton,
        CloseButton,
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



        GetText((int)Texts.ConditionText).text = ($"'{Managers.Data.ExpressBooks[1501+Index].Express_Name}'를 획득하려면\n <color=red>" + Managers.Data.ExpressBooks[1501 + Index].Express_Path+"</color> 를 달성해야 해요!" + "\n다이아를 사용해서 획득하시겠습니까?");



        if (Managers.Game.SaveData.Dia<100)
        {
            GetText((int)Texts.DiaPrice).text = "<color=red>" +"100"+ "</color>";
        }
        else
        {
            GetButton((int)Buttons.OkButton).gameObject.BindEvent(GetEmotion);
        }
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }


    public void SetInfo(int _index)
    {
        Index = _index;
    }

    void GetEmotion(PointerEventData evt)
    {
        Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501+Index].Express_Int_Name);
        Managers.Game.SaveData.Emotion[Index] = true;
        Managers.Game.SaveData.Dia -= 100;
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();
        Managers.UI.FindPopup<UI_Stat>().ReBarExpress();
        Managers.UI.FindPopup<UI_Stat>()._Express.transform.GetChild(Index).GetComponent<UI_ExpressSet>().OpenBlock();
        Managers.Game.SaveGame();
        Managers.UI.ClosePopupUI();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
