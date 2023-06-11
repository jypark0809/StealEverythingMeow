using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalicoCat : MonoBehaviour
{
    Stat _stat;
    Coroutine skillCoroutine = null;
    float skillTime = 3;

    void Start()
    {
        _stat = GetComponentInParent<Stat>();
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= SkillAction;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += SkillAction;
    }

    void Update()
    {

    }

    void SkillAction()
    {
        if (skillCoroutine == null)
        {
            skillCoroutine = StartCoroutine(UseSkill(skillTime));
        }
    }

    IEnumerator UseSkill(float skillTime)
    {
        _stat.MoveSpeed += 2;
        yield return new WaitForSeconds(skillTime);
        _stat.MoveSpeed -= 2;

        skillCoroutine = null;
    }
}
