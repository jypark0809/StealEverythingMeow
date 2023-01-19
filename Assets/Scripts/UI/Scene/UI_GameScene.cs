using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    Vector3 beginDragPos; // BeginDrag position
    public Vector3 joystickDir;
    float joystickRadius;
    PlayerController player;
    Animator _playerAnim;

    public Action skillHandler = null;

    UI_Test _testUI;

    enum GameObjects
    {
        JoystickPanel,
        OutLineCircle,
        FiiledCircle,
        ExpBar,
    }

    enum Texts
    {
        TimeText,
        GoldText,
        LevelText,
    }

    enum Images
    {
        Heart1,
        Heart2,
        Heart3,
        TreasureMapImage
    }

    enum Buttons
    {
        PauseButton,
        SkillButton,
    }

    void Update()
    {
        UpdateTime();
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        player = Managers.Object.Player;
        _playerAnim = player.GetComponent<Animator>();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetObject((int)GameObjects.JoystickPanel).BindEvent(OnPointerDown, Define.UIEvent.PointerDown);
        GetObject((int)GameObjects.JoystickPanel).BindEvent(OnPointerUp, Define.UIEvent.PointerUp);
        GetObject((int)GameObjects.JoystickPanel).BindEvent(OnDrag, Define.UIEvent.Drag);

        GetText((int)Texts.GoldText).text = Managers.Game.SaveData.Gold.ToString();
        GetButton((int)Buttons.PauseButton).gameObject.BindEvent(PopupPuaseUI);
        GetButton((int)Buttons.SkillButton).gameObject.BindEvent((PointerEventData evt) => skillHandler?.Invoke());

        joystickRadius = GetObject((int)GameObjects.OutLineCircle).GetComponent<RectTransform>().sizeDelta.y * 1.2f;
        GetObject((int)GameObjects.OutLineCircle).SetActive(false);
        GetObject((int)GameObjects.FiiledCircle).SetActive(false);
        
    }

    void OnPointerDown(PointerEventData evt)
    {
        GetObject((int)GameObjects.OutLineCircle).SetActive(true);
        GetObject((int)GameObjects.FiiledCircle).SetActive(true);
        GetObject((int)GameObjects.OutLineCircle).transform.position = Input.mousePosition;
        GetObject((int)GameObjects.FiiledCircle).transform.position = Input.mousePosition;
        beginDragPos = Input.mousePosition;
    }

    void OnDrag(PointerEventData evt)
    {
        player.State = Define.State.Walk;
        Vector3 endDragPosition = evt.position;
        joystickDir = (endDragPosition - beginDragPos).normalized;
        SetPlayerAnim();
        player.MoveVec = joystickDir;

        // Set FilledCircle Boundary
        float stickDistance = Vector3.Distance(endDragPosition, beginDragPos);
        if (stickDistance < joystickRadius)
        {
            GetObject((int)GameObjects.FiiledCircle).transform.position = beginDragPos + joystickDir * stickDistance;
        }
        else
        {
            GetObject((int)GameObjects.FiiledCircle).transform.position = beginDragPos + joystickDir * joystickRadius;
        }
    }

    void OnPointerUp(PointerEventData evt)
    {
        joystickDir = Vector3.zero;
        player.MoveVec = joystickDir;

        // Idle Animation
        player.State = Define.State.Idle;

        GetObject((int)GameObjects.OutLineCircle).SetActive(false);
        GetObject((int)GameObjects.FiiledCircle).SetActive(false);
    }

    void SetPlayerAnim()
    {
        float Dot = Vector3.Dot(joystickDir, Vector3.up);
        float Angle = Mathf.Acos(Dot) * Mathf.Rad2Deg;

        if (Angle < 22)
        {
            // Top
            _playerAnim.SetFloat("dirX", 0);
            _playerAnim.SetFloat("dirY", 1);
        }
        else if (Angle >= 22 && Angle < 67)
        {
            if (joystickDir.x > 0)
            {
                // Top Right
                _playerAnim.SetFloat("dirX", 1);
                _playerAnim.SetFloat("dirY", 1);
            }
            else
            {
                // Top Left
                _playerAnim.SetFloat("dirX", -1);
                _playerAnim.SetFloat("dirY", 1);
            }
        }
        else if (Angle >= 67 && Angle < 112)
        {
            if (joystickDir.x > 0)
            {
                // Right
                _playerAnim.SetFloat("dirX", 1);
                _playerAnim.SetFloat("dirY", 0);
            }
            else
            {
                // Left
                _playerAnim.SetFloat("dirX", -1);
                _playerAnim.SetFloat("dirY", 0);
            }
        }
        else if (Angle >= 112 && Angle < 157)
        {
            if (joystickDir.x > 0)
            {
                // Botton Right
                _playerAnim.SetFloat("dirX", 1);
                _playerAnim.SetFloat("dirY", -1);
            }
            else
            {
                // Bottom Left
                _playerAnim.SetFloat("dirX", -1);
                _playerAnim.SetFloat("dirY", -1);
            }
        }
        else if (Angle >= 157)
        {
            // Bottom
            _playerAnim.SetFloat("dirX", 0);
            _playerAnim.SetFloat("dirY", -1);
        }
    }

    int min, sec;
    float limitTime = 121;
    void UpdateTime()
    {
        if (limitTime < 0)
        {
            limitTime = 0;
            (Managers.Scene.CurrentScene as GameScene).GameOver();
        }
        else
        {
            limitTime -= Time.deltaTime;
            min = (int)limitTime / 60;
            sec = (int)limitTime % 60;
            string result = sec.ToString("D2");
            GetText((int)Texts.TimeText).text = $"{min}:{result}";
        }
    }

    public void PlusTime(float time)
    {
        limitTime += time;
    }

    public void SetHeartUI(int hp)
    {
        switch (hp)
        {
            case 3:
                GetImage((int)Images.Heart1).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_red");
                GetImage((int)Images.Heart2).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_red");
                GetImage((int)Images.Heart3).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_red");
                break;
            case 2:
                GetImage((int)Images.Heart1).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_red");
                GetImage((int)Images.Heart2).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_red");
                GetImage((int)Images.Heart3).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_gray");
                break;
            case 1:
                GetImage((int)Images.Heart1).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_red");
                GetImage((int)Images.Heart2).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_gray");
                GetImage((int)Images.Heart3).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_gray");
                break;
            case 0:
                GetImage((int)Images.Heart1).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_gray");
                GetImage((int)Images.Heart2).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_gray");
                GetImage((int)Images.Heart3).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_gray");
                (Managers.Scene.CurrentScene as GameScene).GameOver();
                break;
        }
    }

    public void SetExpBar(int curExp, int maxExp)
    {
        GetObject((int)GameObjects.ExpBar).GetComponent<Slider>().value = (float)curExp/maxExp;
    }

    public void UpdateGoldText()
    {
        GetText((int)Texts.GoldText).text = Managers.Object.Player.Stat.Gold.ToString();
    }

    public void UpdateLevelText()
    {
        GetText((int)Texts.LevelText).text = $"Lv.{Managers.Object.Player.Stat.Level.ToString()}";
    }

    public void UpdateTreasureMapImage(int curMapCount, int maxMapCount)
    {
        GetImage((int)Images.TreasureMapImage).fillAmount = (float)curMapCount / maxMapCount;
    }

    #region EventHandler
    void PopupPuaseUI(PointerEventData evt)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
        Time.timeScale = 0;
        Managers.UI.ShowPopupUI<UI_PauseGamePopup>();
    }
    #endregion
}
