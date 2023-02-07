using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DestroyableObjectPopup : UI_Popup
{
    DestroyableObjectData _object;
    int _touchCount;
    int TouchCount
    {
        get { return _touchCount; }
        set
        {
            if (_touchCount < _maxTouchCount)
                _touchCount = value;

            GetObject((int)GameObjects.TabBar).GetComponent<Slider>().value = (float)_touchCount/_maxTouchCount;

            if ((float)_touchCount / _maxTouchCount > 0.5)
            {
                GetImage((int)Images.ObjectImage).sprite = _sprites[1];
            }

            if (_touchCount == _maxTouchCount)
            {
                GetImage((int)Images.ObjectImage).sprite = _sprites[2];
            }

            if (_touchCount == _maxTouchCount)
            {
                StartCoroutine(GetReward());
            }
        } 
    }
    int _maxTouchCount;
    Vector3 _originPos;
    Sprite[] _sprites;
    Transform _imageTransform;
    bool isShake = false;

    [SerializeField]
    float _shakePower = 10f;
    float _shakeTime;

    enum GameObjects
    {
        TabBar,
    }

    enum Images
    {
        ObjectImage,
        TouchPanel,
    }

    void Awake()
    {
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (TouchCount >= _maxTouchCount)
            return;

        if (_shakeTime > 0)
        {
            _shakeTime -= Time.deltaTime;
            _imageTransform.position += Random.insideUnitSphere * _shakePower;
        }
        else
        {
            _imageTransform.position = _originPos;
        }
    }

    public override void Init()
    {
        base.Init();

        _imageTransform = GetImage((int)Images.ObjectImage).gameObject.transform;
        _originPos = GetImage((int)Images.ObjectImage).gameObject.transform.position;
        _sprites = Resources.LoadAll<Sprite>(_object.Image_Path);

        _maxTouchCount = _object.Touch_Count;
        GetImage((int)Images.ObjectImage).sprite = _sprites[0];
        GetImage((int)Images.ObjectImage).SetNativeSize();
        GetImage((int)Images.TouchPanel).gameObject.BindEvent(OnTouchPanelClicked);
    }

    public void SetInfo(DestroyableObjectData objectData)
    {
        _object = objectData;
    }

    void OnTouchPanelClicked(PointerEventData evt)
    {
        if (TouchCount < _maxTouchCount)
        {
            TouchCount++;

            PlayRandomSound();
            Vibration.Vibrate((long)50);

            // Shake Object
            _shakeTime += 0.3f;
        }
    }

    private void OnDestroy()
    {
        Managers.Object.Player.Stat.Gold += _object.Object_Gold;
        Managers.Object.Player.Stat.Diamond += _object.Object_Diamond;
        (Managers.UI.SceneUI as UI_GameScene).UpdateGoldText();
    }

    void PlayRandomSound()
    {
        int rand = Random.Range(0, 3);
        switch(rand)
        {
            case 0:
                Managers.Sound.Play(Define.Sound.Effect, "Effects/Punch_01");
                break;
            case 1:
                Managers.Sound.Play(Define.Sound.Effect, "Effects/Punch_02");
                break;
            case 2:
                Managers.Sound.Play(Define.Sound.Effect, "Effects/Punch_03");
                break;
        }
    }

    IEnumerator GetReward()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/Clattering");
        yield return new WaitForSeconds(1f);
        Managers.UI.ClosePopupUI();
    }
}
