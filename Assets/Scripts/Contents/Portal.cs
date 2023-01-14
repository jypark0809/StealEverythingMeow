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
        Managers.Object.Player.Stat.Stage++;
        Managers.UI.ShowPopupUI<UI_Blocker>();
    }
}
