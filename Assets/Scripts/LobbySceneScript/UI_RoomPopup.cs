using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UI_RoomPopup : UI_Popup
{

    public int current_Gold = 99999999;
    enum Texts
    {
        Pay_Text,
    }

    enum Buttons
    {
        Ok,
        Cancel
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        //텍스트 금액변경 코드


        //OK키 > 확장
        Button go1 = GetButton((int)Buttons.Ok);
        go1.onClick.AddListener(RoomOpen);

        //Cancel키 > 팝업닫기
        Button go2 = GetButton((int)Buttons.Cancel);
        GameObject go3 = GetButton((int)Buttons.Cancel).gameObject;
        go2.onClick.AddListener(RoomClose);
    }

    public void RoomOpen()
    {
        if(current_Gold <0)
        {
            Debug.Log("No money");
            RoomClose();
        }
        else
        {
            GameManagerLobby thegame = FindObjectOfType<GameManagerLobby>();
            thegame.Room[thegame.current_room].SetActive(false);
            thegame.current_room++;

            RoomClose();
        }

        
    }
    
    public void RoomClose()
    {
        Destroy(this.gameObject);
    }
}
