using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHouseScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.CatHouseScene;
        StartCoroutine(CoWaitLoad());
    }

    IEnumerator CoWaitLoad()
    {
        while (Managers.Data.Loaded() == false)
            yield return null;

        Managers.Object.SpawnCatHouse("CatHouse_" +Managers.Game.SaveData.SpaceLevel);
        Managers.Object.SpawnCat("LobbyCat/");
        Managers.UI.ShowSceneUI<UI_CatHouseScene>();
        Managers.Sound.Play(Define.Sound.Bgm, "BGM/BGM_Home", volume: 0.1f);
    }

    public override void Clear()
    {
    }
}
