using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomSom : MonoBehaviour
{
    public bool IsRoomOpen = false;
    private void Start()
    {
        StartCoroutine("RoomOpen");
    }

    void Update()
    {
        if (IsRoomOpen)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("SomSom")))
            {
                Managers.UI.ShowPopupUI<UI_UnlockRoomPopup>();
                IsRoomOpen = false;
            }

        }
    }

    IEnumerator RoomOpen()
    {
        yield return new WaitForSeconds(2f);
        IsRoomOpen = true;
    }
}
