using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    public Vector3 MoveVec { get; set; }

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Player;

    [SerializeField]
    int speed = 5;

    [SerializeField]
    int hp = 3;
    public int Hp
    {
        get { return hp; }
        set
        {
            if (hp < 0)
                hp = 0;
            hp = value;
            (Managers.UI.SceneUI as UI_GameScene).SetHeartUI(hp);
        }
    }

    public int MaxHp { get; private set; } = 3;

    Define.State _state = Define.State.Idle;

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    break;
                case Define.State.Idle:
                    anim.Play("Idle");
                    break;
                case Define.State.Walk:
                    anim.Play("Walk");
                    break;
            }
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = MoveVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
}
