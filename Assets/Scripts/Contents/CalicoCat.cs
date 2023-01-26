using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalicoCat : MonoBehaviour
{
    Stat _stat;
    Coroutine skillCoroutine = null;
    Coroutine coolTimeCoroutine = null;
    UI_CoolTimeBar _coolTimeBar;
    float timer = 0;
    float skillTime = 3;
    float cooltime;

    void Start()
    {
        _stat = GetComponentInParent<Stat>();
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= SkillAction;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += SkillAction;

        if (gameObject.GetComponentInChildren<UI_CoolTimeBar>() == null)
            _coolTimeBar = Managers.UI.MakeWorldSpaceUI<UI_CoolTimeBar>(transform);

        cooltime = Managers.Object.Player.Stat.CoolTime;
    }

    void Update()
    {
        if (coolTimeCoroutine != null)
        {
            timer += Time.deltaTime;
            float ratio = timer / cooltime;
            _coolTimeBar.SetCoolTimeRatio(ratio);
        }
    }

    void SkillAction()
    {
        _coolTimeBar.gameObject.SetActive(true);

        if (skillCoroutine == null)
        {
            skillCoroutine = StartCoroutine(UseSkill(skillTime));
        }

        if (coolTimeCoroutine == null)
        {
            coolTimeCoroutine = StartCoroutine(SetSkillCoolTime(cooltime));
        }
    }

    IEnumerator UseSkill(float skillTime)
    {
        cooltime = Managers.Object.Player.Stat.CoolTime;

        _stat.MoveSpeed += 2;
        yield return new WaitForSeconds(skillTime);
        _stat.MoveSpeed -= 2;
    }

    IEnumerator SetSkillCoolTime(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        skillCoroutine = null;
        coolTimeCoroutine = null;
        timer = 0;
        _coolTimeBar.gameObject.SetActive(false);
        skillCoroutine = null;
    }
}
