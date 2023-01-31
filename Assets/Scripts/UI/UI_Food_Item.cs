using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Food_Item : UI_Base
{

    Vector3 mousepos;
    Vector3 worldpos;
    GameObject go;

    string Name;
    int Count;
    bool IsDrag;

    enum GameObjects
    {
        Num_Text,
    }

    enum Images
    {
        ItemIcon,
        DragItem,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        go = GetImage((int)Images.DragItem).gameObject;

        //Debug.Log(Name + " " +IsDrag);

        go.BindEvent(drag, Define.UIEvent.Drag);
        go.BindEvent(EndDrag, Define.UIEvent.PointerUp);
        if (Count == 0)
        {
            Get<Image>((int)Images.DragItem).gameObject.SetActive(false);
        }
        Get<Image>((int)Images.ItemIcon).sprite = Resources.Load<Sprite>(("Sprites/UI/" + Name));
        Get<Image>((int)Images.DragItem).sprite = Resources.Load<Sprite>(("Sprites/UI/" + Name));
        Get<GameObject>((int)GameObjects.Num_Text).GetComponent<TextMeshProUGUI>().text = Count.ToString();
    }

    public void SetInfo(string _str, int _count)
    {
        Count = _count;
        Name = _str;
    }

    void drag(PointerEventData evt)
    {

        go.transform.position = evt.position;
        mousepos = Camera.main.ScreenToWorldPoint(evt.position);
    }
    void EndDrag(PointerEventData evt)
    {
        if (Physics2D.Raycast(mousepos, transform.forward, LayerMask.GetMask("Cat")))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousepos, transform.forward, LayerMask.GetMask("Cat"));
            hit.transform.GetComponent<Cat_Lobby>().Love(Name);
            go.transform.localPosition = new Vector3(0, 0, 0);
            Count--;
            Get<GameObject>((int)GameObjects.Num_Text).GetComponent<TextMeshProUGUI>().text = (Count).ToString();
            if(Count == 0)
            {
                Get<Image>((int)Images.DragItem).gameObject.SetActive(false);
            }
        }
        else
        {
            go.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
