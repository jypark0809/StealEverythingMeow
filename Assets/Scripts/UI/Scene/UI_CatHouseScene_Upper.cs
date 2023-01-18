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

        GetText((int)Texts.CoinMount).text  = 99999999.ToString();// ����,������ ������ �߰�
        GetText((int)Texts.JellyMount).text = 99999999.ToString();
    }

    private void Update()
    {
        //�൵�� �ý��� �����߰�
    }

    void CoinOpen()
    {
        //���� �̺�Ʈ �߰� (���ݽý���)
    }
    void JellyOpen()
    {
        //���� �̺�Ʈ�߰�(���� or ����)
    }
}
