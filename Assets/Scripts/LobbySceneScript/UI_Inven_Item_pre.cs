using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inven_Item_pre : UI_Popup, IDragHandler, IEndDragHandler
{
    Vector3 mousepos;
    Vector3 worldpos;
    GameObject Imagego;
    enum Images
    {
        PreImage
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Imagego = Get<Image>((int)Images.PreImage).gameObject;
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Imagego.transform.position = eventData.position;
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        mousepos = Camera.main.ScreenToWorldPoint(eventData.position); 
        if(Physics2D.Raycast(mousepos, transform.forward, LayerMask.GetMask("Cat")))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousepos, transform.forward, LayerMask.GetMask("Cat"));
            hit.transform.GetComponent<Cat_Lobby>().Love();
            //�̺�Ʈ �߰� (������)
            Managers.UI.ClosePopupUI();
        }
    }
}