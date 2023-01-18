using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DestroyableObjectPopup : UI_Popup
{
    int _id;
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

            if (_touchCount == _maxTouchCount)
            {
                Managers.UI.ClosePopupUI();
            }
        } 
    }
    int _maxTouchCount;
    Vector3 _originPos;

    enum GameObjects
    {
        TabBar,
    }

    enum Images
    {
        ObjectImage,
        TouchPanel,
    }

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        _originPos = GetImage((int)Images.ObjectImage).gameObject.transform.position;

        _id = PlayerPrefs.GetInt("DestoryableObject");
        Managers.Data.DestroyableObjects.TryGetValue(_id, out _object);

        _maxTouchCount = _object.Touch_Count;
        GetImage((int)Images.ObjectImage).sprite = Managers.Resource.Load<Sprite>(_object.Image_Path);
        GetImage((int)Images.ObjectImage).SetNativeSize();
        GetImage((int)Images.TouchPanel).gameObject.BindEvent(OnTouchPanelClicked);
    }

    void OnTouchPanelClicked(PointerEventData evt)
    {
        TouchCount++;

        // Shake Object
        StartCoroutine(ShakeObject(_shakePower, _shakeTime));
    }

    [SerializeField]
    float _shakePower = 5f;
    [SerializeField]
    float _shakeTime = 1f;

    IEnumerator ShakeObject(float shakePower, float duration)
    {
        while (duration > 0)
        {
            GetImage((int)Images.ObjectImage).gameObject.transform.position +=
                Random.insideUnitSphere * shakePower;
            duration -= Time.deltaTime;
            yield return null;
        }
        GetImage((int)Images.ObjectImage).gameObject.transform.position = _originPos;
    }

    private void OnDestroy()
    {
        Managers.Object.Player.Stat.Gold += _object.Object_Gold;
        Managers.Object.Player.Stat.Diamond += _object.Object_Diamond;
        (Managers.UI.SceneUI as UI_GameScene).UpdateGoldText();
    }
}
