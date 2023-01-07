using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    float respawnTime = 60;

    float timer;

    void Start()
    {
        timer = respawnTime;
    }

    private void Update()
    {
        //if (gameObject.activeSelf == false)
        //{
        //    timer -= Time.deltaTime;
        //    if (timer < 0)
        //    {
        //        gameObject.SetActive(true);
        //        timer = respawnTime;
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Object.Player.Stat.Hp += 1;
        gameObject.SetActive(false);
    }
}
