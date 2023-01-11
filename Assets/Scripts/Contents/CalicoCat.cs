using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalicoCat : MonoBehaviour
{
    void Start()
    {
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= Test;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += Test;
    }

    void Test()
    {
        Debug.Log("BlackCat Skill");
        GetComponentInParent<Stat>().MoveSpeed += 2f;

        StartCoroutine(CancleSkill());
    }

    IEnumerator CancleSkill()
    {
        yield return new WaitForSeconds(3f);
        GetComponentInParent<Stat>().MoveSpeed -= 2f;
    }
}
