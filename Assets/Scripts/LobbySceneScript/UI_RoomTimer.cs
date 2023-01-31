using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_RoomTimer : UI_Base
{
    TextMeshProUGUI TheTime;

    enum Texts
    {
        Time,
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));

        TheTime = GetText((int)Texts.Time);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
