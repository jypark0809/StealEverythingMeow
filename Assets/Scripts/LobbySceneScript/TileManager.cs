using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    /*
    public void Open()
    {
        Camera.main.GetComponent<CameraTest>().IsMove = true;
        switch (Managers.Game.SaveData.RoomLevel)
        {
            case 1:
                StartCoroutine(OpenRoom("Hide_LivingRoom2"));
                StartCoroutine(OpenRoomBlock("Block_LivingRoom1"));
                break;
            case 2:
                StartCoroutine(OpenRoom("Hide_SmallRoom"));
                StartCoroutine(OpenRoomBlock("Block_LivingRoom2"));
                Camera.main.GetComponent<CameraTest>().targetPos = new Vector3(8, 0, -10);
                break;
            case 4:
                StartCoroutine(OpenRoom("Hide_Kitchen"));
                StartCoroutine(OpenRoomBlock("Block_smallRoom"));
                Camera.main.GetComponent<CameraTest>().targetPos = new Vector3(4, 13, -10);
                break;
            case 5:
                StartCoroutine(OpenRoom("Hide_UtilityRoom"));
                StartCoroutine(OpenRoomBlock("Block_kitchen"));
                Camera.main.GetComponent<CameraTest>().targetPos = new Vector3(14, 14, -10);
                break;
            case 6:
                StartCoroutine(OpenRoom("Hide_BathRoom"));
                StartCoroutine(OpenRoomBlock("Block_UtilityRoom"));
                Camera.main.GetComponent<CameraTest>().targetPos = new Vector3(-9, 14, -10);
                break;
            case 7:
                StartCoroutine(OpenRoom("Hide_BigRoom"));
                StartCoroutine(OpenRoomBlock("Block_BathRoom"));
                Camera.main.GetComponent<CameraTest>().targetPos = new Vector3(-19, -2, -10);
                break;
            case 8:
                StartCoroutine(OpenRoom("Hide_Library"));
                StartCoroutine(OpenRoomBlock("Block_BigRoom"));
                Camera.main.GetComponent<CameraTest>().targetPos = new Vector3(-28, 8, -10);
                break;
            case 9:
                StartCoroutine(OpenRoom("Hide_PlayRoom"));
                StartCoroutine(OpenRoomBlock("Block_Library"));
                Camera.main.GetComponent<CameraTest>().targetPos = new Vector3(-18, 12, -10);
                break;
            case 10:
                StartCoroutine(OpenRoom("Hide_BigYard"));
                Camera.main.GetComponent<CameraTest>().targetPos = new Vector3(-6, -8, -10);
                break;
        }
        Managers.UI.ClosePopupUI();
        Managers.Game.SaveData.curFurnitureCount = 0;
    }
    IEnumerator OpenRoom(string _name)
    {
        yield return new WaitForSeconds(2f);
        Util.FindChild(Managers.Object.CatHouse.gameObject, _name, true).SetActive(false);
        Managers.UI.ShowPopupUI<UI_Sucess>();
        Camera.main.GetComponent<CameraTest>().IsMove = false;
        Managers.Sound.Play(Define.Sound.Effect, "Effects/RoomOpen");
    }
    IEnumerator OpenRoomBlock(string _name)
    {
        yield return new WaitForSeconds(2f);
        Util.FindChild(Managers.Object.CatHouse.gameObject, _name, true).SetActive(false);
    }
    public void OpenF()
    {
        Camera.main.GetComponent<CameraTest>().IsMove = true;
        //가구 배치or 해금
        switch (Managers.Game.SaveData.curFurnitureCount)
        {
            case 0:
                StartCoroutine(OpenFurniture("1"));
                break;
            case 1:
                StartCoroutine(OpenFurniture("2"));
                break;
            case 2:
                StartCoroutine(OpenFurniture("3"));
                break;
            case 3:
                StartCoroutine(OpenFurniture("4"));
                break;
            case 4:
                StartCoroutine(OpenFurniture("5"));
                break;
            
        }
        Managers.Game.SaveData.curFurnitureCount++;

        Managers.UI.ClosePopupUI();
        if (Managers.Game.SaveData.RoomLevel == 3 && Managers.Game.SaveData.curFurnitureCount == Managers.Game.SaveData.MaxFurniture[Managers.Game.SaveData.RoomLevel])
        {
            Managers.UI.ShowPopupUI<UI_TmpUp>();
            return;
        }
        if (Managers.Game.SaveData.RoomLevel == 7 && Managers.Game.SaveData.curFurnitureCount == Managers.Game.SaveData.MaxFurniture[Managers.Game.SaveData.RoomLevel])
        {
            Managers.UI.ShowPopupUI<UI_TmpUp>();
            return;
        }
        if (Managers.Game.SaveData.curFurnitureCount == Managers.Game.SaveData.MaxFurniture[Managers.Game.SaveData.RoomLevel])
        {
            Managers.UI.ShowPopupUI<UI_TmpUp>();
            return;
        }
    }

    IEnumerator OpenFurniture(string _name)
    {
        Camera.main.GetComponent<CameraTest>().targetPos = (Util.FindChild(Managers.Object.CatHouse.gameObject, Managers.Game.SaveData.CurFurniture[Managers.Game.SaveData.RoomLevel] + _name, true)).transform.position;
        yield return new WaitForSeconds(1.3f);
        Managers.Sound.Play(Define.Sound.Effect, "Effects/OpenFurniture");
        GameObject go = Managers.Resource.Instantiate("RoomFurniture/"+ Managers.Game.SaveData.CurFurniture[Managers.Game.SaveData.RoomLevel] +_name, Managers.Object.CatHouse.transform);
        go.transform.position = (Util.FindChild(Managers.Object.CatHouse.gameObject, Managers.Game.SaveData.CurFurniture[Managers.Game.SaveData.RoomLevel]+_name, true)).transform.position;
        Camera.main.GetComponent<CameraTest>().IsMove = false;
    }
    */
}
