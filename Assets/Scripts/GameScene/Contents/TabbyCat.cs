using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabbyCat : MonoBehaviour
{
    void Start()
    {
        (Managers.UI.SceneUI as UI_GameScene).skillHandler -= Test;
        (Managers.UI.SceneUI as UI_GameScene).skillHandler += Test;
    }

    void Test()
    {
        Debug.Log("Skill");
    }
}
