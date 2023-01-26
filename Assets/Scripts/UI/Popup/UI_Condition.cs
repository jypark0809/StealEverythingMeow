using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class UI_Condition : UI_Popup
{
    enum Texts
    {

        CottonCount,
        WoodCount,
        StoneCount,

        RoomText,
        GoldUpText,
        DiaUpText
    }

    enum Buttons
    {
        CloseButton,
        GoldUp,
        DiaUp,
    }

    enum GameObjects
    {
        ConSetCotton,
        ConSetWood,
        ConSetStone,
        CottonPanel,
        WoodPanel,
        StonePanel,
        ConRF,
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

        GameObject gridPanel = Get<GameObject>((int)GameObjects.ConRF);

        GetText((int)Texts.RoomText).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.RoomLevel].Space_Name;

        //집체크, 가구체크
        for (int i = 0; i < 4 ; i++) // 현재 최종단계의 집 + 가구 
        {
            GameObject ConIm = Managers.Resource.Instantiate("UI/UI_ConSet");
            ConIm.transform.SetParent(gridPanel.transform);
            UI_ConSet ConSet = Util.GetOrAddComponent<UI_ConSet>(ConIm);
            ConSet.SetInfo("234" , true); //가구이름추가
        }

        // 재화 체크
        CheckGoods();

        GetText((int)Texts.GoldUpText).text = Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Gold.ToString();
        GetText((int)Texts.DiaUpText).text = Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Diamond.ToString();
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);


        if (Managers.Game.SaveData.IsSoomUp && Managers.Game.SaveData.Dia >= Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Diamond)
        {
            GetButton((int)Buttons.DiaUp).gameObject.BindEvent(OnDiaUpgrade);
            GetButton((int)Buttons.DiaUp).image.color = Color.yellow;
        }
        else
        {
            GetButton((int)Buttons.DiaUp).interactable = false;
        }
        if(Managers.Game.SaveData.IsSoomUp)
        {
            GetButton((int)Buttons.GoldUp).gameObject.BindEvent(OnGoldUpgrdae);
            GetButton((int)Buttons.GoldUp).image.color = Color.yellow;
        }
        else
        {
            GetButton((int)Buttons.GoldUp).interactable = false;
        }
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.CloseAllPopupUI();
    }
    void OnDiaUpgrade(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_DiaUp>();
    }
    void OnGoldUpgrdae(PointerEventData evt)
    {
        Util.FindChild(Managers.Object.CatHouse.gameObject, "Soom", true).GetComponent<Soom>().SomUpgrade();
        Managers.UI.CloseAllPopupUI();
    }
    void CheckGoods()
    {
        if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Cotton == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetCotton));
        else
        {
            if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Cotton > Managers.Game.SaveData.Cotton)
                GetText((int)Texts.CottonCount).text = "<color=red>" + Managers.Game.SaveData.Cotton + "</color> / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Cotton.ToString();
            else
            {
                GetObject((int)GameObjects.CottonPanel).GetComponent<Image>().color = Color.cyan;
                GetText((int)Texts.CottonCount).text = Managers.Game.SaveData.Cotton + " / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Cotton.ToString();
            }
        }
        if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Wood == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetWood));
        else
        {
            if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Wood > Managers.Game.SaveData.Wood)
                GetText((int)Texts.WoodCount).text = "<color=red>" + Managers.Game.SaveData.Wood + "</color> / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Wood.ToString();
            else
            {
                GetObject((int)GameObjects.WoodPanel).GetComponent<Image>().color = Color.cyan;
                GetText((int)Texts.WoodCount).text = Managers.Game.SaveData.Wood + " / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Wood.ToString();
            }
        }
        if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Stone == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetStone));
        else
        {
            if (Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Stone > Managers.Game.SaveData.Cotton)
                GetText((int)Texts.StoneCount).text = "<color=red>" + Managers.Game.SaveData.Stone + "</color> / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Stone.ToString();
            else
            {
                GetObject((int)GameObjects.StonePanel).GetComponent<Image>().color = Color.cyan;
                GetText((int)Texts.StoneCount).text = Managers.Game.SaveData.Stone + " / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SoomLevel + 1].Stone.ToString();
            }
        }
    }
}
