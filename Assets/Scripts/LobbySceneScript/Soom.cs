using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soom : MonoBehaviour
{ 
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
        Managers.Object.CatHouse.gameObject.GetComponent<TileManager>().Open();
        /*
        if(CurSoomLevel == 0)// 튜토리얼
            SomUpgrade();
        else if (CurSoomLevel <3)
            Managers.UI.ShowPopupUI<UI_UpgradeSom>(); 
        */
    }

    public void SomUpgrade()
    {
        CurSoomLevel++;
        Managers.Game.SaveData.SoomLevel++; //추후합치고 변수 재정립하기 
        this.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(("Sprites/Furniture/" + Managers.Data.Sooms[1300 + CurSoomLevel].Soom_Int_Name));
        Managers.Sound.Play(Define.Sound.Effect, "Effects/SomOpen");
        if(Managers.Game.SaveData.SoomLevel == 3)
        {
            Managers.Game.SaveData.IsSoomUp = false;
        }

    }
    //좀더 효율적인코드가있을것같은기분..
    private void IsUpgrdaeCheck()
    {
        if (Managers.Game.SaveData.Wood >= Managers.Data.Sooms[1300 + CurSoomLevel + 1].Wood)
            IsWood = true;
        if (Managers.Game.SaveData.Stone >= Managers.Data.Sooms[1300 + CurSoomLevel + 1].Stone)
            IsStone = true;
        if (Managers.Game.SaveData.Cotton >= Managers.Data.Sooms[1300 + CurSoomLevel + 1].Cotton)
            IsCotton = true;

        //방체크,가구체크 >>가구경우 상점과 확인후 다시 수정필요
        if (Managers.Game.SaveData.SpaceLevel == Managers.Data.Sooms[1300 + CurSoomLevel + 1].Space_Num)
            IsRoom = true;

        /*
        int CurFur = 0;
        for (int i = 0; i < Managers.Game.SaveData.RoomLevel; i++)
        {
            CurFur += Managers.Game.SaveData.MaxFurniture[i];
        }
        if (CurFur == Managers.Data.Sooms[1300 + CurSoomLevel + 1].Space_F_Count)
            IsFur = true;
        */

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
