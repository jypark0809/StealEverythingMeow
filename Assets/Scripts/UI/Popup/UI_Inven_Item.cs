using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class UI_Inven_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        Num_Text,
    }
    string _name;
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        //Get<GameObject>((int)GameObjects.ItemIcon);
        Get<GameObject>((int)GameObjects.Num_Text).GetComponent<TextMeshProUGUI>().text = "999";// 보유수량 추가
        Get<GameObject>((int)GameObjects.ItemIcon).gameObject.BindEvent(OnInstantPre);// 이벤트추가

    }
    void OnInstantPre(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Inven_Item_pre>()
    }

}
