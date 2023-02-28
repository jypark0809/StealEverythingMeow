using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_CatSet : UI_Base
{
    private string[] CatName = { "White", "Black", "Calico", "Tabby", "Gray" };

    public int Index;
    enum Images
    {
        CatImage,
        BlockImage,
        UI_CatSet
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

        Get<Image>((int)Images.CatImage).sprite = Resources.Load<Sprite>(("Sprites/Nyan/" + CatName[Index]+"/"+ CatName[Index]+"_Walk1"));
        Get<TextMeshProUGUI>((int)Texts.CatName).GetComponent<TextMeshProUGUI>().text = Managers.Game.SaveData.CatName[Index];
        GetImage((int)Images.UI_CatSet).gameObject.BindEvent(OpenDetail, Define.UIEvent.Click);
    }
    public void SetInfo(int _index)
    {
        Index = _index;
    }

    public void OpenDetail(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_StatDetail>().SetInfo(Index);
    }

    public void OpenBlock()
    {
        Get<Image>((int)Images.BlockImage).gameObject.SetActive(false);
    }

}
