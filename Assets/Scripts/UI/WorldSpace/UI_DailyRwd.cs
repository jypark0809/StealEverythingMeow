using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_DailyRwd : UI_Base
{

    int Index;
    Transform Target;
    enum Images
    {
        DailyImage,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<Image>(typeof(Images));

        GetImage((int)Images.DailyImage).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Heart_gray");
    
        switch (Index)
        {
            case 0:
                GetImage((int)Images.DailyImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Items/Wood");
                break;
            case 1:
                GetImage((int)Images.DailyImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Items/Rock");
                break;
            case 2:
                GetImage((int)Images.DailyImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Items/Cotton");
                break;
            case 3:
                GetImage((int)Images.DailyImage).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Gold");
                break;
            case 4:
                GetImage((int)Images.DailyImage).sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Diamond");
                break;
        }
    }

    private void Update()
    {
        this.transform.position = Target.position;
        if (Managers.Game.SaveData.DaysRwd[Index]) 
        {
            Destroy(this.gameObject);
        }
    }

    public void SetInfo(int i, Transform tra)
    {
        Index = i;
        Target = tra;
    }
}
