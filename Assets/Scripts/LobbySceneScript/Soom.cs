using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soom : MonoBehaviour
{
    public bool IsRoomOpen = false;
    public bool IsUpgrdae = false;
    public bool IsDia;
    public bool IsWood;
    public bool IsStone;
    public bool IsCotton;
    private int CurSoomLevel;


    private void Awake()
    {
        CurSoomLevel = Managers.Game.SaveData.SoomLevel;
        this.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Furniture/" + Managers.Data.Sooms[1300 + CurSoomLevel].Soom_Img);
    }

    private void Update()
    {
        IsUpgrdaeCheck();
    }
    private void OnMouseDown()
    {
        if (Managers.Game.SaveData.RoomLevel > 6)
            return;

        if (IsRoomOpen)
            Managers.UI.ShowPopupUI<UI_UnlockRoomPopup>();
        else if (IsUpgrdae)
            Managers.UI.ShowPopupUI<UI_UpgradeSom>();
        
        Managers.UI.ShowPopupUI<UI_UnlockRoomPopup>();
    }

    public void SomUpgrade()
    {
        Managers.Game.SaveData.RoomLevel++; //추후합치고 변수 재정립하기 
        this.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Furniture/" + Managers.Data.Sooms[1300 + CurSoomLevel].Soom_Img);
        Managers.Sound.Play(Define.Sound.Effect, "Effects/SomOpen");

    }
    private void IsUpgrdaeCheck()
    {
        //재료체크
        if (Managers.Game.SaveData.Dia >= Managers.Data.Sooms[1300 + CurSoomLevel+1].Diamond)
            IsDia = true;
        else
            return;
        if (Managers.Game.SaveData.Wood >= Managers.Data.Sooms[1300 + CurSoomLevel+1].Wood)
            IsWood = true;
        else
            return;
        if (Managers.Game.SaveData.Stone >= Managers.Data.Sooms[1300 + CurSoomLevel+1].Stone)
            IsStone = true;
        else
            return;
        if (Managers.Game.SaveData.Cotton >= Managers.Data.Sooms[1300 + CurSoomLevel+1].Cotton)
            IsCotton = true;
        else
            return;

        IsUpgrdae = true;
        //방체크,가구체크

    }
}
