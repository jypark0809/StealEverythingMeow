using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_CatSet : UI_Base
{
    string Name;
    int Count;

    enum Images
    {
        CatImage,
    }
    enum Texts
    {
        CatName,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        Get<Image>((int)Images.CatImage).GetComponent<Image>().sprite = Resources.Load<Sprite>(("Sprites/UI/" + Name));
        Get<TextMeshProUGUI>((int)Texts.CatName).GetComponent<TextMeshProUGUI>().text = name;
    }

    public void SetInfo(string _str, int _count)
    {
        Count = _count;
        Name = _str;
    }
}
