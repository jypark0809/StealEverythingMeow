using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_RoomRestTime : UI_Popup
{
    public float LestTime = 20f;

    enum Texts
    {
        RestTime,
    }
    void Start()
    {
        Init();
    }
    void Update()
    {
        LestTime -= Time.deltaTime;
        /*
        if (LestTime < 0)
            Managers.UI.ClosePopupUI();
        */
            
    }
    public override void Init()
    {
        base.Init();
        //LestTime 데이터에서 추가하기 
        Bind<TextMeshProUGUI>(typeof(Texts));
        GetText((int)Texts.RestTime).text = LestTime.ToString();
        
    }
}
