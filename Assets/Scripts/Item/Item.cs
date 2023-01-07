using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
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
        Managers.Game.SaveData.Gold += itemData.Gold;
        Managers.Object.Player.Stat.Exp += itemData.Exp;

        (Managers.UI.SceneUI as UI_GameScene).UpdateGoldText();
        gameObject.SetActive(false);
    }
}
