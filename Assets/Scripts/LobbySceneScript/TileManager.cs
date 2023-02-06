using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public bool IsGold;
    public bool IsWood;
    public bool IsStone;
    public bool IsCotton;
    public bool IsFur;
    public bool Issoom;

    private float OpenTime;
    DateTime now;
    private int CurRoomLevel;
    private void Start()
    {
        CurRoomLevel = Managers.Game.SaveData.SpaceLevel;
    }

    private void Update()
    {
        if(Managers.Game.SaveData.SpaceLevel < 10)
            IsRoomCheck();
    }
    public void Open()
    {
        OpenTime = Managers.Data.Spaces[1200 + CurRoomLevel + 1].Space_Time;
        StartCoroutine(OpenRoom(0f));//OpenTime));
        for (int i = 0; i < Managers.Game.SaveData.CatHave.Length; i++)
        {
            if (Managers.Game.SaveData.CatHave[i])
                Managers.Game.SaveData.CatCurHappinessExp[i] += Managers.Data.Spaces[1200 + CurRoomLevel].Happiness;
        }
        //시간체크 함수 추가

        //카메라 움직임 추가

        //행복도 추가
        Managers.UI.ClosePopupUI();
    }
    IEnumerator OpenRoom(float _Time)
    {
        Managers.Game.SaveData.DoingRoomUpgrade = true;
        Managers.UI.MakeWorldSpaceUI<UI_RestTime>().SetInfo(_Time);
        yield return new WaitForSeconds(_Time);
        Managers.UI.ShowPopupUI<UI_Sucess>();
        CurRoomLevel++;
        Managers.Game.SaveData.SpaceLevel++;
        Managers.Object.SpawnCatHouse("CatHouse_" + Managers.Game.SaveData.SpaceLevel);
        Managers.Sound.Play(Define.Sound.Effect, "Effects/RoomOpen");
        Managers.Game.SaveGame();
        /*
        yield return new WaitForSeconds(2f);
        Managers.Game.SaveData.DoingRoomUpgrade = false;
        Debug.Log(Managers.Game.SaveData.DoingRoomUpgrade);
        Managers.Game.SaveGame();
        Managers.Destroy(Managers.Object.CatHouse.gameObject);
        */
    }
    private void IsRoomCheck()
    {
        if (Managers.Game.SaveData.Gold >= Managers.Data.Spaces[1200 + CurRoomLevel +1].Gold)
            IsGold = true;
        else
            IsGold = false;
        if (Managers.Game.SaveData.Wood >= Managers.Data.Spaces[1200 + CurRoomLevel +1].Wood)
            IsWood = true;
        else
            IsWood = false;
        if (Managers.Game.SaveData.Stone >= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Stone)
            IsStone = true;
        else
            IsStone = false;
        if (Managers.Game.SaveData.Cotton >= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Cotton)
            IsCotton = true;
        else
            IsCotton = false;

        if(Managers.Game.SaveData.SpaceLevel >=2)
        {
            int FurCount = Managers.Game.SaveData.FList.Count;
            for (int i = 1; i<CurRoomLevel; i++)
            {
                FurCount -= Managers.Data.Spaces[1200 + i].Space_Furniture_Count;
            }
            if (FurCount == (Managers.Data.Spaces[1200 + CurRoomLevel].Space_Furniture_Count))
                IsFur = true;
            else
                IsFur = false;
        }
        else
        {
            IsFur = true;
        }


        if (Managers.Data.Spaces[1200 + CurRoomLevel +1].Soom_Lv == Managers.Game.SaveData.SoomLevel)
            Issoom = true;
        else
            Issoom = false;


        if (IsGold & IsWood & IsStone & IsCotton & IsFur && Issoom)
            Managers.Game.SaveData.IsRoomOpen = true;
        else
            Managers.Game.SaveData.IsRoomOpen = false;
    }
}
