using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CatHouseScene : UI_Scene
{
    public UI_CatHoustSceneTop _catHouseSceneTop;

    enum GameObjects
    {
        UI_CatHouseSceneTop
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        _catHouseSceneTop = GetObject((int)GameObjects.UI_CatHouseSceneTop).GetComponent<UI_CatHoustSceneTop>();
    }
}
