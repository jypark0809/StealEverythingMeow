using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Food_Item : UI_Base
{
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
        Get<Image>((int)Images.ItemIcon).sprite = Resources.Load<Sprite>(("Sprites/UI/Bag/" + Name));
        Get<Image>((int)Images.DragItem).sprite = Resources.Load<Sprite>(("Sprites/UI/Bag/" + Name));
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


        Vector2 pos = Camera.main.ScreenToWorldPoint(evt.position);
        RaycastHit hit;

        Debug.DrawRay(pos, transform.forward * 1000, Color.blue);
        if (Physics.Raycast(pos, transform.forward, out hit, 100f, LayerMask.GetMask("Cat")))
        {
            hit.transform.GetComponent<Cat_LobbyHappniess>().Love(Name);
            go.transform.localPosition = new Vector3(0, 0, 0);
            Count--;
            Get<GameObject>((int)GameObjects.Num_Text).GetComponent<TextMeshProUGUI>().text = (Count).ToString();
            if (Count == 0)
            {
                Get<Image>((int)Images.DragItem).gameObject.SetActive(false);
            }
            Debug.Log("실행");
        }
    }
    void EndDrag(PointerEventData evt)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(evt.position);
        
        var ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;

        Debug.DrawRay(pos, transform.forward * 1000, Color.blue);
        if (Physics.Raycast(pos, transform.forward,out hit, 100f, LayerMask.GetMask("Cat")))
        {
            hit.transform.GetComponent<Cat_LobbyHappniess>().Love(Name);
            go.transform.localPosition = new Vector3(0, 0, 0);
            Count--;
            Get<GameObject>((int)GameObjects.Num_Text).GetComponent<TextMeshProUGUI>().text = (Count).ToString();
            if(Count == 0)
            {
                Get<Image>((int)Images.DragItem).gameObject.SetActive(false);
            }
            Debug.Log("실행");
        }
        else
        {
            go.transform.localPosition = Vector3.zero;
        }
    }
}
