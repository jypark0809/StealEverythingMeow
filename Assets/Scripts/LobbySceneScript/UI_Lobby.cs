using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UI_Lobby : UI_Scene
{
    enum Buttons
    {
        Setting,
        Store,
        DoGAM,
        Quest,
        Mail,
        Bag,
    }


    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Button go = GetButton((int)Buttons.Store);
        go.onClick.AddListener(StoreOpen);
    }
    public void StoreOpen()
    {
        Managers.Resource.Instantiate("UI/Popup/Store_UI");
    }
 
}
