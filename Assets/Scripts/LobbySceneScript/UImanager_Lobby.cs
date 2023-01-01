using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager_Lobby : MonoBehaviour
{
    public GameObject NpcCat;
    public GameObject RoomPanel;

    public void RoomOpenPanel()
    {
        RoomPanel.SetActive(true);
    }
    public void RoomCanel()
    {
        RoomPanel.SetActive(false);
    }
}
