using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomSom : MonoBehaviour
{
    public bool IsRoomOpen = false;
    private void Start()
    {
        StartCoroutine("RoomOpen"); // 룸 확장조건 여부체크
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
        else
        {
            //확장 풀가 팝업
        }
    }

    IEnumerator RoomOpen()
    {
        yield return new WaitForSeconds(2f);
        IsRoomOpen = true;
    }
}
