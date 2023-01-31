using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public bool IsRoomOpen;

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
        // IsRoomCheck();
    }
    public void Open()
    {
        if(!IsRoomOpen)
        {
            Debug.Log("업글불가능");
            //업글 불가능시 ui출력?
            return;
        }    
        Camera.main.GetComponent<CameraTest>().IsMove = true;
        IsRoomOpen = false;
        OpenTime = Managers.Data.Spaces[1200 + CurRoomLevel+1].Space_Time;
        StartCoroutine(OpenRoom(OpenTime));


        Debug.Log(DateTime.Now);
        //카메라 움직임 수정!
        Managers.UI.ClosePopupUI();
        Managers.Game.SaveData.SpaceLevel++;
    }
    IEnumerator OpenRoom(float _Time)
    {
        Managers.UI.MakeWorldSpaceUI<UI_RestTime>().SetInfo(_Time);
        yield return new WaitForSeconds(_Time);
        Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_"+Managers.Data.Spaces[1200 + CurRoomLevel+1].Space_Int_Name, true).SetActive(false);
        Util.FindChild(Managers.Object.CatHouse.gameObject, "Block_"+Managers.Data.Spaces[1200 + CurRoomLevel].Space_Int_Name, true).SetActive(false);
        Managers.UI.ShowPopupUI<UI_Sucess>();
        Camera.main.GetComponent<CameraTest>().IsMove = false;
        Managers.Sound.Play(Define.Sound.Effect, "Effects/RoomOpen");
        CurRoomLevel++;
        IsRoomOpen = false;
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
            IsRoomOpen = true;
    }
}
