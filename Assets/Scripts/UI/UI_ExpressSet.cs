using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ExpressSet : UI_Base
{
    public int Index;
    private Animator theanim;
    enum Images
    {
        ExpressImage,
        BlockImage,
        UI_ExpressSet
    }
    enum Texts
    {
        ExpressName,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        theanim = Get<Image>((int)Images.ExpressImage).GetComponent<Animator>();

        if (Managers.Game.SaveData.Emotion[Index])
            Get<Image>((int)Images.BlockImage).gameObject.SetActive(false);

        Get<Image>((int)Images.ExpressImage).sprite = Resources.Load<Sprite>(("Sprites/Nyan/White/White_"+ Managers.Data.ExpressBooks[1501 + Index].Express_Int_Name));
        Get<TextMeshProUGUI>((int)Texts.ExpressName).GetComponent<TextMeshProUGUI>().text = Managers.Data.ExpressBooks[1501 + Index].Express_Name;
        GetImage((int)Images.UI_ExpressSet).gameObject.BindEvent(OpenExpress, Define.UIEvent.Click);

    }

    public void SetInfo(int _index)
    {
        Index = _index;
    }

    public void OpenExpress(PointerEventData evt)
    {
        Debug.Log("감정표현열기!");
        //Managers.UI.ShowPopupUI<UI_StatDetail>().SetInfo(Index);
    }

}
