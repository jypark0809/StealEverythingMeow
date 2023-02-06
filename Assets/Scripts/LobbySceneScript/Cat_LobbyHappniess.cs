using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_LobbyHappniess : MonoBehaviour
{
    [SerializeField]
    public enum Catname
    {
        White,
        Black,
        Gray,
        Calico,
        Tabby,
    }
    public Catname cat;

    public int CatIndex;

    private bool IsInfo;
    private bool Isget;

    public float Exp;
    void Update()
    {
        HappinessLevelUp();
        Exp = Managers.Game.SaveData.CatCurHappinessExp[CatIndex];
    }

    void HappinessLevelUp()
    {
        if (Managers.Game.SaveData.CatHappinessLevel[CatIndex] == 5)
            return;

        if (Managers.Game.SaveData.CatCurHappinessExp[CatIndex] >= Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex] + 1].H_Max)
        {
            int CurMaxLevel = GetMinMax(Managers.Game.SaveData.CatHappinessLevel);
            Managers.Game.SaveData.CatCurHappinessExp[CatIndex] -= Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex] + 1].H_Max;
            Managers.Game.SaveData.CatHappinessLevel[CatIndex]++;
            if (CurMaxLevel < GetMinMax(Managers.Game.SaveData.CatHappinessLevel))
                GetEmotion();

            Managers.UI.ShowPopupUI<UI_HappyLevelUp>().CatIndex = CatIndex;
        }
        if (Managers.Game.SaveData.CatHappinessLevel[CatIndex] == 5)
        {
            Managers.Game.SaveData.CatCurHappinessExp[CatIndex] = 0;
            Managers.Game.SaveData.CatHappinessLevel[CatIndex] = 5;
        }
    }

    public void Love(string _food)
    {
        if (Managers.Game.SaveData.CatHappinessLevel[CatIndex] == 5)
        {
            Managers.UI.ShowPopupUI<UI_FullHappy>();
            return;
        }
        switch (_food)
        {
            case "Churu":
                if (cat == Catname.Black)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15f;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5f;
                Managers.Game.SaveData.Food[1]--;
                break;
            case "Jerky":
                if (cat == Catname.Tabby)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15f;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5f;
                Managers.Game.SaveData.Food[2]--;
                break;
            case "Mackerel":
                if (cat == Catname.Tabby)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15f;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5f;
                Managers.Game.SaveData.Food[3]--;
                break;
            case "Salmon":
                if (cat == Catname.Gray)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15f;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5f;
                Managers.Game.SaveData.Food[4]--;
                break;
            case "Tuna":
                if (cat == Catname.Calico)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15f;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5f;
                Managers.Game.SaveData.Food[5]--;
                break;
            case "CatnipCandy":
                Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 1000f;
                Managers.Game.SaveData.Food[0]--;
                break;
        }
        this.GetComponent<Cat_LobbyMove>().EatEmotion();

    }
    private void OnMouseDown()
    {
        if (!IsInfo)
            Infoset();
        if (!Isget)
            GetGoods();
    }
    void Infoset()
    {
        GameObject go = Managers.UI.MakeWorldSpaceUI<UI_CatInfo>().gameObject;
        go.GetComponent<UI_CatInfo>().SetInfo(Managers.Game.SaveData.CatName[CatIndex], Managers.Game.SaveData.CatHappinessLevel[CatIndex], Managers.Game.SaveData.CatCurHappinessExp[CatIndex], Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex] + 1].H_Max, this.transform);
        go.transform.position = this.transform.position;
        IsInfo = true;
        StartCoroutine(trueInfo());
    }
    IEnumerator trueInfo()
    {
        yield return new WaitForSeconds(8f);
        IsInfo = false;
    }
    void GetGoods()
    {
        switch (CatIndex)
        {
            case 0:
                Managers.Game.SaveData.Wood += Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex]].H_Rwd_Wood;
                break;
            case 1:
                Managers.Game.SaveData.Stone += Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex]].H_Rwd_Stone;
                break;
            case 2:
                Managers.Game.SaveData.Cotton += Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex]].H_Rwd_Cotton;
                break;
            case 3:
                Managers.Game.SaveData.Gold += Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex]].H_Rwd_Gold;
                break;
            case 4:
                Managers.Game.SaveData.Jelly += Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex]].H_Rwd_Power;
                break;
        }
        Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5;
        Isget = true;
    }

    int GetMinMax(int[] array)
    {
        int min = array[0];
        int max = array[0];

        for (int i = 0; i < array.Length; i++)
        {
            if (min > array[i])
                min = array[i];

            else if (min < array[i])
                max = array[i];
        }
        return max;
    }
    void GetEmotion()
    {
        Debug.Log("°¨Á¤Ç¥Çö È¹µæ");

        switch (GetMinMax(Managers.Game.SaveData.CatHappinessLevel))
        {
            case 2:
                Managers.Game.SaveData.Emotion[3] = true;
                Managers.Game.SaveData.Emotion[5] = true;
                Managers.UI.ShowPopupUI<UI_ExpressOpen>().Setinfo(3, 5);
                Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1504].Express_Int_Name);
                Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1506].Express_Int_Name);
                break;
            case 3:
                Managers.Game.SaveData.Emotion[12]   = true;
                Managers.Game.SaveData.Emotion[9] = true;
                Managers.UI.ShowPopupUI<UI_ExpressOpen>().Setinfo(12, 9);
                Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1513].Express_Int_Name);
                Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1510].Express_Int_Name);
                break;
            case 4:
                Managers.Game.SaveData.Emotion[8] = true;
                Managers.Game.SaveData.Emotion[6] = true;
                Managers.UI.ShowPopupUI<UI_ExpressOpen>().Setinfo(8, 6);
                Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1509].Express_Int_Name);
                Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1507].Express_Int_Name);
                break;
            case 5:
                Managers.Game.SaveData.Emotion[7] = true;
                Managers.Game.SaveData.Emotion[13] = true;
                Managers.UI.ShowPopupUI<UI_ExpressOpen>().Setinfo(7, 13);
                Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1508].Express_Int_Name);
                Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1514].Express_Int_Name);
                break;
        }
    }
}
