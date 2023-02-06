using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UnlockRoomPopup : UI_Base
{
    enum Texts
    {
        CottonCount,
        WoodCount,
        StoneCount,

        TopText,
        GoldUpText,
    }
    enum GameObjects
    {
        ConSetCotton,
        ConSetWood,
        ConSetStone,

        CottonPanel,
        WoodPanel,
        StonePanel,


        Upgrade,
        Full,
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

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        if (Managers.Game.SaveData.SpaceLevel == 10)
        {
            GetObject((int)GameObjects.Upgrade).gameObject.SetActive(false);
            GetObject((int)GameObjects.Full).gameObject.SetActive(true);
        }
        else
        {
            GetObject((int)GameObjects.Upgrade).gameObject.SetActive(true);
            GetObject((int)GameObjects.Full).gameObject.SetActive(false);

            if (Managers.Game.SaveData.IsRoomOpen)
                GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnOkayButton);
            else
                GetButton((int)Buttons.OkButton).interactable = false;

            CheckGoods();
            GetText((int)Texts.GoldUpText).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Gold.ToString();
            GetText((int)Texts.TopText).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Space_Name.ToString() + " 해금하기";
        }

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void OnOkayButton(PointerEventData evt)
    {
        Managers.Object.CatHouse.GetComponent<TileManager>().Open();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }

    void CheckGoods()
    {
        if (Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Cotton == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetCotton));
        else
        {
            if (Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Cotton > Managers.Game.SaveData.Cotton)
                GetText((int)Texts.CottonCount).text = "<color=red>" + Managers.Game.SaveData.Cotton + "</color> / " + Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Cotton.ToString();
            else
            {
                GetObject((int)GameObjects.CottonPanel).GetComponent<Image>().color = Color.cyan;
                GetText((int)Texts.CottonCount).text = Managers.Game.SaveData.Cotton + " / " + Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Cotton.ToString();
            }
        }
        if (Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Wood == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetWood));
        else
        {
            if (Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Wood > Managers.Game.SaveData.Wood)
                GetText((int)Texts.WoodCount).text = "<color=red>" + Managers.Game.SaveData.Wood + "</color> / " + Managers.Data.Spaces[1200 + Managers.Game.SaveData.SoomLevel + 1].Wood.ToString();
            else
            {
                GetObject((int)GameObjects.WoodPanel).GetComponent<Image>().color = Color.cyan;
                GetText((int)Texts.WoodCount).text = Managers.Game.SaveData.Wood + " / " + Managers.Data.Spaces[1200 + Managers.Game.SaveData.SoomLevel + 1].Wood.ToString();
            }
        }
        if (Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Stone == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetStone));
        else
        {
            if (Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Stone > Managers.Game.SaveData.Cotton)
                GetText((int)Texts.StoneCount).text = "<color=red>" + Managers.Game.SaveData.Stone + "</color> / " + Managers.Data.Sooms[1300 + Managers.Game.SaveData.SpaceLevel + 1].Stone.ToString();
            else
            {
                GetObject((int)GameObjects.StonePanel).GetComponent<Image>().color = Color.cyan;
                GetText((int)Texts.StoneCount).text = Managers.Game.SaveData.Stone + " / " + Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Stone.ToString();
            }
        }
    }
}
