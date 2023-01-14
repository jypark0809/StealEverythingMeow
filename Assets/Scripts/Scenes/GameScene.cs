using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    UI_GameScene _gameSceneUI;
    GameObject _player;
    GameObject _stage;
    float timer = 0;

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

        _stage = Managers.Object.SpawnStage("Stage/Stage1");
        _player = Managers.Object.SpawnPlayer("Nyan/Minigame/Cat_Calico");
        _player.transform.position = new Vector3(0, -13, 0);
        Managers.Object.Camera.SetPlayer(_player.GetComponent<PlayerController>());

        _gameSceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 110)
        {
            Managers.UI.ShowPopupUI<UI_NextStagePopup>();
            SpawnPortal();
            timer = 0;
        }
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

        Managers.UI.ShowPopupUI<UI_GameOver>();
    }

    void SpawnPortal()
    {
        Transform[] spawnPos = Util.FindChild(_stage, "PortalRandomPoint", false).GetComponentsInChildren<Transform>();
        Managers.Resource.Instantiate("Portal").transform.position = spawnPos[Random.Range(1,9)].position;
    }

    public void GoToNextStage()
    {
        Debug.Log("Next Stage");

        _stage = Managers.Object.SpawnStage($"Stage/Stage{_player.GetComponent<Stat>().Stage}");
        _player.transform.position = new Vector3(0, -13, 0);
    }

    public override void Clear()
    {
        Managers.Resource.Destroy(_stage);

    }
}
