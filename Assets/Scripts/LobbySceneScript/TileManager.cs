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

    private float OpenTime;
    DateTime now;
    private int CurRoomLevel;
    private void Start()
    {
        CurRoomLevel = Managers.Game.SaveData.SpaceLevel;
    }

    private void Update()
    {
        IsRoomCheck();
    }
    public void Open()
    {
        Camera.main.GetComponent<CameraMove>().IsMove = true;
        OpenTime = Managers.Data.Spaces[1200 + CurRoomLevel + 1].Space_Time;
        StartCoroutine(OpenRoom(OpenTime));
        for (int i = 0; i < Managers.Game.SaveData.CatHave.Length; i++)
        {
            if (Managers.Game.SaveData.CatHave[i])
                Managers.Game.SaveData.CatCurHappinessExp[i] += Managers.Data.Spaces[1200 + CurRoomLevel].Happiness;
        }
        //시간체크 함수 추가

        //카메라 움직임 수정!
        Managers.UI.ClosePopupUI();
    }
    IEnumerator OpenRoom(float _Time)
    {
        Managers.UI.MakeWorldSpaceUI<UI_RestTime>().SetInfo(_Time);
        yield return new WaitForSeconds(_Time);
        Managers.Destroy(Managers.Object.CatHouse.gameObject);
        CurRoomLevel++;
        Managers.Game.SaveData.SpaceLevel++;
        Managers.Object.SpawnCatHouse("CatHouse_" + Managers.Game.SaveData.SpaceLevel);
        Managers.UI.ShowPopupUI<UI_Sucess>();
        Managers.Sound.Play(Define.Sound.Effect, "Effects/RoomOpen");

    }
    private void IsRoomCheck()
    {
        if (Managers.Game.SaveData.Gold >= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Gold)
            IsGold = true;
        if (Managers.Game.SaveData.Wood >= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Wood)
            IsWood = true;
        if (Managers.Game.SaveData.Stone >= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Stone)
            IsStone = true;
        if (Managers.Game.SaveData.Cotton >= Managers.Data.Spaces[1200 + CurRoomLevel + 1].Cotton)
            IsCotton = true;

        //방체크,가구체크 >>가구경우 상점과 확인후 다시 수정필요
        if (Managers.Game.SaveData.MaxFurniture[CurRoomLevel] == Managers.Data.Spaces[1200 + CurRoomLevel].Space_Furniture_Count)
            IsFur = true;

        if (IsGold & IsWood & IsStone & IsCotton & IsFur)
            Managers.Game.SaveData.IsRoomOpen = true;
    }
}
