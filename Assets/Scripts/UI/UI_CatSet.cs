using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_CatSet : UI_Base
{
    string Name;
    public int Index;


    enum Images
    {
        CatImage,
        BlockImage
    }
    enum Texts
    {
        CatName,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        if (Managers.Game.SaveData.CatHave[Index])
            Get<Image>((int)Images.BlockImage).gameObject.SetActive(false);

        Get<Image>((int)Images.CatImage).sprite = Resources.Load<Sprite>(("Sprites/UI/" + Name));
        Get<TextMeshProUGUI>((int)Texts.CatName).GetComponent<TextMeshProUGUI>().text = Managers.Data.CatBooks[1401+Index].Cat_Name;
        GetImage((int)Images.CatImage).gameObject.BindEvent(OpenDetail, Define.UIEvent.Click);
    }

    public void SetInfo(int _index)
    {
        Index = _index;
    }

    public void OpenDetail(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_StatDetail>().SetInfo(Index);
    }
    
}
