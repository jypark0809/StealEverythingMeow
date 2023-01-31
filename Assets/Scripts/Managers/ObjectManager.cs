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
