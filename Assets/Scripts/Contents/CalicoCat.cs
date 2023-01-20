using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalicoCat : MonoBehaviour
{
    Stat _stat;
    Coroutine skillCoroutine = null;
    int skillCount = 3;

    void Start()
    {
        _stat = GetComponentInParent<Stat>();
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= SkillAction;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += SkillAction;
    }

    void SkillAction()
    {
        if (skillCoroutine == null && skillCount > 0)
        {
            skillCoroutine = StartCoroutine(UseSkill());
        }
    }

    IEnumerator UseSkill()
    {
        skillCount--;
        _stat.MoveSpeed += 2;
        yield return new WaitForSeconds(3f);
        _stat.MoveSpeed -= 2;

        skillCoroutine = null;
    }
}
