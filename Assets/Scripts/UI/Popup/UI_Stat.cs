using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Stat : UI_Popup
{
    public float catNum = 0;
    public float ExpressNum = 0;

    public GameObject _Stats;
    public GameObject _Express;
    enum GameObjects
    {
        CatStats,
        CatExpress,
        CatStat,
        CatExpresses,
    }
    enum Buttons
    {
        CloseButton,
        CatStatButton,
        CatExpressButton
    }
    enum Images
    {
        StatCompelteBar,
        ExpressCompelteBar
    }
    enum Texts
    {
        StatPercent,
        ExpressPercent
    }
    void Start()
    {
        Init();
    }
    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopupUI();
        }
#endif
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        SetCat();
        SetExpress();

        GetButton((int)Buttons.CatStatButton).gameObject.BindEvent(OpenStat);
        GetButton((int)Buttons.CatExpressButton).gameObject.BindEvent(OpenExpress);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);


        GetImage((int)Images.StatCompelteBar).fillAmount = catNum / Managers.Game.SaveData.CatHave.Length;
        GetText((int)Texts.StatPercent).text = "���� �ϼ��� : " + (catNum / Managers.Game.SaveData.CatHave.Length) * 100 + "%";
        GetImage((int)Images.ExpressCompelteBar).fillAmount = ExpressNum / (Define.MOTION_COUNT);
        GetText((int)Texts.ExpressPercent).text = "���� �ϼ��� : " + (ExpressNum / (Define.MOTION_COUNT) )* 100 + "%";

        OpenSet();
    }


    void OpenSet()
    {
        Get<GameObject>((int)GameObjects.CatStats).SetActive(true);
        Get<GameObject>((int)GameObjects.CatExpresses).SetActive(false);
        GetButton((int)Buttons.CatStatButton).interactable = true;
        GetButton((int)Buttons.CatExpressButton).interactable = false;
    }


    void OpenStat(PointerEventData evt)
    {
        Get<GameObject>((int)GameObjects.CatStats).SetActive(true);
        Get<GameObject>((int)GameObjects.CatExpresses).SetActive(false);
        GetButton((int)Buttons.CatStatButton).interactable = true;
        GetButton((int)Buttons.CatExpressButton).interactable = false;
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
    }
    void OpenExpress(PointerEventData evt)
    {
        Get<GameObject>((int)GameObjects.CatStats).SetActive(false);
        Get<GameObject>((int)GameObjects.CatExpresses).SetActive(true);
        GetButton((int)Buttons.CatStatButton).interactable = false;
        GetButton((int)Buttons.CatExpressButton).interactable = true;
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");
    }

    void SetCat()
    {
        _Stats = Get<GameObject>((int)GameObjects.CatStat);

        foreach (Transform child in _Stats.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < Managers.Game.SaveData.CatHave.Length; i++)
        {
            if (Managers.Game.SaveData.CatHave[i])
            {
                catNum++;
            }
            UI_CatSet Item = Managers.UI.MakeSubItem<UI_CatSet>(_Stats.transform);
            Item.SetInfo(i);
        }
    }
    void SetExpress()
    {
        _Express = Get<GameObject>((int)GameObjects.CatExpress);

        foreach (Transform child in _Express.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < Managers.Game.SaveData.Emotion.Length; i++)
        {
            if (Managers.Game.SaveData.Emotion[i])
            {
                ExpressNum++;
            }
            UI_ExpressSet Item = Managers.UI.MakeSubItem<UI_ExpressSet>(_Express.transform);
            Item.SetInfo(i);
        }
    }
    public void ReBarCat()
    {
        catNum++;
        GetImage((int)Images.StatCompelteBar).fillAmount = catNum / Managers.Game.SaveData.CatHave.Length;
        GetText((int)Texts.StatPercent).text = "���� �ϼ��� : " + (catNum / Managers.Game.SaveData.CatHave.Length) * 100 + "%";
    }
    public void ReBarExpress()
    {
        ExpressNum++;
        GetImage((int)Images.ExpressCompelteBar).fillAmount = ExpressNum / (Define.MOTION_COUNT);
        GetText((int)Texts.ExpressPercent).text = "���� �ϼ��� : " + (ExpressNum / (Define.MOTION_COUNT)) * 100 + "%";
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();

    }
}