using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Game.SaveData.Gold += 100;
        (Managers.UI.SceneUI as UI_GameScene).UpdateGoldText();
        gameObject.SetActive(false);
    }
}
