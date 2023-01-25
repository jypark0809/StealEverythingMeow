using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : UI_Base
{
    Vector3 mousepos;
    Vector3 worldpos;
    GameObject go;
    enum Images
    {
        Test,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        go = GetImage((int)Images.Test).gameObject;

        go.BindEvent(drag, Define.UIEvent.Drag);
    }

    void drag(PointerEventData evt)
    {
        Debug.Log("asd");
        go.transform.position = evt.position;

        mousepos = Camera.main.ScreenToWorldPoint(evt.position);
        if (Physics2D.Raycast(mousepos, transform.forward, LayerMask.GetMask("Cat")))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousepos, transform.forward, LayerMask.GetMask("Cat"));
            //hit.transform.GetComponent<Cat_Lobby>().Love();
            //이벤트 추가 (애정도)
        }
    }
}
