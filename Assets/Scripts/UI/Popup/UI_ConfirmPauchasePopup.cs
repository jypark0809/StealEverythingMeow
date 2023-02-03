using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ConfirmPauchasePopup : UI_Popup
{
    int id;
    FurnitureData fData;

    enum Texts
    {
        ItemNameText,
        PriceText,
    }

    enum Buttons
    {
        PurchaseButton,
        CancleButton
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

        id = PlayerPrefs.GetInt("ItemId");
        Managers.Data.Furnitures.TryGetValue(id, out fData);

        GetText((int)Texts.ItemNameText).text = fData.F_Name;
        GetText((int)Texts.PriceText).text = fData.F_Gold.ToString();

        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnPurchaseButtonClicked);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CancleButtonClicked);
    }

    void OnPurchaseButtonClicked(PointerEventData evt)
    {
        // Happiness Point

        // Gold
        // Managers.Game.SaveData.Gold -= fData.F_Gold;

        // Add furniture to fList of save data
        Managers.Game.SaveData.FList.Add(fData);

        // arrange Furniture
        GameObject go = Managers.Resource.Instantiate($"Furniture/{fData.F_Space_Num}/{fData.F_Int_Name}");
        go.transform.position = new Vector2(go.transform.localPosition.x + 0.5f, go.transform.localPosition.y + 0.5f);

        // Save Data
        Managers.Game.SaveGame();

        // Close All Popup UI;
        Managers.UI.CloseAllPopupUI();
    }

    void CancleButtonClicked(PointerEventData evt)
    {
        ClosePopupUI();
    }
}
