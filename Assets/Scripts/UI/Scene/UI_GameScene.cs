using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        player = Managers.Game.GetPlayer().GetComponent<PlayerController>();
        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.JoystickPanel).BindEvent(OnPointerDown, Define.UIEvent.PointerDown);
        GetObject((int)GameObjects.JoystickPanel).BindEvent(OnPointerUp, Define.UIEvent.PointerUp);
        GetObject((int)GameObjects.JoystickPanel).BindEvent(OnDrag, Define.UIEvent.Drag);

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
}
