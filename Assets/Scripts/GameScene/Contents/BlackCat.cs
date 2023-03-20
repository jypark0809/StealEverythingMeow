using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCat : MonoBehaviour
{
    Coroutine skillCoroutine = null;
    float skillTime = 3;

    void Start()
    {
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
        GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        gameObject.layer = 28;

        yield return new WaitForSeconds(skillTime);

        GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        gameObject.layer = 29;

        skillCoroutine = null;
    }
}
