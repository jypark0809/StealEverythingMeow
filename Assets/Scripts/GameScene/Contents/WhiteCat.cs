using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCat : MonoBehaviour
{
    Coroutine skillCoroutine = null;

    void Start()
    {
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= SkillAction;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += SkillAction;
    }

    void SkillAction()
    {
        if (skillCoroutine == null)
        {
            skillCoroutine = StartCoroutine(UseSkill());
        }
    }

    IEnumerator UseSkill()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/CatIdle");
        yield return null;

        skillCoroutine = null;
    }
}
