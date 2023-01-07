using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chocolate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Object.Player.Stat.Hp -= 1;
        gameObject.SetActive(false);
    }
}
