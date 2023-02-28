using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cat_LobbyHappniess : MonoBehaviour
{
    int pointerID;

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

    void Awake()
    {
#if UNITY_EDITOR
        pointerID = -1; //PC나 유니티 상에서는 -1
#elif UNITY_ANDROID
        pointerID = 0;  // 휴대폰이나 이외에서 터치 상에서는 0 

#endif
        /*
        if (!Managers.Game.SaveData.IsViewStory[CatIndex] && Managers.Game.SaveData.CatHappinessLevel[CatIndex] ==5)
        {
            Managers.UI.MakeWorldSpaceUI<UI_OnHappinessStory>().SetInfo(CatIndex, this.transform);
            //UI띄우기
        }
        */
    }
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
            //Managers.UI.MakeWorldSpaceUI<UI_OnHappinessStory>().SetInfo(CatIndex, this.transform);
        }
        Managers.Game.SaveGame();
    }

    public void Love(string _food)
    {
        if (Managers.Game.SaveData.CatHappinessLevel[CatIndex] == 5)
        {
            Managers.UI.ShowPopupUI<UI_FullHappy>();
            return;
        }
        int Up = 0;
        switch (_food)
        {
            case "Churu":
                if (cat == Catname.Black)
                {
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 20f;
                    Up = 20;
                }
                else
                { 
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 10f;
                    Up = 10;
                }
                Managers.Game.SaveData.Food[(int)Define.SnackType.Churu]--;
                break;
            case "Jerky":
                if (cat == Catname.Tabby)
                {
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 20f;
                    Up = 20;
                }
                else
                {
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 10f;
                    Up = 10;
                }
                Managers.Game.SaveData.Food[(int)Define.SnackType.Jerky]--;
                break;
            case "Mackerel":
                if (cat == Catname.Tabby)
                {
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 20f;
                    Up = 20;
                }
                else
                {
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 10f;
                    Up = 10;
                }
                Managers.Game.SaveData.Food[(int)Define.SnackType.Mackerel]--;
                break;
            case "Salmon":
                if (cat == Catname.Gray)
                {
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 20f;
                    Up = 20;
                }
                else
                {
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 10f;
                    Up = 10;
                }
                Managers.Game.SaveData.Food[(int)Define.SnackType.Salmon]--;
                break;
            case "Tuna":
                if (cat == Catname.Calico)
                {
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 20f;
                    Up = 20;
                }
                else
                {
                    Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 10f;
                    Up = 10;
                }
                Managers.Game.SaveData.Food[(int)Define.SnackType.Tuna]--;
                break;
            case "CatnipCandy":
                Managers.Game.SaveData.CatCurHappinessExp[CatIndex] += 40f;
                Up = 40;
                Managers.Game.SaveData.Food[(int)Define.SnackType.CatnipCandy]--;
                break;
        }
        Managers.UI.MakeWorldSpaceUI<UI_FoodUpSet>().SetInfo(Up, this.transform);
        this.GetComponent<Cat_LobbyMove>().EatEmotion();

    }

    private void OnMouseDown()
    {
        if (!IsPointerOverUIObject(Input.mousePosition))
        {
            /*
            if (Managers.Game.SaveData.CatHappinessLevel[CatIndex] == 5 && !Managers.Game.SaveData.IsViewStory[CatIndex])
            {
                Debug.Log(Managers.Game.SaveData.CatName[CatIndex]);
                Managers.UI.ShowPopupUI<UI_Letter>().SetInfo(Managers.Game.SaveData.CatName[CatIndex], CatIndex);
                Managers.Game.SaveGame();
                return;
            }
            */
            if (!IsInfo)
                Infoset();
            this.GetComponent<Cat_LobbyMove>().SpecialEmotion();
            /*
            if (!Isget)
                GetGoods();
            */
        }
    }
    void Infoset()
    {
        if (Managers.Game.SaveData.CatHappinessLevel[CatIndex] < 5)
            Managers.UI.MakeWorldSpaceUI<UI_CatInfo>().SetInfo(Managers.Game.SaveData.CatName[CatIndex], Managers.Game.SaveData.CatHappinessLevel[CatIndex], Managers.Game.SaveData.CatCurHappinessExp[CatIndex], Managers.Data.Happinesses[1800 + CatIndex * 5 + Managers.Game.SaveData.CatHappinessLevel[CatIndex] + 1].H_Max, this.transform);
        else
            Managers.UI.MakeWorldSpaceUI<UI_CatInfo>().SetInfo(Managers.Game.SaveData.CatName[CatIndex], Managers.Game.SaveData.CatHappinessLevel[CatIndex], 1, 1, this.transform);
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
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();
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
        int _Index1 = 0;
        bool _Index1bool = false;
        int _Index2 = 0;
        bool _Index2bool = false;
        switch (GetMinMax(Managers.Game.SaveData.CatHappinessLevel))
        {
            case 2:
                _Index1 = (int)Define.CatEmotion.Tail;
                _Index2 = (int)Define.CatEmotion.Dig;
                if (!Managers.Game.SaveData.Emotion[_Index1])
                {
                    Managers.Game.SaveData.Emotion[_Index1] = true;
                    Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + _Index1].Express_Int_Name);
                    _Index1bool = true;
                }
                if (!Managers.Game.SaveData.Emotion[_Index2])
                {
                    Managers.Game.SaveData.Emotion[_Index2] = true;
                    Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + _Index2].Express_Int_Name);
                    _Index2bool = true;
                }
                if (_Index1bool || _Index2bool)
                    Managers.UI.ShowPopupUI<UI_ExpressOpen>();
                break;
            case 3:
                _Index1 = (int)Define.CatEmotion.Fly;
                _Index2 = (int)Define.CatEmotion.Paw;
                if (!Managers.Game.SaveData.Emotion[_Index1])
                {
                    Managers.Game.SaveData.Emotion[_Index1] = true;
                    Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + _Index1].Express_Int_Name);
                    _Index1bool = true;
                }
                if (!Managers.Game.SaveData.Emotion[_Index2])
                {
                    Managers.Game.SaveData.Emotion[_Index2] = true;
                    Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + _Index2].Express_Int_Name);
                    _Index2bool = true;

                }
                if (_Index1bool || _Index2bool)
                    Managers.UI.ShowPopupUI<UI_ExpressOpen>();
                break;
            case 4:
                _Index1 = (int)Define.CatEmotion.Stretch;
                _Index2 = (int)Define.CatEmotion.Sleep2;
                if (!Managers.Game.SaveData.Emotion[_Index1])
                {
                    Managers.Game.SaveData.Emotion[_Index1] = true;
                    Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + _Index1].Express_Int_Name);
                    _Index1bool = true;
                }
                if (!Managers.Game.SaveData.Emotion[_Index2])
                {
                    Managers.Game.SaveData.Emotion[_Index2] = true;
                    Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + _Index2].Express_Int_Name);
                    _Index2bool = true;
                }
                if (_Index1bool || _Index2bool)
                    Managers.UI.ShowPopupUI<UI_ExpressOpen>();
                break;
            case 5:
                _Index1 = (int)Define.CatEmotion.Sleep1;
                _Index2 = (int)Define.CatEmotion.Relax;
                if (!Managers.Game.SaveData.Emotion[_Index1])
                {
                    Managers.Game.SaveData.Emotion[_Index1] = true;
                    Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + _Index1].Express_Int_Name);
                    _Index1bool = true;
                }
                if (!Managers.Game.SaveData.Emotion[_Index2])
                {
                    Managers.Game.SaveData.Emotion[_Index2] = true;
                    Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + _Index2].Express_Int_Name);
                    _Index2bool = true;
                }
                if (_Index1bool || _Index2bool)
                    Managers.UI.ShowPopupUI<UI_ExpressOpen>();
                break;
        }

    }
    public bool IsPointerOverUIObject(Vector2 touchPos)
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touchPos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
