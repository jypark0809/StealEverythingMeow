using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UpgradeSom : UI_Popup
{
    enum Texts
    {
        GoldText,
        CottonValue,
        StoneValue,
        WoodValue,
    }

    enum Buttons
    {
        CloseButton,
        OkayButton,
        CancleButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.GoldText).text = "$999,999";
        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OnOkayButton);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(OnCloseButton);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
        GetText((int)Texts.CottonValue).text = "999"; //차후 추가
        GetText((int)Texts.StoneValue).text = "999";//차후 추가
        GetText((int)Texts.WoodValue).text = "999";//차후 추가
        //Util.FindChild(this.gameObject, "Cotton", true).SetActive(false);
    }

    void OnOkayButton(PointerEventData evt)
    {
        if (Managers.Game.SaveData.Gold < -1) //조건체크
        {
            Debug.Log("not enough money");
            Managers.UI.ClosePopupUI();
        }
        else
        {
            GameObject go = Util.FindChild(Managers.Object.CatHouse.gameObject, "Somsom", true);
            go.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Furniture/woodhouse");
            Managers.UI.ClosePopupUI();
        }

    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
