using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    float plusTime = 30;

    [SerializeField]
    float respawnTime = 30;

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
        (Managers.UI.SceneUI as UI_GameScene).PlusTime(plusTime);
        gameObject.SetActive(false);
    }
}
