using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonItem : Item
{
    [SerializeField]
    ItemData itemData;

    ItemGenerator ig;

    private void Start()
    {
        ig = transform.parent.GetComponent<ItemGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ig.isActive = true;
        Managers.Object.Player.Stat.Gold += itemData.Gold * Managers.Object.Player.Stat.Stage;
        Managers.Object.Player.Stat.Exp += itemData.Exp + (Managers.Object.Player.Stat.Stage - 1);

        (Managers.UI.SceneUI as UI_GameScene).UpdateGoldText();
        gameObject.SetActive(false);
    }
}
