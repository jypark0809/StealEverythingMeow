using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayCat : MonoBehaviour
{
    TreasureMap _map;
    Coroutine skillCoroutine = null;
    float skillTime = 5;

    void Start()
    {
        _map = Managers.Object.Map.GetComponent<TreasureMap>();
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
        _map.isIndicate = true;
        yield return new WaitForSeconds(skillTime);
        _map.isIndicate = false;

        skillCoroutine = null;
    }

    public void SetNewMap()
    {
        _map = Managers.Object.Map.GetComponent<TreasureMap>();
    }
}
