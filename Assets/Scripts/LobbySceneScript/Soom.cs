using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Soom : MonoBehaviour
{
    int pointerID;

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
        pointerID = -1; //PC나 유니티 상에서는 -1
#elif UNITY_ANDROID
        pointerID = 0;  // 휴대폰이나 이외에서 터치 상에서는 0 
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
        if (!IsPointerOverUIObject(Input.mousePosition))
        {
            Managers.UI.ShowPopupUI<UI_UpgradePopUp>();
        }
    }

    public void SomUpgrade()
    {
        Managers.UI.ClosePopupUI();
        //재화소모
        Managers.Game.SaveData.Wood -= Managers.Data.Sooms[1300 + CurSoomLevel + 1].Wood;
        Managers.Game.SaveData.Stone -= Managers.Data.Sooms[1300 + CurSoomLevel + 1].Stone;
        Managers.Game.SaveData.Cotton -= Managers.Data.Sooms[1300 + CurSoomLevel + 1].Cotton;
        (Managers.UI.SceneUI as UI_CatHouseScene)._catHouseSceneTop.RefreshUI();

        CurSoomLevel++;
        Managers.Game.SaveData.SoomLevel++;
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
            Managers.Game.SaveData.Emotion[(int)Define.CatEmotion.Scratch] = true;
            Managers.Game.SaveData.Emotion[(int)Define.CatEmotion.Sway] = true;
            Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + (int)Define.CatEmotion.Scratch].Express_Int_Name);
            Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + (int)Define.CatEmotion.Sway].Express_Int_Name);
            Managers.UI.ShowPopupUI<UI_ExpressOpen>();
            Camera.main.GetComponent<CameraMove>().Index = 1;
        }
        if (CurSoomLevel == 3)
        {
            Managers.Game.SaveData.Emotion[(int)Define.CatEmotion.Attack] = true;
            Managers.Game.SaveData.Emotion[(int)Define.CatEmotion.Lick] = true;
            Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + (int)Define.CatEmotion.Attack].Express_Int_Name);
            Managers.Game.SaveData.EmotionList.Add(Managers.Data.ExpressBooks[1501 + (int)Define.CatEmotion.Lick].Express_Int_Name);
            Managers.UI.ShowPopupUI<UI_ExpressOpen>();
            Camera.main.GetComponent<CameraMove>().Index = 2;
            Managers.Game.SaveData.IsSoomUp = false;
        }
        Managers.UI.ShowPopupUI<UI_CatPlus>();
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

    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touchPos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
