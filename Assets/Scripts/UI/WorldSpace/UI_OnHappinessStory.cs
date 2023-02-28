using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnHappinessStory : UI_Base
{
    private Transform Target;
    private int Index;
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        this.transform.position = Target.position;
    }


    private void Update()
    {
        this.transform.position = Target.position;
        if (Managers.Game.SaveData.IsViewStory[Index])
            Destroy(this.gameObject);
    }

    public void SetInfo(int i,Transform tra)
    {
        Index = i;
        Target = tra;
    }
}
