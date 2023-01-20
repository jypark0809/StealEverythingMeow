using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomSom : MonoBehaviour
{
    public bool IsRoomOpen = false;
    public bool IsUpgrdae = false;


    private void Update()
    {
        if(Managers.Game.SaveData.curFurnitureCount == Managers.Game.SaveData.MaxFurniture[Managers.Game.SaveData.RoomLevel])
        {
            IsRoomOpen = true;
            if (Managers.Game.SaveData.RoomLevel == 3)
            {
                IsRoomOpen = false;
                IsUpgrdae = true;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Managers.Game.SaveData.RoomLevel > 6)
            return;

        if (IsRoomOpen)
            Managers.UI.ShowPopupUI<UI_UnlockRoomPopup>();
        else if (IsUpgrdae)
            Managers.UI.ShowPopupUI<UI_UpgradeSom>();
    }

    public void SomUpgrade()
    {
        if (Managers.Game.SaveData.RoomLevel < 7)
        {
            GameObject go = Util.FindChild(Managers.Object.CatHouse.gameObject, "Somsom", true);
            go.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Furniture/woodhouse");
            Managers.Game.SaveData.RoomLevel++;
            Managers.Game.SaveData.curFurnitureCount = 0;
            IsUpgrdae = false;
        }

        Managers.UI.ClosePopupUI();
    }
}
