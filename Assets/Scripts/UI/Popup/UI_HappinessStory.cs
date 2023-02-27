using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_HappinessStory : UI_Popup
{
    int Index;

    enum GameObjects
    {

    }

    enum Buttons
    {

    }
    enum Texts
    {
        HappyText
    }

    enum Images
    {
        HappyImages
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetText((int)Texts.HappyText).text = Managers.Data.CatBooks[1401 + Index].End_Story1;
    }
    

    public void SetInfo(int _index)
    {
        Index = _index;
    }
}
