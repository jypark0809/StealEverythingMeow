using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_FoodUpSet : UI_Base
{
    private float UpText;
    private Transform Target;
    private float DeleteTime = 2f;

    enum Texts
    {
        Food_Up,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.Food_Up).text = " + " + UpText.ToString();
        this.transform.position = Target.position;
    }


    private void Update()
    {
        this.transform.position = Target.position;
        DeleteTime -= Time.deltaTime;
        if (DeleteTime <= 0)
            Destroy(this.gameObject);
    }

    public void SetInfo(int i, Transform tra)
    {
        UpText = i;
        Target = tra;
    }
}