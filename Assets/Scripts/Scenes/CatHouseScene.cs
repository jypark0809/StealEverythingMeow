using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHouseScene : BaseScene
{
    int ClickCount = 0;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.CatHouseScene;
        StartCoroutine(CoWaitLoad());
    }

    IEnumerator CoWaitLoad()
    {
        while (Managers.Data.Loaded() == false)
            yield return null;

        Managers.Object.SpawnCatHouse("CatHouse_" + Managers.Game.SaveData.SpaceLevel);
        Managers.Object.SpawnSoom("Soom");
        Managers.Object.SpawnCat("LobbyCat/");
        Managers.UI.ShowSceneUI<UI_CatHouseScene>();
        
        for (int i = 0; i < Managers.Game.SaveData.FList.Count; i++)
        {
            FurnitureData fData = Managers.Game.SaveData.FList[i];
            GameObject go = Managers.Resource.Instantiate($"Furniture/{fData.F_Space_Num}/{fData.F_Int_Name}");
            go.transform.position = new Vector2(go.transform.localPosition.x + 0.5f, go.transform.localPosition.y + 0.5f);
        }

        if (Managers.Game.SaveData.firstExecution == true)
        {
            //Managers.UI.ShowPopupUI<UI_FindHelp>();
            Managers.UI.ShowPopupUI<UI_ConversationScript>();
        }
        else
            Managers.Sound.Play(Define.Sound.Bgm, "BGM/BGM_Home", volume: 0.1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc");
            ClickCount++;
            if (ClickCount == 2)
            {
                ClickCount = 0;
                Application.Quit();
            }
        }
    }

    public override void Clear()
    {
    }
}
