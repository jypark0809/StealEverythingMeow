using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UI_StatDetail : UI_Popup
{
    private string[] CatName = { "White", "Black", "Calico", "Tabby", "Gray" };
    private string[] FoodName = { "Churu", "Jerky", "Mackerel", "Salmon", "Tuna", "CatnipCandy" };
    int Index;
    GameObject HaveGo, NotHaveGo;
    TextMeshProUGUI HaveText, NotHaveText, HaveDes, NotHaveDes, HaveSkil, NotHaveSkill, HaveFoodName, CatPrice;
    Image HaveImage, NotHaveImage, HaveSkillImage, HaveFoodImage, DiaGold;
    Button Buy;

    private int HappyLevel;
    enum GameObjects
    {
        NotHavePanel,
        HavePanel,
        Skill,
        HeartSet,
    }

    enum Buttons
    {
        RightButton,
        LeftButton,
        CloseButton,
        BuyButton

    }
    enum Texts
    {
        NotHaveName,
        HaveName,
        NotHaveCatDesc,
        HaveCatDesc,
        HaveSkillText,
        NotHaveSkillText,
        HaveFoodName,
        Price,
        HappyLevel,
        NeedExp

    }

    enum Images
    {
        NotHaveCatImage,
        HaveCatImage,
        HaveSkillImage,
        HaveFoodImage,
        DiaOrGold
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        SetGet();
        if (Managers.Game.SaveData.CatHave[Index])
        {
            SetHave(Index);
        }
        else
        {
            SetNotHave(Index);
        }

        GetButton((int)Buttons.RightButton).gameObject.BindEvent(RightButtonIndex);
        GetButton((int)Buttons.LeftButton).gameObject.BindEvent(LeftButtonIndex);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }

    void SetGet()
    {
        HaveGo = GetObject((int)GameObjects.HavePanel);
        NotHaveGo = GetObject((int)GameObjects.NotHavePanel);
        HaveText = GetText((int)Texts.HaveName);
        NotHaveText = GetText((int)Texts.NotHaveName);
        HaveImage = GetImage((int)Images.HaveCatImage);
        NotHaveImage = GetImage((int)Images.NotHaveCatImage);
        HaveDes = GetText((int)Texts.HaveCatDesc);
        NotHaveDes = GetText((int)Texts.NotHaveCatDesc);
        HaveSkil = GetText((int)Texts.HaveSkillText);
        NotHaveSkill = GetText((int)Texts.NotHaveSkillText);
        HaveSkillImage = GetImage((int)Images.HaveSkillImage);
        HaveFoodName = GetText((int)Texts.HaveFoodName);
        HaveFoodImage = GetImage((int)Images.HaveFoodImage);
        DiaGold = GetImage((int)Images.DiaOrGold);
        CatPrice = GetText((int)Texts.Price);
        Buy = GetButton((int)Buttons.BuyButton);
    }
    private void SetHave(int _index)
    {
        HaveGo.SetActive(true);
        NotHaveGo.SetActive(false);

        //정보설정
        HaveText.text = Managers.Data.CatBooks[1401 + _index].Cat_Name;
        HaveImage.sprite = Managers.Resource.Load<Sprite>("Sprites/Nyan/" + CatName[Index] + "/" + CatName[Index] + "_Walk1");
        HaveDes.text = Managers.Data.CatBooks[1401 + _index].Cat_Desc;
        if (Index != 0)
        {
            HaveSkillImage.gameObject.SetActive(true);
            HaveSkil.text = Managers.Data.CatBooks[1401 + _index].Cat_Skill_Name;
            HaveSkillImage.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/SkillIcon/Skill_" + CatName[Index] + "Cat");
        }
        else if (Index == 0)
        {
            HaveSkillImage.gameObject.SetActive(false);
            HaveSkil.text = "없음";
        }
        else
        {
            HaveSkil.text = "";
            HaveSkillImage.sprite = null;
        }
        HaveFoodName.text = Managers.Data.ShopItems[Managers.Data.CatBooks[1401 + _index].Cat_Favor_Food].Shop_Name;
        HaveFoodImage.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/ShopItem/Snack/" + FoodName[Index]);
        HappyLevel = Managers.Game.SaveData.CatHappinessLevel[_index];
        GetText((int)Texts.NeedExp).text = "다음 행복도까지 필요경험치 : " + (Managers.Data.Happinesses[1800 + _index * 5 + HappyLevel + 1].H_Max - Managers.Game.SaveData.CatCurHappinessExp[_index]).ToString();
        GetText((int)Texts.HappyLevel).text = "행복도 레벨 : " + HappyLevel.ToString();
        SetHappiness();

    }
    private void SetNotHave(int _index)
    {
        HaveGo.SetActive(false);
        NotHaveGo.SetActive(true);

        //정보설정
        NotHaveText.text = Managers.Data.CatBooks[1401 + _index].Cat_Name;
        NotHaveImage.sprite = Managers.Resource.Load<Sprite>("Sprites/Nyan/" + CatName[Index] + "/" + CatName[Index] + "_Walk1");
        NotHaveDes.text = Managers.Data.CatBooks[1401 + _index].Cat_Desc;
        NotHaveSkill.text = "특기 : " + Managers.Data.CatBooks[1401 + _index].Cat_Skill_Name;

        if (Managers.Data.CatBooks[1401 + Index].Diamond > 0)
        {
            DiaGold.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Diamond");
            CatPrice.text = Managers.Data.CatBooks[1401 + Index].Diamond.ToString();
            if (Managers.Data.CatBooks[1401 + Index].Diamond > Managers.Game.SaveData.Dia)
            {
                CatPrice.color = Color.red; 
                Buy.interactable = false;
                Buy.gameObject.BindEvent(CannotBuy);
            }
            else
            {
                CatPrice.color = Color.black;
                Buy.interactable = true;
                Buy.gameObject.BindEvent(BuyCat);
            }
        }
        else if (Managers.Data.CatBooks[1401 + Index].Gold > 0)
        {
            DiaGold.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Gold");
            CatPrice.text = Managers.Data.CatBooks[1401 + Index].Gold.ToString();
            if (Managers.Data.CatBooks[1401 + Index].Gold > Managers.Game.SaveData.Gold)
            {
                CatPrice.color = Color.red;
                Buy.interactable = false;
                Buy.gameObject.BindEvent(CannotBuy);
            }
            else
            {
                CatPrice.color = Color.black;
                Buy.interactable = true;
                Buy.gameObject.BindEvent(BuyCat);
            }
        }

    }

    
    public void SetInfo(int _index)
    {
        Index = _index;
    }
    void RightButtonIndex(PointerEventData evt)
    {
        Index++;
        if (Index == Managers.Game.SaveData.CatHave.Length)
        {
            Index = 0;
        }

        if (Managers.Game.SaveData.CatHave[Index])
        {
            SetHave(Index);
        }
        else
        {
            SetNotHave(Index);
        }
    }
    void LeftButtonIndex(PointerEventData evt)
    {
        Index--;
        if (Index == -1)
        {
            Index = Managers.Game.SaveData.CatHave.Length - 1;
        }

        if (Managers.Game.SaveData.CatHave[Index])
        {
            SetHave(Index);
        }
        else
        {
            SetNotHave(Index);
        }
    }

    void BuyCat(PointerEventData evt)
    {
        if(Managers.Game.SaveData.CatCount < Managers.Data.Sooms[1300 +Managers.Game.SaveData.SoomLevel].Cap_Capacity)
        {
            Managers.Game.SaveData.CatCount++;
            Managers.Game.SaveData.CatHave[Index] = true;
            Managers.Resource.Instantiate("LobbyCat/" + Managers.Data.CatBooks[1401 + Index].Cat_Int_Name.Substring(2));

            if (Managers.Data.CatBooks[1401 + Index].Diamond > 0)
            {
                Managers.Game.SaveData.Dia -= Managers.Data.CatBooks[1401 + Index].Diamond;
            }
            else if (Managers.Data.CatBooks[1401 + Index].Gold > 0)
            {
                Managers.Game.SaveData.Gold -= Managers.Data.CatBooks[1401 + Index].Gold;
            }
            Managers.Game.SaveGame();
            (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();
            Managers.UI.CloseAllPopupUI();
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_MoreSoom>();
        }
    }

    void CannotBuy(PointerEventData evt)
    {
        return;
    }
    void SetHappiness()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.HeartSet);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);


        for (int i = 0; i < 5; i++)
        {
            if (i < HappyLevel)
            {
                UI_HeartSet Item1 = Managers.UI.MakeSubItem<UI_HeartSet>(gridPanel.transform);
                Item1.SetInfo(1, 1);
            }
            else if (i == HappyLevel)
            {
                UI_HeartSet Item1 = Managers.UI.MakeSubItem<UI_HeartSet>(gridPanel.transform);
                Item1.SetInfo(Managers.Game.SaveData.CatCurHappinessExp[Index], Managers.Data.Happinesses[1800 + Index * 5 + HappyLevel + 1].H_Max); ;
            }
            else if (i > HappyLevel)
            {
                UI_HeartSet Item1 = Managers.UI.MakeSubItem<UI_HeartSet>(gridPanel.transform);
                Item1.SetInfo(0, 1);
            }
        }
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
