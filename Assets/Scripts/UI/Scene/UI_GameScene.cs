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

    enum GameObjects
    {
        JoystickPanel,
        OutLineCircle,
        FiiledCircle,
    }

    enum Texts
    {
        TimeText,
        GoldText,
    }

    enum Images
    {
        Heart1,
        Heart2,
        Heart3,
    }

    enum Buttons
    {
        PauseButton,
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
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetObject((int)GameObjects.JoystickPanel).BindEvent(OnPointerDown, Define.UIEvent.PointerDown);
        GetObject((int)GameObjects.JoystickPanel).BindEvent(OnPointerUp, Define.UIEvent.PointerUp);
        GetObject((int)GameObjects.JoystickPanel).BindEvent(OnDrag, Define.UIEvent.Drag);

        GetText((int)Texts.GoldText).text = Managers.Game.SaveData.Coin.ToString();

        Managers.Resource.Load<Sprite>("Art/Sprites/UI/Heart_gray");
        Managers.Resource.Load<Sprite>("Art/Sprites/UI/Heart_red");

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
        Vector3 endDragPosition = evt.position;
        joystickDir = (endDragPosition - beginDragPos).normalized;
        player.MoveVec = joystickDir;

        player.State = Define.State.Walk;
        if (joystickDir.x > 0)
        {
            // Right Animation
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            // Left Animation
            player.GetComponent<SpriteRenderer>().flipX = true;
        }

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

    public void UpdateGoldText()
    {
        GetText((int)Texts.GoldText).text = Managers.Game.SaveData.Coin.ToString();
    }
}
