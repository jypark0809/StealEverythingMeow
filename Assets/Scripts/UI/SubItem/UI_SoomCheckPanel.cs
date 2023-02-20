using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_SoomCheckPanel : UI_Base
{

    string _FurName;
    bool _Need;

    enum Texts
    {
        RoomText
    }
    enum Images
    {
        CheckImages
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetText((int)Texts.RoomText).text = "¼û¼ûÁý Lv" +_FurName;

        if(_Need)
            GetImage((int)Images.CheckImages).GetComponent<Image>().sprite = Resources.Load<Sprite>(("Sprites/UI/Panel2/Checkbox3"));
        else
            GetImage((int)Images.CheckImages).GetComponent<Image>().sprite = Resources.Load<Sprite>(("Sprites/UI/Panel2/Checkbox4"));
    }

    public void SetInfo(string FurName, bool IsNeed = true)
    {
        _FurName = FurName;
        _Need = IsNeed;
    }
}
