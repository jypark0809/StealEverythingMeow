using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRoomScene : BaseScene
{
    UI_CatHouseScene _catRoomScene;

    protected override void Init()
    {
        base.Init();

        // SceneType = Define.SceneType.GameScene;

        StartCoroutine(CoWaitLoad());
    }

    IEnumerator CoWaitLoad()
    {
        while (Managers.Data.Loaded() == false)
            yield return null;

        Managers.Object.SpawnCatHouse("CatHouse");

        _catRoomScene = Managers.UI.ShowSceneUI<UI_CatHouseScene>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Clear()
    {

    }
}
