using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_Inven_Item : UI_Base
{
    string Name;
    int Count;
    enum GameObjects
    {
        ItemIcon,
        Num_Text,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));


        Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>().sprite = Resources.Load<Sprite>(("Sprites/UI/Bag/" + Name));
        Get<GameObject>((int)GameObjects.Num_Text).GetComponent<TextMeshProUGUI>().text = Count.ToString();
    }

    public void SetInfo(string _str, int _count)
    {
        Count = _count;
        Name = _str;
    }
}
