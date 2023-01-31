using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_ConSet : UI_Base
{
    enum Texts
    {
        ConText
    }
    enum Images
    {
        CheckImages
    }

    string _FurName;
    bool _Need;
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetText((int)Texts.ConText).text = _FurName;
        if(_Need)
            GetImage((int)Images.CheckImages).GetComponent<Image>().sprite = Resources.Load<Sprite>(("Sprites/UI/Check"));
    }
    public void SetInfo(string FurName, bool IsNeed)
    {
        _FurName = FurName;
    }
}
