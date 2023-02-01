using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    PlayerController _player;
    public PlayerController Player { get { return _player; } set { _player = value; } }

    CameraController _camera;
    public CameraController Camera
    {
        get
        {
            if (_camera == null)
                _camera = GameObject.Find("Main Camera").GetComponent<CameraController>();

            return _camera;
        }
    }

    Grid _catHouse;
    public Grid CatHouse { get { return _catHouse; } set { _catHouse = value; } }

    GameObject _stage;
    public GameObject Stage { get { return _stage; } set { _stage = value; } }


    Cat_Lobby _catLobbyWhite;
    Cat_Lobby _catLobbyBlack;
    Cat_Lobby _catLobbyGray;
    Cat_Lobby _catLobbyCalico;
    Cat_Lobby _catLobbyTabby;


    public Cat_Lobby CatLobbyWhite { get { return _catLobbyWhite; } set { _catLobbyWhite = value; } }
    public Cat_Lobby CatLobbyBlack { get { return _catLobbyBlack; } set { _catLobbyBlack = value; } }
    public Cat_Lobby CatLobbyGray { get { return _catLobbyGray; } set { _catLobbyGray = value; } }
    public Cat_Lobby CatLobbyCalico { get { return _catLobbyCalico; } set { _catLobbyCalico = value; } }
    public Cat_Lobby CatLobbyTabby { get { return _catLobbyTabby; } set { _catLobbyTabby = value; } }

    public ObjectManager()
    {
        Init();
    }

    public void Init()
    {

    }

    public GameObject SpawnPlayer(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        _player = go.GetOrAddComponent<PlayerController>();
        return go;
    }

    public void SpawnCatHouse(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        _catHouse = go.GetOrAddComponent<Grid>();
    }

    public void SpawnCat(string path, Transform parent = null)
    {
        if (Managers.Game.SaveData.CatHave[0])
        {
            GameObject go1 = Managers.Resource.Instantiate(path+ "White", parent);
            _catLobbyWhite = go1.GetOrAddComponent<Cat_Lobby>();
        }
        if (Managers.Game.SaveData.CatHave[1])
        {
            GameObject go2 = Managers.Resource.Instantiate(path+ "Black", parent);
            _catLobbyBlack = go2.GetOrAddComponent<Cat_Lobby>();
        }
        if (Managers.Game.SaveData.CatHave[2])
        {
            GameObject go3 = Managers.Resource.Instantiate(path + "Gray", parent);
            _catLobbyGray = go3.GetOrAddComponent<Cat_Lobby>();
        }
        if (Managers.Game.SaveData.CatHave[3])
        {
            GameObject go4 = Managers.Resource.Instantiate(path + "Calico", parent);
            _catLobbyCalico = go4.GetOrAddComponent<Cat_Lobby>();
        }
        if (Managers.Game.SaveData.CatHave[4])
        {
            GameObject go5 = Managers.Resource.Instantiate(path + "Tabby", parent);
            _catLobbyTabby = go5.GetOrAddComponent<Cat_Lobby>();
        }

    }

    public GameObject SpawnStage(string path, Transform parent = null)
    {
        _stage = Managers.Resource.Instantiate(path, parent);
        return _stage;
    }

    public void ShowGoldText(Vector2 pos, int gold)
    {
        GameObject go = Managers.Resource.Instantiate("Item/GoldText");
        GoldText goldText = go.GetOrAddComponent<GoldText>();
        goldText.SetInfo(pos, gold);
    }
}
