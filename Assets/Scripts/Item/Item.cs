using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemData itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Game.SaveData.Gold += itemData.Gold;
        Managers.Object.Player.Stat.Exp += itemData.Exp;

        (Managers.UI.SceneUI as UI_GameScene).UpdateGoldText();
        gameObject.SetActive(false);
    }
}
