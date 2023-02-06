using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Soom : MonoBehaviour
{
    int pointerId;

    public bool IsWood;
    public bool IsStone;
    public bool IsCotton;
    public bool IsRoom;
    public bool IsFur;

    private int CurSoomLevel;

    public bool Effect;
    private void Awake()
    {
        CurSoomLevel = Managers.Game.SaveData.SoomLevel;
        this.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(("Sprites/Furniture/Soom/" + Managers.Data.Sooms[1300 + CurSoomLevel].Soom_Int_Name));

#if UNITY_EDITOR
        pointerId = -1;
#elif UNITY_ANDROID
        pointerId = 0;
#endif
    }

    private void Update()
    {
        if (CurSoomLevel < 3)
            IsUpgrdaeCheck();
        if (Managers.Game.SaveData.IsSoomUp && !Effect)
        {
            Managers.UI.MakeWorldSpaceUI<UI_SoomUpEffect>();
            Effect = true;
        }

    }
    private void OnMouseDown()
    {
        /*
        if(CurSoomLevel == 0)// 튜토리얼
            SomUpgrade();
        else if (CurSoomLevel <3)
            Managers.UI.ShowPopupUI<UI_UpgradeSom>(); 
        */
        if (!EventSystem.current.IsPointerOverGameObject(pointerId))
        {
            Managers.UI.ShowPopupUI<UI_UpgradePopUp>();
        }
    }

    public void SomUpgrade()
    {
        CurSoomLevel++;
        Managers.Game.SaveData.SoomLevel++;
        Managers.Game.SaveData.CatCount += 2;
        this.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(("Sprites/Furniture/Soom/" + Managers.Data.Sooms[1300 + CurSoomLevel].Soom_Int_Name));
        Managers.Sound.Play(Define.Sound.Effect, "Effects/SomOpen");

        //행복도 증가
        for (int i = 0; i < Managers.Game.SaveData.CatHave.Length; i++)
        {
            if (Managers.Game.SaveData.CatHave[i])
                Managers.Game.SaveData.CatCurHappinessExp[i] += Managers.Data.Sooms[1300 + CurSoomLevel].Happiness;
        }

        if (CurSoomLevel == 2)
        {
            Managers.Game.SaveData.Emotion[14] = true;
            Managers.Game.SaveData.Emotion[15] = true;
            Managers.UI.ShowPopupUI<UI_ExpressOpen>().Setinfo(14, 15);
            Camera.main.GetComponent<CameraMove>().Index = 1;
        }
        if (CurSoomLevel == 3)
        {
            Managers.Game.SaveData.Emotion[1] = true;
            Managers.Game.SaveData.Emotion[4] = true;
            Managers.UI.ShowPopupUI<UI_ExpressOpen>().Setinfo(1, 4);
            Camera.main.GetComponent<CameraMove>().Index = 2;
            Managers.Game.SaveData.IsSoomUp = false;
        }

        Managers.Game.SaveGame();
    }
    private void IsUpgrdaeCheck()
    {
        if (Managers.Game.SaveData.Wood >= Managers.Data.Sooms[1300 + CurSoomLevel + 1].Wood)
            IsWood = true;
        else
            IsWood = false;
        if (Managers.Game.SaveData.Stone >= Managers.Data.Sooms[1300 + CurSoomLevel + 1].Stone)
            IsStone = true;
        else
            IsStone = false;
        if (Managers.Game.SaveData.Cotton >= Managers.Data.Sooms[1300 + CurSoomLevel + 1].Cotton)
            IsCotton = true;
        else
            IsCotton = false;
        if (Managers.Game.SaveData.SpaceLevel == Managers.Data.Sooms[1300 + CurSoomLevel].Space_Num)
            IsRoom = true;
        else
            IsRoom = false;

        if (Managers.Game.SaveData.FList.Count == Managers.Data.Sooms[1300 + CurSoomLevel].Space_F_Count)
            IsFur = true;
        else
            IsFur = false;

        if (IsWood & IsStone & IsCotton & IsFur & IsRoom)
        {
            Managers.Game.SaveData.IsSoomUp = true;
        }
        else
        {
            Managers.Game.SaveData.IsSoomUp = false;
        }
    }
}
