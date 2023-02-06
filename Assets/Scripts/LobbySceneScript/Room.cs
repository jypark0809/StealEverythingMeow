using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool IsGold;
    public bool IsWood;
    public bool IsStone;
    public bool IsCotton;
    public bool IsFur;
    public bool Issoom;

    private float DurationTime;
    DateTime OpenTime;
    private int CurRoomLevel;
    private void Start()
    {
        CurRoomLevel = Managers.Game.SaveData.SpaceLevel;
    }

    private void Update()
    {
        if(Managers.Game.SaveData.SpaceLevel < 10)
            IsRoomCheck();

        /*
        if(Managers.Game.SaveData.DoingRoomUpgrade)
        {
            if(DateTime.Now >= OpenTime)
            {
                OpenRoom();
            }
        }
        */
    }
    public void Open()
    {
        Managers.Game.SaveData.DoingRoomUpgrade = true;
        DurationTime = Managers.Data.Spaces[1200 + CurRoomLevel + 1].Space_Time;
        OpenTime = DateTime.Now.AddSeconds(DurationTime);
        Debug.Log(DateTime.Now);
        Debug.Log(OpenTime);
        //Managers.UI.MakeWorldSpaceUI<UI_RestTime>().SetInfo(DurationTime);
        Managers.UI.ClosePopupUI();
        OpenRoom();
    }
    private void OpenRoom()
    {
        //Managers.Game.SaveData.DoingRoomUpgrade = false;
        //행복도 추가
        for (int i = 0; i < Managers.Game.SaveData.CatHave.Length; i++)
        {
            if (Managers.Game.SaveData.CatHave[i])
                Managers.Game.SaveData.CatCurHappinessExp[i] += Managers.Data.Spaces[1200 + CurRoomLevel].Happiness;
        }

        //카메라 움직임 추가
        Managers.Resource.Destroy(Managers.Object.CatHouse.gameObject);
        Managers.UI.ShowPopupUI<UI_Sucess>();
        CurRoomLevel++;
        Managers.Game.SaveData.SpaceLevel++;
        Managers.Object.SpawnCatHouse("CatHouse_" + Managers.Game.SaveData.SpaceLevel);
        Managers.Sound.Play(Define.Sound.Effect, "Effects/RoomOpen");
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
