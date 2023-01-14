using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabbyCat : MonoBehaviour
{
    Rigidbody2D _rigid;

    void Start()
    {
        _rigid = GetComponentInParent<Rigidbody2D>();
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= Test;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += Test;
    }

    private void FixedUpdate()
    {
        _rigid.AddForce(new Vector2(1, 1) * 5f, ForceMode2D.Impulse);
    }

    void Test()
    {
        Managers.Object.Player.State = Define.State.Jump;

    }
}
