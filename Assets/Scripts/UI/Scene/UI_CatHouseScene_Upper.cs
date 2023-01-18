using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_CatHouseScene_Upper : UI_Scene
{
    enum Texts
    {
        Current_Room_Text_Step,
        CoinMount,
        DiaMount,
        JellyMount,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetText((int)Texts.JellyMount).text = "123 / 123";//123.ToString();
        GetText((int)Texts.DiaMount).text = 99999999.ToString();
        GetText((int)Texts.CoinMount).text  = 99999999.ToString();// ����,������ ������ �߰�



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
