using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_CatHouseScene_Upper : UI_Scene
{
    enum Texts
    {
        ChargeTime,
        CoinMount,
        JellyMount,
    }

    enum Images
    {
        Heart1,
        Heart2,
        Heart3,
        Heart4,
        Heart5,
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.CoinMount).text  = 99999999.ToString();// 코인,젤리양 데이터 추가
        GetText((int)Texts.JellyMount).text = 99999999.ToString();
    }

    private void Update()
    {
        //행도력 시스템 갱신추가
    }

    void CoinOpen()
    {
        //추후 이벤트 추가 (과금시스템)
    }
    void JellyOpen()
    {
        //추후 이벤트추가(충전 or 과금)
    }
}
