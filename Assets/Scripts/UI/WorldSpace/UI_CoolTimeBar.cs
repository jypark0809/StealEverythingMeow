using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class UI_CoolTimeBar : UI_Base
{
    enum GameObjects
    {
        CoolTimeBar,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<CircleCollider2D>().bounds.size.y * 1.2f);
    }

    public void SetCoolTimeRatio(float ratio)
    {
        GetObject((int)GameObjects.CoolTimeBar).GetComponent<Slider>().value = ratio;
    }
}
