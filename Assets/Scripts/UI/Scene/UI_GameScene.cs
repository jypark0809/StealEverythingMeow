using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    enum GameObjects
    {
        Joystick,
        Circle,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        // GetObject((int)GameObjects.Joystick).BindEvent();
    }

    void Update()
    {
        
    }
}
