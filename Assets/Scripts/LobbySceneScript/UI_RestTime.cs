using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RestTime : UI_Base
{
    public float resttime = 20f;
    public TextMeshProUGUI text;

    enum Texts
    {
        TimeText,
        
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        text = GetText((int)Texts.TimeText);
        text.text = Mathf.Floor(resttime).ToString();
    }
    void Update()
    {
        resttime -= Time.deltaTime;
        text.text = Mathf.Floor(resttime).ToString();
        if (resttime <= 0)
            Destroy(this.gameObject);

    }
}
