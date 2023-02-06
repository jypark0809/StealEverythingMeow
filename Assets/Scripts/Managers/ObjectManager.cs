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


    Cat_LobbyHappniess _catLobbyWhite;
    Cat_LobbyHappniess _catLobbyBlack;
    Cat_LobbyHappniess _catLobbyGray;
    Cat_LobbyHappniess _catLobbyCalico;
    Cat_LobbyHappniess _catLobbyTabby;


    public Cat_LobbyHappniess CatLobbyWhite { get { return _catLobbyWhite; } set { _catLobbyWhite = value; } }
    public Cat_LobbyHappniess CatLobbyBlack { get { return _catLobbyBlack; } set { _catLobbyBlack = value; } }
    public Cat_LobbyHappniess CatLobbyGray { get { return _catLobbyGray; } set { _catLobbyGray = value; } }
    public Cat_LobbyHappniess CatLobbyCalico { get { return _catLobbyCalico; } set { _catLobbyCalico = value; } }
    public Cat_LobbyHappniess CatLobbyTabby { get { return _catLobbyTabby; } set { _catLobbyTabby = value; } }
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
        if (Managers.Game.SaveData.CatHave[(int)Define.CatType.White])
        {
            GameObject go1 = Managers.Resource.Instantiate(path+ "White", parent);
            _catLobbyWhite = go1.GetOrAddComponent<Cat_LobbyHappniess>();
        }
        if (Managers.Game.SaveData.CatHave[(int)Define.CatType.Black])
        {
            GameObject go2 = Managers.Resource.Instantiate(path+ "Black", parent);
            _catLobbyBlack = go2.GetOrAddComponent<Cat_LobbyHappniess>();
        }
        if (Managers.Game.SaveData.CatHave[(int)Define.CatType.Grey])
        {
            GameObject go3 = Managers.Resource.Instantiate(path + "Gray", parent);
            _catLobbyGray = go3.GetOrAddComponent<Cat_LobbyHappniess>();
        }
        if (Managers.Game.SaveData.CatHave[(int)Define.CatType.Calico])
        {
            GameObject go4 = Managers.Resource.Instantiate(path + "Thcolor", parent);
            _catLobbyCalico = go4.GetOrAddComponent<Cat_LobbyHappniess>();
        }
        if (Managers.Game.SaveData.CatHave[(int)Define.CatType.Tabby])
        {
            GameObject go5 = Managers.Resource.Instantiate(path + "Cheeze", parent);
            _catLobbyTabby = go5.GetOrAddComponent<Cat_LobbyHappniess>();
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
