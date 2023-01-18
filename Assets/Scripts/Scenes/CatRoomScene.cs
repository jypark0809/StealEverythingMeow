using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRoomScene : BaseScene
{
    UI_CatHouseScene _catRoomScene;
    UI_CatHouseScene_Upper _catHouseScene_Upper;
    UI_CatHouse _catHouse;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.CatRoomScene;

        StartCoroutine(CoWaitLoad());
    }

    IEnumerator CoWaitLoad()
    {
        while (Managers.Data.Loaded() == false)
            yield return null;

        Managers.Object.SpawnCatHouse("CatHouse2");

        _catHouse = Managers.UI.ShowSceneUI<UI_CatHouse>();
        _catHouseScene_Upper = Managers.UI.ShowSceneUI<UI_CatHouseScene_Upper>();

        
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
