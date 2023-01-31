using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            NextStage();
        }
    }

    void NextStage()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/NextStage", volume: 0.4f);
        Managers.Object.Player.Stat.Stage++;
        Managers.UI.ShowPopupUI<UI_Blocker>();
        gameObject.SetActive(false);
    }
}
