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

        // SetPlayer();
        _player = Managers.Object.SpawnPlayer("Nyan/Minigame/Cat_Calico");
        _player.transform.position = new Vector3(0, -13, 0);
        Managers.Object.Camera.SetPlayer(_player.GetComponent<PlayerController>());

        _gameSceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 30)
        {
            Managers.UI.ShowPopupUI<UI_NextStagePopup>();
            SpawnPortal();
            timer = -999;
        }
    }

    void SetPlayer()
    {
        switch(PlayerPrefs.GetInt("SelectCat"))
        {
            case 0:
                _player = Managers.Object.SpawnPlayer("Nyan/Minigame/Cat_White");
                break;
            case 1:
                _player = Managers.Object.SpawnPlayer("Nyan/Minigame/Cat_Black");
                break;
            case 2:
                _player = Managers.Object.SpawnPlayer("Nyan/Minigame/Cat_Calico");
                break;
            case 3:
                _player = Managers.Object.SpawnPlayer("Nyan/Minigame/Cat_Tabby");
                break;
            case 4:
                _player = Managers.Object.SpawnPlayer("Nyan/Minigame/Cat_Gray");
                break;
        }

        _player.transform.position = new Vector3(0, -13, 0);
        Managers.Object.Camera.SetPlayer(_player.GetComponent<PlayerController>());
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
        Managers.Resource.Instantiate("PortalContainer").transform.position = spawnPos[Random.Range(1,9)].position;
    }

    public void GoToNextStage()
    {
        _stage = Managers.Object.SpawnStage($"Stage/Stage{_player.GetComponent<Stat>().Stage}");
        _player.transform.position = new Vector3(0, -13, 0);
    }

    public override void Clear()
    {
        Managers.Resource.Destroy(_stage);
    }
}
