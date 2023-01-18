using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UnlockRoomPopup : UI_Popup
{
    enum Texts
    {
        GoldText,
        CottonValue,
        StoneValue,
        WoodValue,
    }

    enum Buttons
    {
        CloseButton,
        OkayButton,
        CancleButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.GoldText).text = "$999,999";
        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OnOkayButton);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(OnCloseButton);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
        GetText((int)Texts.CottonValue).text = "999"; //차후 추가
        GetText((int)Texts.StoneValue).text = "666";//차후 추가
        GetText((int)Texts.WoodValue).text = "333";//차후 추가
        //Util.FindChild(this.gameObject, "Cotton", true).SetActive(false);
    }

    void OnOkayButton(PointerEventData evt)
    {
        if (Managers.Game.SaveData.Gold < -1)
        {
            Debug.Log("not enough money");
            Managers.UI.ClosePopupUI();
        }
        else
        {
            switch (Managers.Game.SaveData.Level)
            {
                case 1:
                    //Managers.UI.MakeWorldSpaceUI<UI_RestTime>();
                    Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_Living_Room2", recursive: true).SetActive(false);
                    Camera.main.transform.position = new Vector3(0,0,-10); 
                    break;
                case 2:
                    Util.FindChild(Managers.Object.CatHouse.gameObject, "Hide_Samll_Room", recursive: true).SetActive(false);
                    Camera.main.transform.position = new Vector3(10, 0, -10);
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
            Managers.Game.SaveData.Level++;
            // Unlock
            // Spend Gold
            Managers.Game.SaveData.Gold -= 0;
            // Save Data
            //Managers.Game.SaveGame();
            Managers.UI.ClosePopupUI();
        }
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
