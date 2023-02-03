using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SoomUpEffect : UI_Base
{
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        
    }

    private void Update()
    {
        if (!Managers.Game.SaveData.IsSoomUp)
        {
            Destroy(this.gameObject);
        }

    }
}
