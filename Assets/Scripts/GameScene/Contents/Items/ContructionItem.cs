using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContructionItem : Item
{
    [SerializeField]
    int type = 0;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Managers.Sound.Play(Define.Sound.Effect, "Effects/GetItem", volume: 0.4f);

            switch (type)
            {
                case 0:
                    Managers.Object.Player.Stat.Wood += 3;
                    break;
                case 1:
                    Managers.Object.Player.Stat.Rock += 3;
                    break;
                case 2:
                    Managers.Object.Player.Stat.Cotton += 2;
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
