using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCat : MonoBehaviour
{
    Coroutine skillCoroutine = null;
    int skillCount = 3;

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
        if (skillCoroutine == null && skillCount > 0)
        {
            skillCoroutine = StartCoroutine(UseSkill());
        }
    }

    IEnumerator UseSkill()
    {
        skillCount--;
        GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        gameObject.layer = 28;

        yield return new WaitForSeconds(3f);

        GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        gameObject.layer = 29;

        skillCoroutine = null;
    }
}
