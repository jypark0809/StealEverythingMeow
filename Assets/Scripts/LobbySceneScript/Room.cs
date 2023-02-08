using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private bool IsTime;
    private void Start()
    {
        CurRoomLevel = Managers.Game.SaveData.SpaceLevel;
    }

    private void Update()
    {
        if(Managers.Game.SaveData.SpaceLevel < 10)
            IsRoomCheck();

        if(Managers.Game.SaveData.DoingRoomUpgrade)
        {
            DateTime st = DateTime.ParseExact(PlayerPrefs.GetString("OpenTime"), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            if (DateTime.Now >= st)
            {
                OpenRoom();
            }
            else
            {
                if (!IsTime)
                {
                    Managers.UI.MakeWorldSpaceUI<UI_RestTime>().SetInfo(Managers.Data.Spaces[1200 + CurRoomLevel + 1].Space_Time);
                    IsTime = true;
                }
            }
        }

    }
    public void Open()
    {
        //재화소모
        Managers.Game.SaveData.Gold -= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Gold;
        Managers.Game.SaveData.Wood -= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Wood;
        Managers.Game.SaveData.Stone -= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Stone;
        Managers.Game.SaveData.Cotton -= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Cotton;
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();

        Managers.Game.SaveData.DoingRoomUpgrade = true;
        DurationTime = Managers.Data.Spaces[1200 + CurRoomLevel + 1].Space_Time;
        OpenTime = DateTime.Now.AddSeconds(DurationTime);
        PlayerPrefs.SetString("OpenTime", OpenTime.ToString("yyyyMMddHHmmss"));
        IsTime = true;
        Managers.UI.CloseAllPopupUI();
        Managers.UI.MakeWorldSpaceUI<UI_RestTime>().SetInfo(DurationTime);
        Managers.Game.SaveGame();
    }
    private void OpenRoom()
    {
        IsTime = false;
        Managers.Game.SaveData.DoingRoomUpgrade = false;
        //행복도 추가
        for (int i = 0; i < Managers.Game.SaveData.CatHave.Length; i++)
        {
            if (Managers.Game.SaveData.CatHave[i])
                Managers.Game.SaveData.CatCurHappinessExp[i] += Managers.Data.Spaces[1200 + CurRoomLevel].Happiness;
        }

        //카메라 움직임 추가

        //생성
        Managers.Resource.Destroy(Managers.Object.CatHouse.gameObject);
        Managers.UI.ShowPopupUI<UI_Sucess>();
        CurRoomLevel++;
        Managers.Game.SaveData.SpaceLevel++;
        Managers.Object.SpawnCatHouse("CatHouse_" + Managers.Game.SaveData.SpaceLevel);
        Managers.Sound.Play(Define.Sound.Effect, "Effects/RoomOpen");
        Managers.Game.SaveGame();
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
