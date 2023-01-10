using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    UI_GameScene _gameSceneUI;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.GameScene;

        StartCoroutine(CoWaitLoad());
    }


    IEnumerator CoWaitLoad()
    {
        while (Managers.Data.Loaded() == false)
            yield return null;
        
        Managers.Object.SpawnPlayer("Nyan/Cat_WhiteTest");
        Managers.Object.Player.transform.position = new Vector3(-0.5f, -12.5f, 0);
        Managers.Object.Camera.SetPlayer(Managers.Object.Player);

        Managers.UI.MakeWorldSpaceUI<UI_Test>();

        _gameSceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();
    }

    public void StageClear()
    {
        Debug.Log("Stage Clear");

        // Get Reward

        // Managers.Game.SaveGame();
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        // State = BattleState.GameOver;
        // Managers.UI.ShowPopupUI<UI_GameOverPopup>();
    }

    public override void Clear()
    {

    }
}
