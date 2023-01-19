using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    UI_GameScene _gameSceneUI;
    GameObject _player;
    GameObject _stage;
    public float LIMIT_TIME = 121;
    public float _playTime;

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

        SetPlayer();
        Managers.Object.Camera.SetPlayer(_player.GetComponent<PlayerController>());
        SpawnTreasureMap();

        _gameSceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Sound.Play(Define.Sound.Bgm, "BGM/BGM_Game", volume: 0.1f);

        StartCoroutine(PortalScheduler(10));
    }

    private void Update()
    {
        _playTime += Time.deltaTime;
    }

    void SetPlayer()
    {
        switch(PlayerPrefs.GetInt("SelectedCatNum"))
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

        _player.transform.position = Util.FindChild(_stage, "PlayerSpawnPos").transform.position;
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
        if (Managers.Object.Player.Stat.Stage > 3)
            return;

        Transform[] spawnPos = Util.FindChild(_stage, "PortalSpawnPoint", false).GetComponentsInChildren<Transform>();
        Managers.Resource.Instantiate("PortalContainer", _stage.transform).transform.position = spawnPos[Random.Range(1,9)].position;
    }

    public void GoToNextStage()
    {
        // Set Stage
        _stage = Managers.Object.SpawnStage($"Stage/Stage{_player.GetComponent<Stat>().Stage}");

        // Set Player Position
        _player.transform.position = Util.FindChild(_stage, "PlayerSpawnPos").transform.position;

        // Set TreasureMap();
        SpawnTreasureMap();

        StartCoroutine(PortalScheduler(10));
    }

    List<int> indexList = new List<int>();
    void SpawnTreasureMap()
    {
        if (Managers.Object.Player.Stat.Stage > 3)
            return;

        Transform[] spawnPos = Util.FindChild(_stage, "TreasureMapSpawnPoint", false).GetComponentsInChildren<Transform>();
        if (Managers.Object.Player.Stat.Stage == 1 || Managers.Object.Player.Stat.Stage == 2)
        {
            CreateUnDuplicateRandom(1, spawnPos.Length, 2);
            for (int i = 0; i < indexList.Count; i++)
            {
                Managers.Resource.Instantiate("Item/FuntionalItem/TreasureMap",_stage.transform).transform.position = spawnPos[indexList[i]].position;
            }
        }
        else if (Managers.Object.Player.Stat.Stage == 3)
        {
            CreateUnDuplicateRandom(1, spawnPos.Length, 1);
            for (int i = 0; i < indexList.Count; i++)
            {
                Managers.Resource.Instantiate("Item/FuntionalItem/TreasureMap",_stage.transform).transform.position = spawnPos[indexList[i]].position;
            }
        }
    }

    void CreateUnDuplicateRandom(int min, int max, int count)
    {
        int currentNumber = Random.Range(min, max);

        for (int i = 0; i < count;)
        {
            if (indexList.Contains(currentNumber))
            {
                currentNumber = Random.Range(min, max);
            }
            else
            {
                indexList.Add(currentNumber);
                i++;
            }
        }
    }

    IEnumerator PortalScheduler(float time)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }

        Managers.UI.ShowPopupUI<UI_NextStagePopup>();
        SpawnPortal();
    }

    public override void Clear()
    {
        Managers.Resource.Destroy(_stage);
        indexList.Clear();
    }
}
