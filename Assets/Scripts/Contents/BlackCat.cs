using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCat : MonoBehaviour
{
    void Start()
    {
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= Test;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += Test;
    }

    void Update()
    {
        
    }

    void Test()
    {
        GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        gameObject.layer = 28;

        StartCoroutine(CancleSkill());
    }

    IEnumerator CancleSkill()
    {
        yield return new WaitForSeconds(3f);
        GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        gameObject.layer = 29;
    }
}
