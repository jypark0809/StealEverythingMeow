using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Nyan/Cat_BlackTest");
        Camera.main.GetComponent<CameraController>().SetPlayer(player);
    }

    void Update()
    {

    }

    public override void Clear()
    {

    }
}
