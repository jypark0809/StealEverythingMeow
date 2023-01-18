using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    void Update()
    {

    }
    public void Open()
    {
        switch (Managers.Game.SaveData.Level)
        {
            case 1:
                Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_Living_Room2", recursive: true).SetActive(false);
                break;
            case 2:
                StartCoroutine(OpenRoom("Hide_Samll_Room"));
                Camera.main.GetComponent<CameraTest>().target = new Vector3(10, 0, -10);
                break;
            case 3:
                Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_Samll_Kitchen", recursive: true).SetActive(false);
                Camera.main.transform.position = new Vector3(4, 13, -10);
                break;
            case 4:
                Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_Samll_Utility_Room", recursive: true).SetActive(false);
                Camera.main.transform.position = new Vector3(14, 14, -10);
                break;
            case 5:
                Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_Samll_BathRoom", recursive: true).SetActive(false);
                Camera.main.transform.position = new Vector3(-9, 14, -10);
                break;
            case 6:
                Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_BigRoom", recursive: true).SetActive(false);
                Camera.main.transform.position = new Vector3(-19, -2, -10);
                break;
            case 7:
                Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_Library", recursive: true).SetActive(false);
                Camera.main.transform.position = new Vector3(-28, 8, -10);
                break;
            case 8:
                Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_PlayRoom", recursive: true).SetActive(false);
                Camera.main.transform.position = new Vector3(-18, 12, -10);
                break;
            case 9:
                Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_BigYard", recursive: true).SetActive(false);
                Camera.main.transform.position = new Vector3(-6, -8, -10);
                break;
        }
    }
    IEnumerator OpenRoom(string _name)
    {
        yield return new WaitForSeconds(2f);
        Util.FindChild(Managers.Object.CatHouse.gameObject, _name, recursive: true).SetActive(false);
    }
}
