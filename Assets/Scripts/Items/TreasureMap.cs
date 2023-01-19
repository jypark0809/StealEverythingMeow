using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMap : Item
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Managers.Object.Player.Stat.Map++;
            gameObject.SetActive(false);
        }
    }
}
