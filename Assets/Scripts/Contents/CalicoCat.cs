using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalicoCat : MonoBehaviour
{
    Stat _stat;

    void Start()
    {
        _stat = GetComponentInParent<Stat>();
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= Test;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += Test;
    }

    void Test()
    {
        _stat.MoveSpeed += 2;

        StartCoroutine(CancleSkill());
    }

    IEnumerator CancleSkill()
    {
        yield return new WaitForSeconds(3f);
        _stat.MoveSpeed -= 2;
    }
}
