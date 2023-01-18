using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_CatHouseScene_Upper : UI_Scene
{
    enum Texts
    {
        Current_Room_Text_Step,
        JellyText,
        DiamondText,
        GoldText,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.JellyText).text = "5 / 5";// 123.ToString();
        GetText((int)Texts.DiamondText).text = 999999.ToString();
        GetText((int)Texts.GoldText).text  = 999999.ToString();// ����,������ ������ �߰�



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
