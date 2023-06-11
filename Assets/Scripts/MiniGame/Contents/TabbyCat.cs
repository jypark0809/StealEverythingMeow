using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabbyCat : MonoBehaviour
{
    public Coroutine skillCoroutine = null;
    float skillTime = 3;

    void Start()
    {
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= SkillAction;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += SkillAction;
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
        Time.timeScale = 0.5f;

        yield return new WaitForSecondsRealtime(skillTime);

        Time.timeScale = 1f;

        skillCoroutine = null;
    }
}
