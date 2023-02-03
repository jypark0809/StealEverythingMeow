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


    void Update()
    {
        HappinessLevelUp();
    }

    void HappinessLevelUp()
    {
        if (Managers.Game.SaveData.CatHappinessLevel[CatIndex] == 5)
            return;

        if (Managers.Game.SaveData.CatCurHappinessExp[CatIndex] >= Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex] + 1].H_Max)
        {
            int CurMaxLevel = GetMinMax(Managers.Game.SaveData.CatHappinessLevel);
            Managers.Game.SaveData.CatHappinessLevel[CatIndex]++;
            Managers.Game.SaveData.CatCurHappinessExp[CatIndex] -= Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex] + 1].H_Max;
            if (CurMaxLevel < GetMinMax(Managers.Game.SaveData.CatHappinessLevel))
                GetEmotion();
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
            return;
        switch (_food)
        {
            case "chew":
                if (cat == Catname.Black)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5;
                Managers.Game.SaveData.Food[1]--;
                break;
            case "jerky":
                if (cat == Catname.Tabby)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5;
                Managers.Game.SaveData.Food[2]--;
                break;
            case "mackerel":
                if (cat == Catname.Tabby)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5;
                Managers.Game.SaveData.Food[3]--;
                break;
            case "salmon":
                if (cat == Catname.Gray)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5;
                Managers.Game.SaveData.Food[4]--;
                break;
            case "tunacan":
                if (cat == Catname.Calico)
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 15;
                else
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 5;
                Managers.Game.SaveData.Food[5]--;
                break;
            case "catnipcandy":
                Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 1000;
                Managers.Game.SaveData.Food[0]--;
                break;
        }
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
        go.GetComponent<UI_CatInfo>().SetInfo(Managers.Game.SaveData.CatName[CatIndex], Managers.Game.SaveData.CatHappinessLevel[CatIndex], Managers.Game.SaveData.CatHappinessLevel[CatIndex], Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex] + 1].H_Max, this.transform);
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
        Managers.Game.SaveData.CatCurHappinessExp[CatIndex]++;
        Debug.Log("È¹µæ!");
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
                break;
            case 3:
                Managers.Game.SaveData.Emotion[12] = true;
                Managers.Game.SaveData.Emotion[11] = true;
                Managers.UI.ShowPopupUI<UI_ExpressOpen>().Setinfo(12, 11);
                break;
            case 4:
                Managers.Game.SaveData.Emotion[8] = true;
                Managers.Game.SaveData.Emotion[6] = true;
                Managers.UI.ShowPopupUI<UI_ExpressOpen>().Setinfo(8, 6);
                break;
            case 5:
                Managers.Game.SaveData.Emotion[7] = true;
                Managers.Game.SaveData.Emotion[13] = true;
                Managers.UI.ShowPopupUI<UI_ExpressOpen>().Setinfo(7, 13);
                break;
        }
    }
}
