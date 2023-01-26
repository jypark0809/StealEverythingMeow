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
        transform.position = Util.FindChild(Managers.Object.CatHouse.gameObject, "Soom", true).transform.position + new Vector3(1,0,0);
    }

    private void Update()
    {
        if (!Managers.Game.SaveData.IsSoomUp)
        {
            Util.FindChild(Managers.Object.CatHouse.gameObject, "Soom", true).GetComponent<Soom>().Effect = false;
            Destroy(this.gameObject);
        }

    }
}
