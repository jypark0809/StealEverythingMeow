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
    int Index;
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

        go.BindEvent(drag, Define.UIEvent.Drag);
        go.BindEvent(EndDrag, Define.UIEvent.PointerUp);

        if (Managers.Game.SaveData.Food[Index] == 0)
        {
            Get<Image>((int)Images.DragItem).gameObject.SetActive(false);
        }
        Get<Image>((int)Images.ItemIcon).sprite = Resources.Load<Sprite>(("Sprites/UI/ShopItem/Snack/" + Name));
        Get<Image>((int)Images.DragItem).sprite = Resources.Load<Sprite>(("Sprites/UI/ShopItem/Snack/" + Name));
        Get<GameObject>((int)GameObjects.Num_Text).GetComponent<TextMeshProUGUI>().text = Managers.Game.SaveData.Food[Index].ToString();
    }

    private void Update()
    {
        Get<GameObject>((int)GameObjects.Num_Text).GetComponent<TextMeshProUGUI>().text = Managers.Game.SaveData.Food[Index].ToString();
    }

    public void SetInfo(string _str, int _index)
    {
        Index = _index;
        Name = _str;
    }

    void drag(PointerEventData evt)
    {
        go.transform.position = evt.position;
    }
    void EndDrag(PointerEventData evt)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(evt.position);
        var ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        Debug.DrawRay(pos, transform.forward * 1000, Color.blue);
        if (Physics.Raycast(pos, transform.forward, out hit, 100f, LayerMask.GetMask("Cat")))
        {
            hit.transform.GetComponent<Cat_LobbyHappniess>().Love(Name);
            go.transform.localPosition = new Vector3(0, 0, 0);
            if(Managers.Game.SaveData.Food[Index] == 0)
            {
                Get<Image>((int)Images.DragItem).gameObject.SetActive(false);
            }
        }
        else
        {
            go.transform.localPosition = Vector3.zero;
        }
    }
}
