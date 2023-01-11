using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabbyCat : MonoBehaviour
{
    void Start()
    {
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= Test;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += Test;
    }

    void Test()
    {
        Debug.Log("Tabby Skill");
        Managers.Object.Player.State = Define.State.Jump;


        StartCoroutine(CancleSkill());
    }

    IEnumerator CancleSkill()
    {
        yield return new WaitForSeconds(0.6f);
        Managers.Object.Player.State = Define.State.Idle;
    }
}
