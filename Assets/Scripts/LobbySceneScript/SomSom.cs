using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomSom : MonoBehaviour
{
    public bool IsRoomOpen;
    public bool IsUpgrdae;
    private void Start()
    {
        //StartCoroutine("RoomOpen"); // �� Ȯ������ ����üũ
    }

    private void OnMouseDown()
    {
        if (IsRoomOpen)
            Managers.UI.ShowPopupUI<UI_UnlockRoomPopup>();
        else if (IsUpgrdae)
            Managers.UI.ShowPopupUI<UI_UpgradeSom>();

    }

    IEnumerator RoomOpen()
    {
        yield return new WaitForSeconds(2f);
        IsRoomOpen = true;
    }
}
