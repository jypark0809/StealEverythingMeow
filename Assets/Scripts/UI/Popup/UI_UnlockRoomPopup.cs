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
        FullText,
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

        FurConPanel
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

        if (Managers.Game.SaveData.SpaceLevel == 3 && Managers.Game.SaveData.SoomLevel != 2)
        {
            GetObject((int)GameObjects.Upgrade).gameObject.SetActive(false);
            GetObject((int)GameObjects.Full).gameObject.SetActive(true);
            GetText((int)Texts.FullText).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Space_Name +"을 해금하기 위해서는 \n 숨숨집 Lv.2가 필요하다냥 ";
        }
        else if (Managers.Game.SaveData.SpaceLevel == 6 && Managers.Game.SaveData.SoomLevel != 3)
        {
            GetObject((int)GameObjects.Upgrade).gameObject.SetActive(false);
            GetObject((int)GameObjects.Full).gameObject.SetActive(true);
            GetText((int)Texts.FullText).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Space_Name + "을 해금하기 위해서는 \n 숨숨집 Lv.3가 필요하다냥 ";
        }
        else if (Managers.Game.SaveData.SpaceLevel == 10)
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

            SetFur();
            CheckGoods();
            GetText((int)Texts.GoldUpText).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Gold.ToString();
            GetText((int)Texts.TopText).text = Managers.Data.Spaces[1200 + Managers.Game.SaveData.SpaceLevel + 1].Space_Name + " 해금하기";

        }

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }
    void OnOkayButton(PointerEventData evt)
    {
       Managers.Object.RoomOpen.Open();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
    void SetFur()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.FurConPanel);


        int Index = Managers.Game.SaveData.SpaceLevel;

        int Count = Managers.Data.Spaces[1200 + Index].Space_Furniture_Count;
        int CurFurCount = 0;
        bool CurHave = false;
        for (int i = 1; i < Index; i++)
        {
            CurFurCount += Managers.Data.Spaces[1200 + i].Space_Furniture_Count;
        }

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);
        
        for (int i = 0; i < Count; i++)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_FurnitureCheckPanel");
            Item.transform.SetParent(gridPanel.transform);
            UI_FurnitureCheckPanel FurSet = Util.GetOrAddComponent<UI_FurnitureCheckPanel>(Item);
            for (int j = CurFurCount; j < Managers.Game.SaveData.FList.Count; j++)
            {
                if (Managers.Game.SaveData.FList[j].F_Name == Managers.Data.Furnitures[1101 + i + CurFurCount].F_Name)
                {
                    CurHave = true;
                    break;
                }
                else
                {
                    CurHave = false;
                }
            }
            FurSet.SetInfo(Managers.Data.Furnitures[1101 + i + CurFurCount].F_Name, CurHave);

        }
        
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
