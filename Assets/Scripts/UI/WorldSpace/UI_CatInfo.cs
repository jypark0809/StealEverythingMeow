using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CatInfo : UI_Base
{
    public string CatName;
    public int HappyLevel;
    public float CurExp;
    public float NextExp;
    public Transform Target;

    private float DeleteTime = 6f;

    enum GameObjects
    {
        HeartSet,
    }
    enum Texts
    {
        HappyLevel,
        NameText
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.NameText).text = CatName;
        GetText((int)Texts.HappyLevel).text = "LV. " + HappyLevel.ToString();
        SetHappiness();
    }


    private void Update()
    {
        this.transform.position = Target.position;
        DeleteTime -= Time.deltaTime;
        if (DeleteTime <= 0)
            Destroy(this.gameObject);
    }
    void SetHappiness()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.HeartSet);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 1; i < HappyLevel; i++)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_HeartSet");
            Item.transform.SetParent(gridPanel.transform);
            UI_HeartSet HerartSet = Util.GetOrAddComponent<UI_HeartSet>(Item);
            HerartSet.SetInfo(1, 1);
        }
        GameObject Item1 = Managers.Resource.Instantiate("UI/UI_HeartSet");
        Item1.transform.SetParent(gridPanel.transform);
        UI_HeartSet HerartSet1 = Util.GetOrAddComponent<UI_HeartSet>(Item1);
        HerartSet1.SetInfo(CurExp, NextExp);

        for (int i = HappyLevel + 1; i < 6; i++)
        {
            GameObject Item = Managers.Resource.Instantiate("UI/UI_HeartSet");
            Item.transform.SetParent(gridPanel.transform);
            UI_HeartSet HerartSet = Util.GetOrAddComponent<UI_HeartSet>(Item);

        }
    }

    public void SetInfo(string _str, int _int, float _float1, float _float2 ,Transform tra)
    {
        CatName = _str;
        HappyLevel = _int;
        CurExp = _float1;
        NextExp = _float2;
        Target = tra;
    }
}
