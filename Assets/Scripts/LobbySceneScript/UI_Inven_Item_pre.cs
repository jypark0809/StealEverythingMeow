using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inven_Item_pre : UI_Base, IDragHandler, IEndDragHandler
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
        Bind<Image>(typeof(Images));

        Imagego = Get<Image>((int)Images.PreImage).gameObject;
    }
    /*
    private void Update()
    {
        ScreentToWolrd();
        go.transform.position = worldpos;
    }
    void ScreentToWolrd()
    {
        mousepos = Input.mousePosition;
        worldpos = Camera.main.ScreenToWorldPoint(mousepos + new Vector3(0, 0, 10f));
        
    }
    */
    public void Ondrag(PointerEventData eventData)
    {

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Imagego.transform.position = eventData.position;
    }


    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        /*
        Vector3 laypos = Camera.main.ScreenToWorldPoint(eventData.position);
        laypos.z = 10;
        if (Physics.Raycast(laypos, Vector3.forward, LayerMask.GetMask("Cat")))
        {
            Debug.Log("asd");
        }
        */
        mousepos = Camera.main.ScreenToWorldPoint(eventData.position);
        RaycastHit2D hit = Physics2D.Raycast(mousepos, transform.forward);
        if(Physics2D.Raycast(mousepos, transform.forward, LayerMask.GetMask("Cat")))
        {
            Debug.Log("asd");
            //이벤트 추가
            //Managers.UI
            
        }
    }

}
