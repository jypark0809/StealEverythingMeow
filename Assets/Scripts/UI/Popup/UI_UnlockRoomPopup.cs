using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UnlockRoomPopup : UI_Popup
{
    int Index = 0;

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
        Bind<GameObject>(typeof(GameObjects));

        /*
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

        */
        GetObject((int)GameObjects.Upgrade).gameObject.SetActive(true);
        GetObject((int)GameObjects.Full).gameObject.SetActive(false);

        if (Managers.Game.SaveData.IsRoomOpen && !Managers.Game.SaveData.DoingRoomUpgrade && Index-1 == Managers.Game.SaveData.SpaceLevel)
            GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnOkayButton);
        else
            GetButton((int)Buttons.OkButton).interactable = false;

        GetText((int)Texts.GoldUpText).text = Managers.Data.Spaces[1200 + Index].Gold.ToString();
        GetText((int)Texts.TopText).text = Managers.Data.Spaces[1200 + Index].Space_Name + " 해금하기";

        CheckGoods();
        SetFur();
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }
    void OnOkayButton(PointerEventData evt)
    {
        Managers.Object.RoomOpen.Open();
        Managers.UI.ClosePopupUI();
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }


    void SetFur()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.FurConPanel);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        bool CurHave = false;
        bool SoomHave = false;
        UI_SoomCheckPanel Item1 = Managers.UI.MakeSubItem<UI_SoomCheckPanel>(gridPanel.transform);
        if (Managers.Game.SaveData.SoomLevel == Managers.Data.Spaces[1200 + Index].Soom_Lv)
            SoomHave = true;
        Item1.SetInfo(Managers.Data.Spaces[1200 + Index].Soom_Lv.ToString(), SoomHave);


        //가구
        Index--;

        int Count = Managers.Data.Spaces[1200 + Index].Space_Furniture_Count;
        int CurFurCount = 0;
        for (int i = 1; i < Index; i++)
        {
            CurFurCount += Managers.Data.Spaces[1200 + i].Space_Furniture_Count;
        }

        for (int i = 0; i < Count; i++)
        {
            UI_FurnitureCheckPanel Item = Managers.UI.MakeSubItem<UI_FurnitureCheckPanel>(gridPanel.transform);
            for (int j = 0; j < Managers.Game.SaveData.FList.Count; j++)
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
            Item.SetInfo(Managers.Data.Furnitures[1101 + i + CurFurCount].F_Name, CurHave);
        }

    }
    void CheckGoods()
    {
        //나무
        if (Managers.Data.Spaces[1200 + Index].Wood == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetWood));
        else
        {
            if (Managers.Data.Spaces[1200 + Index].Wood > Managers.Game.SaveData.Wood)
                GetText((int)Texts.WoodCount).text = "<color=red>" + Managers.Game.SaveData.Wood + "</color> / " + Managers.Data.Spaces[1200 + Index].Wood.ToString();
            else
            {
                GetText((int)Texts.WoodCount).text = Managers.Game.SaveData.Wood + " / " + Managers.Data.Spaces[1200 + Index].Wood.ToString();
            }
        }

        //돌
        if (Managers.Data.Spaces[1200 + Index].Stone == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetStone));
        else
        {
            if (Managers.Data.Spaces[1200 + Index].Stone > Managers.Game.SaveData.Stone)
                GetText((int)Texts.StoneCount).text = "<color=red>" + Managers.Game.SaveData.Stone + "</color> / " + Managers.Data.Spaces[1200 + Index].Stone.ToString();
            else
            {
                GetText((int)Texts.StoneCount).text = Managers.Game.SaveData.Stone + " / " + Managers.Data.Spaces[1200 + Index].Stone.ToString();
            }
        }

        //솜
        if (Managers.Data.Spaces[1200 + Index].Cotton == 0)
            Managers.Resource.Destroy(GetObject((int)GameObjects.ConSetCotton));
        else
        {
            if (Managers.Data.Spaces[1200 + Index].Cotton > Managers.Game.SaveData.Cotton)
                GetText((int)Texts.CottonCount).text = "<color=red>" + Managers.Game.SaveData.Cotton + "</color> / " + Managers.Data.Spaces[1200 + Index].Cotton.ToString();
            else
            {
                GetText((int)Texts.CottonCount).text = Managers.Game.SaveData.Cotton + " / " + Managers.Data.Spaces[1200 + Index].Cotton.ToString();
            }
        }
    }


    public void SetRoomLevel(int RoomLevel)
    {
        Index = RoomLevel;
    }
}
