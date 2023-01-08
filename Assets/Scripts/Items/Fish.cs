using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Object.Player.Stat.Hp += 1;
        gameObject.SetActive(false);
    }
}
