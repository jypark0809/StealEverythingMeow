using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Room : UI_Base
{
    private int Index;

    enum Gameobjects
    {
        Block,
    }
    enum Images
    {
        RoomImage,
        OnText
    }
    enum Texts
    {
        RoomName,
        RoomDes,
        OpneText,
    }
    enum Buttons
    {
        OpenButton,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(Gameobjects));
        Bind<Button>(typeof(Buttons));

        GetText((int)Texts.RoomName).text = Managers.Data.Spaces[1202+Index].Space_Name ;
        GetText((int)Texts.RoomDes).text = Managers.Data.Spaces[1202 + Index].Space_Desc;
        GetImage((int)Images.RoomImage).sprite = Resources.Load<Sprite>(("Sprites/UI/RoomImage/" + Managers.Data.Spaces[1202 + Index].Space_Int_Name));

        if (Managers.Game.SaveData.SpaceLevel > Index + 1)
        {
            GetObject((int)Gameobjects.Block).gameObject.SetActive(false);
        }
        else if ( Managers.Game.SaveData.SpaceLevel == Index + 1)
        {
            GetImage((int)Images.OnText).gameObject.SetActive(false);
            GetText((int)Texts.OpneText).text = "확장 가능";
            GetButton((int)Buttons.OpenButton).gameObject.BindEvent(OpenRoom);
        }
        else if(Managers.Game.SaveData.SpaceLevel < Index + 1)
        {
            GetImage((int)Images.OnText).gameObject.SetActive(false);
            GetButton((int)Buttons.OpenButton).gameObject.BindEvent(OpenRoom);
            GetButton((int)Buttons.OpenButton).GetComponent<Image>().color = Color.white;
        }

    }

    void OpenRoom(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_UnlockRoomPopup>().SetRoomLevel(Index+2);

    }
    public void SetInfo(int _index)
    {
        Index = _index;
    }
}
