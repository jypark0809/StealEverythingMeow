using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Item
{
    [SerializeField]
    float plusTime = 10;

    ItemGenerator ig;

    private void Start()
    {
        ig = transform.parent.GetComponent<ItemGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Managers.Sound.Play(Define.Sound.Effect, "Effects/GetClock", volume: 0.4f);
            Managers.Object.ShowTimeText(transform.position);

            ig.isActive = true;
            (Managers.UI.SceneUI as UI_GameScene).GetClockItem(plusTime);
            gameObject.SetActive(false);
        }
    }
}